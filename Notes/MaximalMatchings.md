# Maximal Matchings

Intuitively, this would seem to be the combination of the possible base pairs multiplied together.

Ok, naive apporach 1

## Defining the total number of possible BP
The Sequence object automatically creates counters. As C-G and A-U for RNA, the total number of matches is limited by the floor of Count(C)-Count(G) and Count(A)-Count(U)
THere is no accounting for steric hinderance/steric forces here. Concretely, this means that AU and A*******U's matching is treated the same way.

I might have done this with a silly mistake.

But let's be formal here. 

Assuming this forumlation is correct, to find the total number of perfect matchings, all we need to do is multiply of the potential bonds together.

```
Max(count(G,C)) P Min(count(G,C)) * Max(count(A,U)) P Min(count(A,U))
```

Why?

Let's take the following:
```
A A A
U U
```
U (0) will have 3 possibilities. U(1) will have 2 possibilities

So 3 * 2

This can be extended to just be the permuations formula

As we have 2 possible matchings, we simply  multiply both by eachother to get all possible matchings.
