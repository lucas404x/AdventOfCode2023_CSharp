using Aoc2023.Days;

static async Task Run<T>() where T : Day, new()
{
    var input = await File.ReadAllLinesAsync(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Inputs", $"{typeof(T).Name.ToLower()}.txt"));
    var day = new T
    {
        Input = input!
    };

    Console.WriteLine($"First Half: {day.FirstHalf()}");
    Console.WriteLine($"Second Half: {day.SecondHalf()}");
}

await Run<Day5>();