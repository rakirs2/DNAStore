# Catalan Numbers and RNA Secondary Structures
NB: for later -- is there a good graphing software for this. This is clumsy as heck.

The hint is to do this dynamically. 

The string has the same numbers of AU and GC

The length is a maxinum of 300

Let's start off with a basic graph:

```
ACUG
```

This has 0 perfect matchings

We can add a few possible sequeences-- an 'ACUG' again, 'GUCA'

```
  C  U
A      G
A      G
  C  U
```

This is still has 0 matchings

```
  C  U
A      G
G      A
  U  C
```

So if we have a section where the n-1^th node doesn't immediately match with the the nth or n-2nd element, that causes a crossing. So ACUG is a classic example. The C is locked in unless there's a perfect matching which asllows the U and A to map to. For example:

```
  C  U
A      G
U      C
  G  A
```

So what is it that takes going from ACUG --> ACUGCAGU from 0-->1 matching?


A better formulation would be that there exists some dividing edge for two valid base pairs. A perfect matching for that subgraph may or may not exist. The requirements for a potential perfect mapping of the subgraph are the same as the requirements for the potential perfect. 

An empty string has a perfect matching of 1. 

As this is an RNA sequence, all possible matchings (to cover every possible divider, starts at the first nucleotide (index 0) and maps to, potenitally, every other nucleotide at the 'odd indices.'
