from Bio import Entrez
from Bio import SeqIO
from Bio.SeqRecord import SeqRecord
Entrez.email = "your_name@your_mail_server.com"
handle = Entrez.efetch(db="nucleotide", id=["JX475048","BT149870","JX308817","JX569368","BT149867","JX460804","JQ762396","NM_131329","NM_002037","JF927165"], rettype="fasta")
records = list (SeqIO.parse(handle, "fasta")) #we get the list of SeqIO objects in FASTA format
current = 0
records.sort(key=lambda record: len(record.seq))
for record in records:
    print(len(record))

record1 = SeqRecord(
    records[0].seq,
    id=records[0].id,
    description=records[0].description
)

output_filename = "output.fasta"
with open(output_filename, "w") as output_handle:
    SeqIO.write(record1, output_handle, "fasta")