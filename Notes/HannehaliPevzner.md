# Reversal Distance Explorations
This is mostly based off of Anne Bergeron's paper


## Motivation
Counting reversals/reversal distance is relatively easy to do with just a brute force approach. As with everything, a simple algorithm might look like the following:

```
    current = Queue(states)
    next = Queue(states)
    i = iteration(depth)
    while q.HasElements
        calculate all n^2 possible reversals
            if current reversal == target
                return i
            next.Add(currentReversal)
        current = next;
        next = new Queue
```

The issue with this problem/approach is that the memory space gets hogged up almost immediately and exhaustively searching.

If you look at the tests written, it's clear that anything past length 8 is trouble some. Why?

Obvious answer, 2^8 is a lot of memory/combinatorial complexity-- especially with the states being measured as full integer arrays. There might be some memory shrinking that could be done-- however, it's still inefficient. I'd rather get a smaller search space.

## Approach 1

I tried playing with some heuristics. This would be a classic A* like approach. So what heuristic could I use?
Per Pevzner and Hannenhali, the breakpoint graph is a good starting point. But what's even simpler than that? Number of elements in the correct position. 

Ok, the easiest way, score each iteration, score it, and then put it into a heap. 

```
    h = Heap(states, heuristic(states))
    t = new HashSet(states)
    h.add(initial)
    var completed = new HashSet(states)
    while q.HasElements
        var current = q.pop();
        calculate all n^2 possible reversals
            if current reversal == target
                return i
            if current not in t
                h.push(currentReversalTarget, heurstic)
```

The heuristic here can matter. All things being equal, this isn't a bad way to get a first guess for the problem

## Approach 2
