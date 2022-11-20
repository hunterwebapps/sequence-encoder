using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace SequenceEncoder.Models;

public class NumberCountList
{
    private readonly List<NumberCount> numberCounts = new();

    public NumberCount this[int param] => numberCounts[param];
    public int Count => numberCounts.Count;
    public void Add(NumberCount numberCount) => numberCounts.Add(numberCount);

    public override string ToString()
    {
        var numberCountPairs = numberCounts.Select(x => $"[{x.Number},{x.Count}]");
        var joinedNumberCounts = string.Join(',', numberCountPairs);

        return $"[{joinedNumberCounts}]";
    }

    /// <summary>
    /// Parses a serialized 2-dimensional array of numbers into a <see cref="NumberCountList"/>.
    /// </summary>
    /// <param name="serializedNumberCountList">A 2-dimensional array in the format of [[#,#],[#,#],...]</param>
    /// <returns>An instance of <see cref="NumberCountList"/> parsed from <paramref name="serializedNumberCountList"/>.</returns>
    public static NumberCountList Parse(string serializedNumberCountList)
    {
        var numberCountList = new NumberCountList();

        var serializedNumberCounts = JsonSerializer.Deserialize<int[][]>(serializedNumberCountList);

        for (var i = 0; i < serializedNumberCounts.Length; i++)
        {
            var number = serializedNumberCounts[i][0];
            var count = serializedNumberCounts[i][1];
            var numberCount = new NumberCount(number, count);
            numberCountList.Add(numberCount);
        }

        return numberCountList;
    }
}
