namespace Aoc2023.Days;

internal class Day6 : Day
{
    public override string FirstHalf()
    {
        int numberOfWaysToBeatRace = 1;

        string[] times = Input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] distances = Input[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < times.Length; i++)
        {
            var raceTime = long.Parse(times[i]);
            var recordDist = long.Parse(distances[i]);

            double c = (recordDist + (recordDist / raceTime));
            double delta = Math.Sqrt((-raceTime * -raceTime) - 4 * c);

            int n1 = (int)Math.Round((-raceTime + delta) / 2);
            int n2 = (int)Math.Round((-raceTime - delta) / 2);

            numberOfWaysToBeatRace *= (n1 - n2) + 1;
        }

        return numberOfWaysToBeatRace.ToString();
    }
}
