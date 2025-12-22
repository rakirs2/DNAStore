from Bio import SeqIO

path = input("please input path to file")
count = 0
with open(path, "r") as f:
    threshold = int(f.readline())
    qualities = []
    # continue passing in stream here
    for req in SeqIO.parse(f, "fastq"):
        #same idea as phre
        quality = req.letter_annotations["phred_quality"]
        qualities.append(quality)
for i in range(len(qualities[0])):
    if sum([q[i] for q in qualities])/len(qualities) < threshold:
        count += 1

print(count)

