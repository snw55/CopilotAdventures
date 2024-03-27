using Azure;
using Azure.AI.OpenAI;

public enum DanceMove
{
    Twirl,
    Leap,
    Spin
}

public class Creature
{
    public string Name { get; set; }
    public List<DanceMove> DanceMoves { get; set; }
}

public class Forest
{
    public List<string> States { get; set; } = new List<string>();

    public string GetDescription()
    {
        return string.Join(" ", States);
    }

    public async Task GenerateImage()
    {
        var description = this.GetDescription();
        string endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        string key = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
        string deploymentName = "dalle3";

        OpenAIClient client = new(new Uri(endpoint), new AzureKeyCredential(key));

        Response<ImageGenerations> imageGenerations = await client.GetImageGenerationsAsync(
            new ImageGenerationOptions()
            {
                Prompt = description,
                Size = ImageSize.Size1024x1024,
                DeploymentName = deploymentName
            });

        // Image Generations responses provide URLs you can use to retrieve requested images
        Uri imageUri = imageGenerations.Value.Data[0].Url;
        
        // Save the image to a file
        var httpClient = new HttpClient();
        var imageData = await httpClient.GetByteArrayAsync(imageUri);
        await File.WriteAllBytesAsync("forest.png", imageData);
    }
}

public class Program
{
    static async Task Main(string[] args)
    {
        var lox = new Creature
        {
            Name = "Lox",
            DanceMoves = new List<DanceMove> { DanceMove.Twirl, DanceMove.Leap, DanceMove.Spin, DanceMove.Twirl, DanceMove.Leap }
        };

        var drako = new Creature
        {
            Name = "Drako",
            DanceMoves = new List<DanceMove> { DanceMove.Spin, DanceMove.Twirl, DanceMove.Leap, DanceMove.Leap, DanceMove.Spin }
        };

        var forest = new Forest();

        for (int i = 0; i < 5; i++)
        {
            var loxMove = lox.DanceMoves[i];
            var drakoMove = drako.DanceMoves[i];

            Console.WriteLine($"Lox performs {loxMove} and Drako performs {drakoMove}");

            if (loxMove == DanceMove.Twirl && drakoMove == DanceMove.Twirl)
            {
                forest.States.Add("Fireflies light up the forest.");
            }
            else if ((loxMove == DanceMove.Leap && drakoMove == DanceMove.Spin) || (loxMove == DanceMove.Spin && drakoMove == DanceMove.Leap))
            {
                forest.States.Add(loxMove == DanceMove.Leap ? "Gentle rain starts falling." : "A rainbow appears in the sky.");
            }
            else
            {
                // If the dance moves combination doesn't have an effect, don't add a state
                continue;
            }

            Console.WriteLine(forest.GetDescription());
        }

        await forest.GenerateImage();
    }
}