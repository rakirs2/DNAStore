from Bio.Seq import translate

def find_possible_genetic_coding_table(dna_seq, protein_seq):
    # https://www.ncbi.nlm.nih.gov/Taxonomy/Utils/wprintgc.cgi
    table_ids = 33 
    
    for table_id in range(1, table_ids+1):
        try:
            # if there's no exception, we assume it transalte, can return.
            translated = translate(dna_seq, table=table_id, to_stop=True)
            if translated == protein_seq:
                return table_id
        except:
            continue
    return None

# Really fascinating bit here. One of those beautiful things in Biology-- things are the same
# but they're not

## too large

file_path = input("filePath")
with open(file_path, 'r', encoding='utf-8') as file:
        lines = file.readlines()
        dna = lines[0].strip()
        prt = lines[1].strip()
        print(find_possible_genetic_coding_table(dna, prt))
    