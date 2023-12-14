using System.Collections.Frozen;
using System.Collections.Immutable;

namespace Aoc2023.Days;

internal class Day1 : Day
{
    private readonly FrozenDictionary<string, char> _spellToCardinalNums = new Dictionary<string, char>()
    {
        { "one", '1' },
        { "two", '2' },
        { "three", '3' },
        { "four", '4' },
        { "five", '5' },
        { "six", '6' },
        { "seven", '7' },
        { "eight", '8' },
        { "nine", '9' },
    }.ToFrozenDictionary();

    private readonly ImmutableArray<string> _spellNums;

    public Day1()
    {
        _spellNums = _spellToCardinalNums.Keys;
    }

    public override string FirstHalf() 
    {
        int accumulator = 0;
        
        foreach (string line in Input)
        {
            char? firstNum = null, secondNum = null;
            foreach (char ch in line)
            {
                if (char.IsNumber(ch))
                {
                    if (firstNum is null) firstNum = ch;
                    else secondNum = ch;
                }
            }
            secondNum ??= firstNum;
            accumulator += int.Parse($"{firstNum}{secondNum}");
        }

        return accumulator.ToString();
    }

    public override string SecondHalf()
    {
        int accumulator = 0;

        foreach (string line in Input)
        {
            char? firstNum = null, secondNum = null;
            string lineContentBuffer = string.Empty;
            foreach (char ch in line)
            {
                if (char.IsNumber(ch))
                {
                    lineContentBuffer = string.Empty;
                    if (firstNum is null) firstNum = ch;
                    else secondNum = ch;
                }
                else
                {
                    lineContentBuffer += ch;
                    var spellLineKey = _spellNums.FirstOrDefault(x => lineContentBuffer.Contains(x));
                    if (spellLineKey is not null)
                    {
                        lineContentBuffer = ch.ToString();
                        if (firstNum is null) firstNum = _spellToCardinalNums[spellLineKey];
                        else secondNum = _spellToCardinalNums[spellLineKey];
                    }
                }
            }
            secondNum ??= firstNum;
            accumulator += int.Parse($"{firstNum}{secondNum}");
        }

        return accumulator.ToString();
    }
}
