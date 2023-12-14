namespace Aoc2023.Days;

internal class Day6 : Day
{
    public override string FirstHalf()
    {
        int totalWaysToBeatRace = 1;

        string[] times = Input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] distances = Input[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < times.Length; i++)
        {
            var raceTime = long.Parse(times[i]);
            var recordDist = long.Parse(distances[i]);

            // Calculate 'c' and 'delta' for the quadratic formula.
            // The 'c' cannot be the 'recordDist' only, we need to get the next value from the fn to retrieve the right Xs.
            double c = (recordDist + (recordDist / raceTime));
            double delta = Math.Sqrt((raceTime * raceTime) - 4 * c);

            // Calculate the number of ways to beat the race.
            int n1 = (int)Math.Round((-raceTime + delta) / 2);
            int n2 = (int)Math.Round((-raceTime - delta) / 2);

            // Update the total number of ways to beat the race.
            totalWaysToBeatRace *= (n1 - n2) + 1;
        }

        return totalWaysToBeatRace.ToString();
    }
}
