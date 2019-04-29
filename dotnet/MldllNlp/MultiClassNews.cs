using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using MldllNlp.DataModel;
using static Microsoft.ML.DataOperationsCatalog;

namespace MldllNlp
{
    public class MultiClassNews
    {
        private MLContext mlContext;        
        private TrainTestData trainTestData;

        public MultiClassNews(List<NewsItem> news)
        {            
            mlContext = new MLContext(seed: 7);
            loadDataView(news);
        }

        private void loadDataView(List<NewsItem> news)
        {            
            var dv = mlContext.Data.LoadFromEnumerable<NewsItem>(news);
            trainTestData = mlContext.Data.TrainTestSplit(dv, testFraction: 0.3);
        }

        public ITransformer TrainModel()
        {
            var dataPipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: nameof(NewsItem.NewsTopic))
                 .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(NewsItem.Text)))
                 .AppendCacheCheckpoint(mlContext);

            var trainPipeline = dataPipeline.Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
            var trainedModel = trainPipeline.Fit(trainTestData.TrainSet);

            return trainedModel;
        }

        public void TestModel(ITransformer trainedModel)
        {
            var predictions = trainedModel.Transform(trainTestData.TestSet);
            var metrics = mlContext.MulticlassClassification.Evaluate(predictions, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine($"MACRO ACCURACY: {metrics.MacroAccuracy}");
            Console.WriteLine($"MICRO ACCURACY: {metrics.MicroAccuracy}");
            Console.WriteLine($"LOSS: {metrics.LogLoss}");
        }
    }
}
