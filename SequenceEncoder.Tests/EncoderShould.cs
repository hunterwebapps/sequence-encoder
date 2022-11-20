using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SequenceEncoder.Tests;

public class EncoderShould
{
    [Test]
    public void CompressTheCodeTestExample()
    {
        // ARRANGE
        var sequence = new List<int>();
        sequence.AddRange(Enumerable.Repeat(2, 3));
        sequence.AddRange(Enumerable.Repeat(5, 4));
        sequence.AddRange(Enumerable.Repeat(9, 1993));

        var unencodedSequence = JsonSerializer.Serialize(sequence);

        // ACT
        var encodedSequence = Encoder.Encode(sequence.ToArray());

        // ASSERT
        Assert.That(encodedSequence.Length, Is.LessThan(unencodedSequence.Length));
    }

    [TestCase(new int[0], "[]")]
    [TestCase(new[] { 1, 1, 1, 2, 2, 2, 2 }, "[[1,3],[2,4]]")]
    public void EncodeTheSequence(int[] sequence, string expectedEncoding)
    {
        // ARRANGE;

        // ACT
        var encodedSequence = Encoder.Encode(sequence);

        // ASSERT
        Assert.That(encodedSequence, Is.EqualTo(expectedEncoding));
    }

    [Test]
    public void ThrowIfEncodeNull()
    {
        // ARRANGE

        // ACT & ASSERT
        Assert.Throws<ArgumentNullException>(() => Encoder.Encode(null));
    }

    [TestCase("[]", new int[0])]
    [TestCase("[[1,3],[2,4]]", new[] { 1, 1, 1, 2, 2, 2, 2 })]
    public void DecodeTheSequence(string encodedSequence, int[] expectedDecoded)
    {
        // ARRANGE

        // ACT
        var decodedSequence = Encoder.Decode(encodedSequence);

        // ASSERT
        Assert.That(decodedSequence, Is.EquivalentTo(expectedDecoded));
    }

    [Test]
    public void ThrowIfDecodeNull()
    {
        // ARRANGE

        // ACT & ASSERT
        Assert.Throws<ArgumentNullException>(() => Encoder.Decode(null));
    }

    [Test]
    public void OptimizeOutputSize()
    {
        // ARRANGE
        var largeArrayString = File.ReadAllText("./MockData/large_array.json").Replace(" ", "");
        var sequence = JsonSerializer.Deserialize<int[]>(largeArrayString);

        // ACT
        var encodedSequence = Encoder.Encode(sequence);
        
        // ASSERT
        Assert.That(encodedSequence, Has.Length.LessThan(largeArrayString.Length));
    }
}