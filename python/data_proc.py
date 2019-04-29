import glob
import os
import re
import pandas as pd


def load_files(dir_path):
    topics = ["business","entertainment","politics","sport","tech"]
    df = pd.DataFrame(columns=["fname","topic","topic_i","text"])
    c = 0
    for t in topics:
        for file in glob.glob(os.path.join(dir_path, t, "*")):
            id = os.path.splitext(os.path.basename(file))[0]
            with open(file) as txt:
                df.loc[c] = {"fname":id, "topic":t, "topic_i":topics.index(t), "text": re.sub("[^a-zA-Z]"," ", txt.read()) }
            c += 1

    return df
