using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MldllNlp.DataModel;

namespace MldllNlp.helper
{
    public class DataLoader
    {
        public static List<NewsItem> LoadData(string path)
        {
            string[] topics = new string[5] { "business", "entertainment", "politics", "sport", "tech" };

            List<NewsItem> items = new List<NewsItem>();

            foreach (string t in topics)
            {
                string[] files = Directory.GetFiles($@"{path}\{t}");
                foreach (string f in files)
                {
                    items.Add(new NewsItem
                    {
                        Text = File.OpenText(f).ReadToEnd(),
                        NewsTopic = t
                    });
                }
            }

            return items;
            
        }
    }
}
