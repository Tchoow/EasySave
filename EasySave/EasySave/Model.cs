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
        public int currenLang { get; set; }

        public Model(ViewModel viewModel)
        {
            this.viewModel  = viewModel;
            // default lang is french
            this.currenLang = 1;
        }

        public bool setJob(Job job)
        {
            try
            {
                List<Job> jsonObj = JsonConvert.DeserializeObject<List<Job>>(File.ReadAllText(this.jobFile));
                if (jsonObj == null) jsonObj = new List<Job>();
                jsonObj.Add(job);
                JsonFileUtils.SimpleWrite(jsonObj, this.jobFile);

                return true;
            }
            catch
            {
                return false;
            }

          
        }

        public bool setSave(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true);
                return true;
            }
            catch(IOException err)
            {
                Console.WriteLine(err);
                return false;
            }

        }

        public List<Job> getJobs()
        {
            List<Job> jsonObj = JsonConvert.DeserializeObject<List<Job>>(File.ReadAllText(this.jobFile));
            if (jsonObj == null) jsonObj = new List<Job>();
            return jsonObj;
        }

        public bool deleteJob(int jobIndex)
        {
            try
            {
                // load all jobs
                List<Job> jsonObj = JsonConvert.DeserializeObject<List<Job>>(File.ReadAllText(this.jobFile));
                if (jsonObj == null) jsonObj = new List<Job>();
                // remove job index
                jsonObj.RemoveAt(jobIndex - 1);
                JsonFileUtils.SimpleWrite(jsonObj, this.jobFile);
                return true;
            }
            catch
            {
                return false;
            }
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
