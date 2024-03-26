using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

public enum Move
{
    None,
    Twirl,
    Leap,
    Spin
}

public class Forest
{
    public bool Fireflies { get; set; }
    public bool Rain { get; set; }
    public bool Rainbow { get; set; }

    private readonly Dictionary<(Move, Move), Action<Forest>> danceEffects = new Dictionary<(Move, Move), Action<Forest>>
    {
        { (Move.Twirl, Move.Twirl), forest => forest.Fireflies = true },
        { (Move.Leap, Move.Spin), forest => forest.Rain = true },
        { (Move.Spin, Move.Leap), forest => forest.Rainbow = true }
    };

    public void UpdateState(Move move1, Move move2)
    {
        if (danceEffects.TryGetValue((move1, move2), out var effect))
        {
            effect.Invoke(this);
        }
    }

    public override string ToString()
    {
        return $"Fireflies: {Fireflies}, Rain: {Rain}, Rainbow: {Rainbow}";
    }
}

public class Algora
{
    public static async Task<Move> GetMove(string creatureName)
    {
        var config = SpeechConfig.FromSubscription("", "");
        config.SpeechRecognitionLanguage = "en-US";

        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var recognizer = new SpeechRecognizer(config, audioConfig);

        while (true)
        {
            Console.WriteLine($"Enter a move for {creatureName} (or 'done' to finish): ");
            var result = await recognizer.RecognizeOnceAsync();

            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                string moveInput = result.Text;
                if (moveInput.EndsWith("."))
                {
                    moveInput = moveInput.Remove(moveInput.Length - 1);
                }

                if (moveInput.ToLower() == "done") return Move.None;
                try
                {
                    return Enum.Parse<Move>(moveInput.Trim());
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Invalid move: {moveInput}. Please enter a valid move.");
                }
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                Console.WriteLine("No speech could be recognized.");
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    Console.WriteLine($"CANCELED: Did you update the subscription info?");
                }
            }
        }
    }

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to the Magical Forest of Algora!");

        Forest forest = new();

        while (true)
        {
            Move loxMove = await GetMove("Lox");
            if (loxMove == Move.None) break;

            Move drakoMove = await GetMove("Drako");
            if (drakoMove == Move.None) break;

            forest.UpdateState(loxMove, drakoMove);
            Console.WriteLine($"State of the forest after this round: {forest}");
        }

        Console.WriteLine($"Final state of the forest: {forest}");
    }
}