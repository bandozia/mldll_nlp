from data_proc import load_files
import pandas as pd
from gensim.utils import tokenize
from gensim.models import LdaModel
from gensim.corpora.dictionary import Dictionary

df = load_files("../data/bbc")
df["text"] = [list(tokenize(t)) for t in df.text]
dic = Dictionary(df.text)
corpus = [dic.doc2bow(w) for w in df.text]
lda = LdaModel(corpus, num_topics=5)

df["lda_result"] = [lda[x] for x in corpus]
