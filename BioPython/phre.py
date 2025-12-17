from Bio import SeqIO

# problem asks for threshold at top of file
# How do these distributions normally even look?
def count_low_quality_reads(file_path, threshold):
    low_quality_count = 0
    for record in SeqIO.parse(file_path, "fastq"):
        # given in problem
        # TODO: note, phred is an amazing wiki read
        scores = record.letter_annotations["phred_quality"]
        avg_score = sum(scores) / len(scores)
        
        if avg_score < threshold:
            low_quality_count += 1
            
    return low_quality_count

result = count_low_quality_reads("/Users/rakirs/Downloads/rosalind_phre.txt", 21)
print(result)
