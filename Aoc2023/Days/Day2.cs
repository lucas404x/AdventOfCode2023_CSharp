namespace Aoc2023.Days;

internal class Day2 : Day
{
    public override string FirstHalf() 
    {
        int gamesIdSum = 0;

        foreach (string game in Input)
        {
            string[] gameData = game.Split(':');
            bool isGameConfigurationPossible = true;

            foreach (string round in gameData[1].Split(';', StringSplitOptions.TrimEntries))
            {
                var cubeColorCounters = new Dictionary<string, int>
                {
                    { "green", 0 },
                    { "blue", 0 },
                    { "red", 0 },
                };
                string[] cubes = round.Split(',', StringSplitOptions.TrimEntries);
                foreach (string cube in cubes)
                {
                    string[] cubeInfo = cube.Split(' ', StringSplitOptions.TrimEntries);
                    cubeColorCounters[cubeInfo[1]] += int.Parse(cubeInfo[0]);
                }
                if (cubeColorCounters["red"] > 12 || cubeColorCounters["green"] > 13 || cubeColorCounters["blue"] > 14)
                {
                    isGameConfigurationPossible = false;
                    break;
                }
            }

            if (isGameConfigurationPossible)
            {
                int gameId = int.Parse(gameData[0].Split(' ', StringSplitOptions.TrimEntries)[1]);
                gamesIdSum += gameId;
            }
        }

        return gamesIdSum.ToString();
    }

    public override string SecondHalf()
    {
        long sumOfSetsPower = 0;

        foreach (string game in Input)
        {
            string[] gameData = game.Split(':');
            var cubeColorMaxQuantity = new Dictionary<string, int>
            {
                { "green", 0 },
                { "blue", 0 },
                { "red", 0 },
            };

            foreach (string round in gameData[1].Split(';', StringSplitOptions.TrimEntries))
            {
                string[] cubes = round.Split(',', StringSplitOptions.TrimEntries);
                foreach (string cube in cubes)
                {
                    string[] cubeInfo = cube.Split(' ', StringSplitOptions.TrimEntries);
                    cubeColorMaxQuantity[cubeInfo[1]] = Math.Max(cubeColorMaxQuantity[cubeInfo[1]], int.Parse(cubeInfo[0]));
                }
            }
            
            long setPower = cubeColorMaxQuantity["red"] * cubeColorMaxQuantity["green"] * cubeColorMaxQuantity["blue"];
            sumOfSetsPower += setPower;
        }

        return sumOfSetsPower.ToString();
    }
}
