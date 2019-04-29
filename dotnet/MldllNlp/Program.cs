using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.ML;
using MldllNlp.DataModel;
using MldllNlp.helper;

namespace MldllNlp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("carregando arquivos...");
            Stopwatch watch = Stopwatch.StartNew();
            List<NewsItem> newsList = DataLoader.LoadData(@"../../../../../data/bbc");
            watch.Stop();
            PrintTimeElapsed(watch);

            Console.WriteLine("carregando dados no modelo...");
            watch = Stopwatch.StartNew();
            MultiClassNews multiClass = new MultiClassNews(newsList);
            watch.Stop();
            PrintTimeElapsed(watch);

            Console.WriteLine("treinando o modelo...");
            watch = Stopwatch.StartNew();
            var trainedModel = multiClass.TrainModel();
            watch.Stop();
            PrintTimeElapsed(watch);
                        
            multiClass.TestModel(trainedModel);            
        }

        static void PrintTimeElapsed(Stopwatch warch)
        {
            long mills = warch.ElapsedMilliseconds;
            if (mills > 3000)
            {
                Console.WriteLine($"======CONCLUIDO. [{mills / 1000}s]=======");
            }
            else
            {
                Console.WriteLine($"======CONCLUIDO. [{mills}ms]=======");
            }
        }
                
    }
}
