namespace Aoc2023.Days;

internal class Day3 : Day
{
    public override string FirstHalf()
    {
        long sumOfAdjacentNums = 0;

        int width = Input[0].Length;
        int height = Input.Length;
        for (int i = 0; i < height; i++)
        {
            string num = string.Empty;
            bool isNumAdjacentToSymbols = false;

            for (int j = 0; j < width; j++)
            {
                if (char.IsDigit(Input[i][j]))
                {
                    num += Input[i][j];
                    if (!isNumAdjacentToSymbols)
                    {
                        if (i > 0 && i + 1 < height && j > 0 && j + 1 < width) // Middle
                        {
                            bool xAxis = IsSymbol(Input[i][j + 1]) || IsSymbol(Input[i][j - 1]);
                            bool top = IsSymbol(Input[i - 1][j]) || IsSymbol(Input[i - 1][j + 1]) || IsSymbol(Input[i - 1][j - 1]);
                            bool bottom = IsSymbol(Input[i + 1][j]) || IsSymbol(Input[i + 1][j + 1]) || IsSymbol(Input[i + 1][j - 1]);
                            isNumAdjacentToSymbols = xAxis || top || bottom;
                        }
                        else if (i == 0) // Top
                        {
                            if (j == 0) // Start
                            {
                                bool xAxis = IsSymbol(Input[i][j + 1]);
                                bool bottom = IsSymbol(Input[i + 1][j]) || IsSymbol(Input[i + 1][j + 1]);
                                isNumAdjacentToSymbols = xAxis || bottom;
                            }
                            else if (j + 1 == width) // End
                            {
                                bool xAxis = IsSymbol(Input[i][j - 1]);
                                bool bottom = IsSymbol(Input[i + 1][j]) || IsSymbol(Input[i + 1][j - 1]);
                                isNumAdjacentToSymbols = xAxis || bottom;
                            }
                            else // Middle
                            {
                                bool xAxis = IsSymbol(Input[i][j + 1]) || IsSymbol(Input[i][j - 1]);
                                bool bottom = IsSymbol(Input[i + 1][j]) || IsSymbol(Input[i + 1][j + 1]) || IsSymbol(Input[i + 1][j - 1]);
                                isNumAdjacentToSymbols = xAxis || bottom;
                            }
                        }
                        else if (i + 1 == height) // Bottom
                        {
                            if (j == 0) // Start
                            {
                                bool xAxis = IsSymbol(Input[i][j + 1]);
                                bool top = IsSymbol(Input[i - 1][j]) || IsSymbol(Input[i - 1][j + 1]);
                                isNumAdjacentToSymbols = xAxis || top;
                            }
                            else if (j + 1 == width) // End
                            {
                                bool xAxis = IsSymbol(Input[i][j - 1]);
                                bool top = IsSymbol(Input[i - 1][j]) || IsSymbol(Input[i - 1][j - 1]);
                                isNumAdjacentToSymbols = xAxis || top;
                            }
                            else // Middle
                            {
                                bool top = IsSymbol(Input[i - 1][j]) || IsSymbol(Input[i - 1][j + 1]) || IsSymbol(Input[i - 1][j - 1]);
                                bool xAxis = IsSymbol(Input[i][j + 1]) || IsSymbol(Input[i][j - 1]);
                                isNumAdjacentToSymbols = xAxis || top;
                            }
                        }
                    }
                }
                if (!char.IsDigit(Input[i][j]) || j + 1 == width)
                {
                    if (isNumAdjacentToSymbols) sumOfAdjacentNums += long.Parse(num);
                    isNumAdjacentToSymbols = false;
                    num = string.Empty;
                }
            }
        }

        return sumOfAdjacentNums.ToString();
    }

    private static bool IsSymbol(char ch) => !char.IsDigit(ch) && ch != '.';

