# Sequence Encoder

This is a small project to demonstrate a very simple method for encoding a sequence that has a high degree of repeating numbers.

## Approach

We can eliminate the repetition by pairing a given number with a count of how many times it repeats.

The effectiveness of this approach increases/decreases with the density of repeated numbers in the sequence.

I simply group the sequences into pairs, which are implicitly the given number and how many times the given number repeats.

I used some custom classes for readability, since compute is not a significant concern. It's still performant, but if the problem suggested there was a constraint on time/compute then I would have taken a terser approach.

## Test Example

```javascript
{ 
    data: [2,2,2,5,5,5,5,9,9,9,9,...9] 
}
```
In the above example, assume data has a length of 2000 elements.

The first 3 elements contain the value 2. The next 4 elements contain the value 5. The remaining 1993 elements contain the value 9.

### Test Example Result

The unencoded sequence length is 4001. (`[2,2,2,5,5,5,5,9,9,9,9,...9]`)

The encoded sequence length is 22. (`[[2,3],[5,4],[9,1993]]`)
