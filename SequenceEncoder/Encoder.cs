using System.Collections.Generic;
using System;
using SequenceEncoder.Models;
using System.Linq;

namespace SequenceEncoder;

public class Encoder
{
    /// <summary>
    /// Creates a 2-dimensional array of integer pairs.
    /// The first entry is the number that repeats in the sequence.
    /// The second entry is the number of times it repeats.
    /// </summary>
    /// <returns>Shape: "[[number,count],...]"</returns>
    public static string Encode(IList<int> sequence)
    {
        ArgumentNullException.ThrowIfNull(sequence);

        var numberCountList = new NumberCountList();

        if (sequence.Count == 0)
        {
            return numberCountList.ToString();
        }

        // Initialize the first NumberCount
        var numberCount = new NumberCount(sequence[0], 1);

        for (var i = 1; i < sequence.Count; i++)
        {
            if (numberCount.Number != sequence[i])
            {
                // Entered a new sequence, so add the active one and start a new one.
                numberCountList.Add(numberCount);

                numberCount = new NumberCount(sequence[i], 0);
            }

            numberCount.Count++;
        }

        // Add the last NumberCount
        numberCountList.Add(numberCount);

        return numberCountList.ToString();
    }

    /// <summary>
    /// Parses the <paramref name="encodedSequence"/> into a <see cref="NumberCountList"/> and then builds it into a list of integers.
    /// </summary>
    /// <param name="encodedSequence">An encoded sequence in the format of "[[#,#],[#,#],...]".</param>
    /// <returns>The decoded sequence of numbers.</returns>
    public static IEnumerable<int> Decode(string encodedSequence)
    {
        ArgumentNullException.ThrowIfNull(encodedSequence);

        var numberCountList = NumberCountList.Parse(encodedSequence);

        var decodedSequence = new List<int>();

        for (var i = 0; i < numberCountList.Count; i++)
        {
            var explodedNumber = Enumerable.Repeat(
                numberCountList[i].Number,
                numberCountList[i].Count);

            decodedSequence.AddRange(explodedNumber);
        }

        return decodedSequence;
    }
}
