using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning.Preview;

// 1ac4f15e-8725-46e0-9011-89ffda43db1f_9d5a2d70-978a-415d-bf78-321931a57c93

namespace UWP_ONNX
{
    public sealed class _x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93ModelInput
    {
        public VideoFrame data { get; set; }
    }

    public sealed class _x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93ModelOutput
    {
        public IList<string> classLabel { get; set; }
        public IDictionary<string, float> loss { get; set; }
        public _x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93ModelOutput()
        {
            this.classLabel = new List<string>();
            this.loss = new Dictionary<string, float>()
            {
                { "hardshell", float.NaN },
                { "insulated", float.NaN },
            };
        }
    }

    public sealed class _x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93Model
    {
        private LearningModelPreview learningModel;
        public static async Task<_x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93Model> Create_x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93Model(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            _x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93Model model = new _x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93Model();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<_x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93ModelOutput> EvaluateAsync(_x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93ModelInput input) {
            _x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93ModelOutput output = new _x0031_ac4f15e_x002D_8725_x002D_46e0_x002D_9011_x002D_89ffda43db1f_9d5a2d70_x002D_978a_x002D_415d_x002D_bf78_x002D_321931a57c93ModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("data", input.data);
            binding.Bind("classLabel", output.classLabel);
            binding.Bind("loss", output.loss);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
