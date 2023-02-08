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
        private int currentLang { get; set; }
        private Translate translate;

        public Model(ViewModel viewModel)
        {

            this.viewModel = viewModel;
            // default lang is french
            this.translate = new Translate();
            this.currentLang = 1;
        }


        public bool setJob(Job job)
        {
            try
            {
                // The creation of the job works
                List<Job> jsonObj = JsonConvert.DeserializeObject<List<Job>>(File.ReadAllText(this.jobFile));
                if (jsonObj == null) jsonObj = new List<Job>();
                jsonObj.Add(job);
                JsonFileUtils.SimpleWrite(jsonObj, this.jobFile);

                return true;
            }
            catch
            {
                // Error in the process
                return false;
            }
        }

        public int getLanguageIndex() { return this.currentLang; }
        public void setLanguageIndex(int indexLang) { this.currentLang = indexLang; }
        public string getTraduction(string key) { return this.translate.getTraduction(this.currentLang, key); }
        public List<string> getLstLanguages() { return this.translate.getLstLanguages(); }

        public bool setSave(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true);
                return true;
            }
            catch (IOException err)
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

        public List<string> getLogs()
        {
            string folderPath = "../../../datas/logs/";
            string[] files = Directory.GetFiles(folderPath);
            List<string> lstLogs = new List<string>();

            foreach (string file in files)
            {
                if (file.EndsWith(".json"))
                {
                    String fileEdit = new string(file);
                    fileEdit = fileEdit.Replace("../../../datas/logs/", "");
                    fileEdit = fileEdit.Replace(".json", "");
                    lstLogs.Add(fileEdit);
                }
            }

            return lstLogs;
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


        public bool executeJobs(List<Job> jobs)
        {
            try
            {
                // The execution of the job works
                for (int i = 0; i < jobs.Count; i++)
                {
                    FileAttributes attrDest = File.GetAttributes(jobs[i].DestinationFilePath);
                    FileAttributes attrSrc = File.GetAttributes(jobs[i].SourceFilePath);
                    if ((attrDest & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        string[] files; 
                        if((attrSrc & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            files = Directory.GetFiles(jobs[i].SourceFilePath);
                        }
                        else
                        {
                            files = new string[] { jobs[i].SourceFilePath };
                        }
                        var TimestampStart = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                        long filesSize = 0;
                        DateTime startTime = DateTime.Now;

                        for (int j = 0; j < files.Length; j++)
                        {
                            string fileName = Path.GetFileName(files[j]);
                            if(File.Exists(jobs[i].DestinationFilePath + "\\" + fileName) && jobs[i].SaveType == 1) 
                            {
                                File.Delete(jobs[i].DestinationFilePath + "\\" + fileName);
                            }
                            var myFile = File.Create(jobs[i].DestinationFilePath + "\\" + fileName);
                            myFile.Close();
                            viewModel.saveFile(files[j], jobs[i].DestinationFilePath + "\\" + fileName);

                            FileInfo fileInfos = new FileInfo(jobs[i].DestinationFilePath + "\\" + fileName);
                            filesSize += fileInfos.Length;

                        }
                        DateTime endTime = DateTime.Now;
                        TimeSpan execTime = endTime - startTime;

                        // Logs
                        Log log = new Log("copy - " + jobs[i].Name, jobs[i].SourceFilePath + "\\", jobs[i].SourceFilePath + "\\", "", filesSize, (long)execTime.TotalMilliseconds);
                        log.saveLogInFile();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("La source ou la destination n'est pas un dossier");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Error in the process
                return false;
            }
        }

    }
}
