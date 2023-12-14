namespace Aoc2023.Days;
internal class Day4 : Day
{
    public override string FirstHalf()
    {
        long sumOfMatchedPoints = 0;

        foreach (string line in Input)
        {
            string[] card = line.Split('|');
            string[] winnerPoints = card[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] myPoints = card[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int matches = winnerPoints.Where(x => myPoints.Contains(x)).Count();
            sumOfMatchedPoints += (long)Math.Pow(2, matches - 1);
        }

        return sumOfMatchedPoints.ToString();
    }

    public override string SecondHalf()
    {
        long sumOfCardCopies = 0;
        var cardIdxCopies = new Dictionary<int, int>();

        for (int i = 0; i < Input.Length; i++)
        {
            string[] card = Input[i].Split('|');
            string[] winnerPoints = card[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] myPoints = card[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            int currentCardCopies = 1;
            if (cardIdxCopies.TryGetValue(i, out int v)) currentCardCopies = ++cardIdxCopies[i];
            else cardIdxCopies.Add(i, 1);

            int matches = winnerPoints.Where(x => myPoints.Contains(x)).Count();
            for (int j = 1; j <= matches; j++)
            {
                int nextCardIdx = i + j;
                if (cardIdxCopies.TryGetValue(nextCardIdx, out int _)) cardIdxCopies[nextCardIdx] += currentCardCopies;
                else cardIdxCopies.Add(nextCardIdx, currentCardCopies);
            }

            sumOfCardCopies += currentCardCopies;
        }

        return sumOfCardCopies.ToString();
    }
}
