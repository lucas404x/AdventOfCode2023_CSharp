using System.Diagnostics;

namespace Aoc2023.Days;

internal class Day5 : Day
{
    private struct MapperInstruction
    {
        public long DestRange { get; set; }
        public long SourceRange { get; set; }
        public long RangeLength { get; set; }
    }

    private struct SeedRangeInput
    {
        public long StartRange { get; set; }
        public long RangeLength { get; set; }
    }

    public override string FirstHalf()
    {
        var mappers = new Dictionary<string, List<MapperInstruction>>();
        string currentDestinationKey = string.Empty;
        string currentSourceKey = string.Empty;

        for (int i = 2; i < Input.Length; i++)
        {
            string line = Input[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            else if (line.Contains("map"))
            {
                string[] mapperInfo = line.Split("to");
                currentSourceKey = mapperInfo[0][..^1];
                currentDestinationKey = mapperInfo[1][1..^5];
            }
            else if (!string.IsNullOrEmpty(currentDestinationKey) && !string.IsNullOrEmpty(currentSourceKey))
            {
                string[] instruction = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var mapperInstruction = new MapperInstruction
                {
                    DestRange = long.Parse(instruction[0]),
                    SourceRange = long.Parse(instruction[1]),
                    RangeLength = long.Parse(instruction[2])
                };
                string mapperKey = $"{currentSourceKey}-{currentDestinationKey}";
                if (mappers.TryGetValue(mapperKey, out List<MapperInstruction>? mapInstructions)) mapInstructions.Add(mapperInstruction);
                else mappers.Add(mapperKey, [mapperInstruction]);
            }
        }

        long lowestLocation = -1;
        string currentSrcKey = "seed";

        string[] requiredSeeds = Input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string seed in requiredSeeds)
        {
            long sourceVal = long.Parse(seed);
            do
            {
                var mapper = mappers.First(x => x.Key.StartsWith(currentSrcKey));
                var mapperInstructions = mapper.Value;
                foreach (MapperInstruction instruction in mapperInstructions)
                {
                    long sourceRangeLimit = instruction.SourceRange + instruction.RangeLength - 1;
                    if (sourceVal >= instruction.SourceRange && sourceVal <= sourceRangeLimit)
                    {
                        long diff = Math.Abs(instruction.DestRange - instruction.SourceRange);
                        sourceVal = instruction.DestRange > instruction.SourceRange ? sourceVal + diff : sourceVal - diff;
                        break;
                    }
                }
                currentSrcKey = mapper.Key.Split('-')[1];
            } while (currentSrcKey != "location");

            if (lowestLocation == -1) lowestLocation = sourceVal;
            else lowestLocation = Math.Min(lowestLocation, sourceVal);
            currentSrcKey = "seed";
        }

        return lowestLocation.ToString();
    }

    public override string SecondHalf()
    {
        var watch = Stopwatch.StartNew();
        var mappers = new Dictionary<string, List<MapperInstruction>>();
        string currentDestinationKey = string.Empty;
        string currentSourceKey = string.Empty;

        for (int i = 2; i < Input.Length; i++)
        {
            string line = Input[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            else if (line.Contains("map"))
            {
                string[] mapperInfo = line.Split("to");
                currentSourceKey = mapperInfo[0][..^1];
                currentDestinationKey = mapperInfo[1][1..^5];
            }
            else if (!string.IsNullOrEmpty(currentDestinationKey) && !string.IsNullOrEmpty(currentSourceKey))
            {
                string[] instruction = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var mapperInstruction = new MapperInstruction
                {
                    DestRange = long.Parse(instruction[0]),
                    SourceRange = long.Parse(instruction[1]),
                    RangeLength = long.Parse(instruction[2])
                };
                string mapperKey = $"{currentSourceKey}-{currentDestinationKey}";
                if (mappers.TryGetValue(mapperKey, out List<MapperInstruction>? mapInstructions)) mapInstructions.Add(mapperInstruction);
                else mappers.Add(mapperKey, [mapperInstruction]);
            }
        }

        long lowestLocation = -1;
        string currentSrcKey = "seed";
        var seedRangeInputs = new List<SeedRangeInput>();

        string[] seedsInfo = Input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 2; i <= seedsInfo.Length; i+=2)
        {
            seedRangeInputs.Add(new SeedRangeInput
            {
                StartRange = long.Parse(seedsInfo[i - 2]),
                RangeLength = long.Parse(seedsInfo[i - 1])
            });
        }

        foreach (var seedRangeInput in seedRangeInputs)
        {
            for (long seed = seedRangeInput.StartRange; seed <= seedRangeInput.StartRange + seedRangeInput.RangeLength; seed++)
            {
                long sourceVal = seed;
                do
                {
                    var mapper = mappers.First(x => x.Key.StartsWith(currentSrcKey));
                    var mapperInstructions = mapper.Value;
                    foreach (MapperInstruction instruction in mapperInstructions)
                    {
                        long sourceRangeLimit = instruction.SourceRange + instruction.RangeLength - 1;
                        if (sourceVal >= instruction.SourceRange && sourceVal <= sourceRangeLimit)
                        {
                            long diff = Math.Abs(instruction.DestRange - instruction.SourceRange);
                            sourceVal = instruction.DestRange > instruction.SourceRange ? sourceVal + diff : sourceVal - diff;
                            break;
                        }
                    }
                    currentSrcKey = mapper.Key.Split('-')[1];
                } while (currentSrcKey != "location");

                if (lowestLocation == -1) lowestLocation = sourceVal;
                else lowestLocation = Math.Min(lowestLocation, sourceVal);
                currentSrcKey = "seed";
            }
        }

        Console.WriteLine($"Took {watch.Elapsed.TotalMinutes} minutes.");

        return lowestLocation.ToString();
    }
}
