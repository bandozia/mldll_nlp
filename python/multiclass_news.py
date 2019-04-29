import pandas as pd
import numpy as np
from sklearn.svm import SVC
from sklearn.model_selection import train_test_split
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.metrics import classification_report
from gensim.utils import tokenize
from data_proc import load_files

df = load_files("../data/bbc")
X_train, X_test, y_train, y_test = train_test_split(df["text"], df["topic"], test_size=0.3)

vec = CountVectorizer()
vec.fit(X_train)
X_features = vec.transform(X_train)
X_test_features = vec.transform(X_test)

svm_model = SVC(C=1.0, kernel='linear')
svm_model.fit(X_features, y_train)

accuracy = svm_model.score(X_test_features, y_test)
print(classification_report(y_test, pred))