    public override string SecondHalf()
    {
        var starsNumsStorage = new Dictionary<Tuple<int, int>, List<long>>();

        int width = Input[0].Length;
        int height = Input.Length;
        for (int i = 0; i < height; i++)
        {
            Tuple<int, int>? lastStarPos = null;
            string num = string.Empty;
            bool isNumAdjacentToStar = false;

            for (int j = 0; j < width; j++)
            {
                if (char.IsDigit(Input[i][j]))
                {
                    num += Input[i][j];
                    if (!isNumAdjacentToStar)
                    {
                        if (i > 0 && i + 1 < height && j > 0 && j + 1 < width) // Middle
                        {
                            if (IsStar(Input[i][j + 1])) lastStarPos = Tuple.Create(i, j + 1);
                            else if (IsStar(Input[i][j - 1])) lastStarPos = Tuple.Create(i, j - 1);
                            else if (IsStar(Input[i - 1][j])) lastStarPos = Tuple.Create(i - 1, j);
                            else if (IsStar(Input[i - 1][j + 1])) lastStarPos = Tuple.Create(i - 1, j + 1);
                            else if (IsStar(Input[i - 1][j - 1])) lastStarPos = Tuple.Create(i - 1, j - 1);
                            else if (IsStar(Input[i + 1][j])) lastStarPos = Tuple.Create(i + 1, j);
                            else if (IsStar(Input[i + 1][j + 1])) lastStarPos = Tuple.Create(i + 1, j + 1);
                            else if (IsStar(Input[i + 1][j - 1])) lastStarPos = Tuple.Create(i + 1, j - 1);
                        }
                        else if (i == 0) // Top
                        {
                            if (j == 0) // Start
                            {
                                if (IsStar(Input[i][j + 1])) lastStarPos = Tuple.Create(i, j + 1);
                                else if (IsStar(Input[i + 1][j])) lastStarPos = Tuple.Create(i + 1, j);
                                else if (IsStar(Input[i + 1][j + 1])) lastStarPos = Tuple.Create(i + 1, j + 1);
                            }
                            else if (j + 1 == width) // End
                            {
                                if (IsStar(Input[i][j - 1])) lastStarPos = Tuple.Create(i, j - 1);
                                else if (IsStar(Input[i + 1][j])) lastStarPos = Tuple.Create(i + 1, j);
                                else if (IsStar(Input[i + 1][j - 1])) lastStarPos = Tuple.Create(i + 1, j - 1);
                            }
                            else // Middle
                            {
                                if (IsStar(Input[i][j + 1])) lastStarPos = Tuple.Create(i, j + 1);
                                else if (IsStar(Input[i][j - 1])) lastStarPos = Tuple.Create(i, j - 1);
                                else if (IsStar(Input[i + 1][j])) lastStarPos = Tuple.Create(i + 1, j);
                                else if (IsStar(Input[i + 1][j + 1])) lastStarPos = Tuple.Create(i + 1, j + 1);
                                else if (IsStar(Input[i + 1][j - 1])) lastStarPos = Tuple.Create(i + 1, j - 1);
                            }
                        }
                        else if (i + 1 == height) // Bottom
                        {
                            if (j == 0) // Start
                            {
                                if (IsStar(Input[i][j + 1])) lastStarPos = Tuple.Create(i, j + 1);
                                else if (IsStar(Input[i - 1][j])) lastStarPos = Tuple.Create(i - 1, j);
                                else if (IsStar(Input[i - 1][j + 1])) lastStarPos = Tuple.Create(i - 1, j + 1);
                            }
                            else if (j + 1 == width) // End
                            {
                                if (IsStar(Input[i][j - 1])) lastStarPos = Tuple.Create(i, j - 1);
                                else if (IsStar(Input[i - 1][j])) lastStarPos = Tuple.Create(i - 1, j);
                                else if (IsStar(Input[i - 1][j - 1])) lastStarPos = Tuple.Create(i - 1, j - 1);
                            }
                            else // Middle
                            {
                                if (IsStar(Input[i][j + 1])) lastStarPos = Tuple.Create(i, j + 1);
                                else if (IsStar(Input[i][j - 1])) lastStarPos = Tuple.Create(i, j - 1);
                                else if (IsStar(Input[i - 1][j])) lastStarPos = Tuple.Create(i - 1, j);
                                else if (IsStar(Input[i - 1][j + 1])) lastStarPos = Tuple.Create(i - 1, j + 1);
                                else if (IsStar(Input[i - 1][j - 1])) lastStarPos = Tuple.Create(i - 1, j - 1);
                            }
                        }

                        isNumAdjacentToStar = lastStarPos is not null;
                    }
                }
                if (!char.IsDigit(Input[i][j]) || j + 1 == width)
                {
                    if (lastStarPos is not null)
                    {
                        if (starsNumsStorage.TryGetValue(lastStarPos, out List<long>? value)) value.Add(long.Parse(num));
                        else starsNumsStorage.Add(lastStarPos, [long.Parse(num)]);
                    }
                    lastStarPos = null;
                    isNumAdjacentToStar = false;
                    num = string.Empty;
                }
            }
        }

        return starsNumsStorage
            .Where(x => x.Value.Count == 2)
            .Select(x => x.Value.Aggregate((current, next) => current * next))
            .Sum()
            .ToString();
    }

    private static bool IsStar(char ch) => ch == '*';
}
