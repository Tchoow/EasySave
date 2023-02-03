using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EasySave
{
    class Model
    {
        private string jobFile = @"../../../datas/saves/jobs.json";
        private ViewModel viewModel { get; set; }
        private string    language { get; set; }
        public Model(ViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.language  = "";
        }

        public void setJob(Job job)
        {
            Console.WriteLine(JsonConvert.DeserializeObject(File.ReadAllText(this.jobFile)));
            List<Job> jsonObj = JsonConvert.DeserializeObject<List<Job>>(File.ReadAllText(this.jobFile));
            if (jsonObj == null) jsonObj = new List<Job>(); 
            jsonObj.Add(job);
            JsonFileUtils.SimpleWrite(jsonObj, this.jobFile);
        }

        public List<Job> getJobs()
        {
            List<Job> jsonObj = JsonConvert.DeserializeObject<List<Job>>(File.ReadAllText(this.jobFile));
            if (jsonObj == null) jsonObj = new List<Job>();
            return jsonObj;
        }

        public static class JsonFileUtils
        {
            private static readonly JsonSerializerSettings _options = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };
            public static void SimpleWrite(object obj, string fileName)
            {
                var jsonString = JsonConvert.SerializeObject(obj, _options);
                File.WriteAllText(fileName, jsonString);
            }
        } 

    }
}
