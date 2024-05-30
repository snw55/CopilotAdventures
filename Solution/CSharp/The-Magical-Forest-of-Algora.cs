using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class Creature
{
    public string Name { get; set; }
    public List<string> Moves { get; set; }
}
public class Forest
{
    private Creature creature1;
    private Creature creature2;
    private HttpClient client;

    public Forest(Creature creature1, Creature creature2)
    {
        this.creature1 = creature1;
        this.creature2 = creature2;
        this.client = new HttpClient();
        this.client.DefaultRequestHeaders.Add("api-key", "983adf5268414b19841bd91cd3feec89");
    }

    public async Task PerformDance()
    {
        for (int i = 0; i < creature1.Moves.Count; i++)
        {
            var creature1Move = creature1.Moves[i];
            var creature2Move = creature2.Moves[i];

            string forestState;

            if (creature1Move == "Twirl" && creature2Move == "Twirl")
            {
                forestState = "Fireflies light up the forest";
            }
            else if ((creature1Move == "Leap" && creature2Move == "Spin") || (creature1Move == "Spin" && creature2Move == "Leap"))
            {
                forestState = "Gentle rain starts falling";
            }
            else if ((creature1Move == "Spin" && creature2Move == "Leap") || (creature1Move == "Leap" && creature2Move == "Spin"))
            {
                forestState = "A rainbow appears in the sky";
            }
            else
            {
                forestState = "Other magical effect";
            }

            Console.WriteLine($"After sequence {i + 1}, the state of the forest is: {forestState}");

            var content = new StringContent(JsonConvert.SerializeObject(new { prompt = forestState }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://oai-sbp-techmarket24.openai.azure.com/openai/deployments/dalle3/images/generations?api-version=2023-12-01-preview", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"DALL-E response: {responseContent}");
        }
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        var creature1 = new Creature { Name = "Lox", Moves = new List<string> { "Twirl", "Leap", "Spin", "Twirl", "Leap" } };
        var creature2 = new Creature { Name = "Drako", Moves = new List<string> { "Spin", "Twirl", "Leap", "Leap", "Spin" } };

        var forest = new Forest(creature1, creature2);
        await forest.PerformDance();
    }
}