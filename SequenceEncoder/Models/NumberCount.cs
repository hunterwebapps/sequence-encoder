namespace SequenceEncoder.Models;

public struct NumberCount
{
    public NumberCount(int number, int count)
    {
        Number = number;
        Count = count;
    }

    public int Number { get; set; }
    public int Count { get; set; }
}
