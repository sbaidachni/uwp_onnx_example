using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning.Preview;

namespace UWP_ONNX
{
    public sealed class JacketModelInput
    {
        public VideoFrame data { get; set; }
    }

    public sealed class JacketModelOutput
    {
        public IList<string> classLabel { get; set; }
        public IDictionary<string, float> loss { get; set; }
        public JacketModelOutput()
        {
            this.classLabel = new List<string>();
            this.loss = new Dictionary<string, float>()
            {
                { "hardshell", float.NaN },
                { "insulated", float.NaN },
            };
        }
    }

    public sealed class JacketModel
    {
        private LearningModelPreview learningModel;
        public static async Task<JacketModel> CreateModel(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            JacketModel model = new JacketModel();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<JacketModelOutput> EvaluateAsync(JacketModelInput input) {
            JacketModelOutput output = new JacketModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("data", input.data);
            binding.Bind("classLabel", output.classLabel);
            binding.Bind("loss", output.loss);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
