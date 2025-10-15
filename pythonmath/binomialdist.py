from scipy.special import binom

def probability(n, k):
    return binom(2**k, n) * 0.25**n * 0.75**(2**k - n)

def sum_of_probabilities(k, N):
     return 1 - sum(probability(n, k) for n in range(N))

print(round(sum_of_probabilities(7, 37), 3)) 