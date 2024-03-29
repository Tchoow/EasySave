﻿using System;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using System.Net.Sockets;

namespace EasySave
{
    public class Model
    {
        private string jobFile = @"../../../../EasySave/datas/saves/jobs.json";
        private ViewModel viewModel { get; set; }
        private int currentLang { get; set; }
        private Translate translate;

        private List<string> lstPriorities;
        private List<string> lstBusinessSoft;
        private string cryptoSoftPath;
        private int maxFileSizeSim;
        private Dictionary<Thread, Job> lstThreadJobs;
        private Server server;

        public Model(ViewModel viewModel)
        {
            // VM
            this.viewModel = viewModel;

            // default lang is french
            this.translate       = new Translate();
            this.currentLang     = 1;

            // Configs
            this.lstPriorities   = new List<string>();
            this.lstBusinessSoft = new List<string>();
            this.cryptoSoftPath  = "../../../../EasySave/CryptoSoft/CryptoSoft.exe";
            this.maxFileSizeSim  = 0;
            this.server = new Server();
            this.lstThreadJobs   = new Dictionary<Thread, Job>();

        }   


        // Config
        public List<string> LstPriorities
        {
            get { return this.lstPriorities;  }
            set { this.lstPriorities = value; }
        }

        public string CryptoSoftPath
        {
            get { return this.cryptoSoftPath;  }
            set { this.cryptoSoftPath = value; }
        }

        public int MaxFileSizeSim
        {
            get { return this.maxFileSizeSim; }
            set { this.maxFileSizeSim = value; }
        }

        public List<string> LstBusinessSoft
        {
            get { return this.lstBusinessSoft;  }
            set { this.lstBusinessSoft = value; }
        }

        public bool setJob(Job job)
        {
            try
            {
                // The creation of the job works
                List<Job> jsonObj = JsonConvert.DeserializeObject<List<Job>>(File.ReadAllText(this.jobFile));
                if (jsonObj == null) jsonObj = new List<Job>();
                int size = 0;
                FileInfo[] infos = new DirectoryInfo(job.SourceFilePath).GetFiles();
                job.TotalFileToCopy = infos.Length;
                foreach(FileInfo info in infos) //We sum up the size of all files in the folder
                {
                    size += (int)info.Length;
                }
                job.TotalFileSize = size;
                job.State = "Not started";
                jsonObj.Add(job); //We add a job to the list that will be written in the json file
                SimpleWrite(jsonObj, this.jobFile);
                return true;
            }
            catch
            {
                // Error in the process
                return false;
            }
        }

        public bool setJobByIndex(Job job, int index) //set a job in the file at a specified index 
        {
            try
            {
                List<Job> currentJobs = this.getJobs();
                int size = 0;
                FileInfo[] infos = new DirectoryInfo(job.SourceFilePath).GetFiles();
                job.TotalFileToCopy = infos.Length;
                foreach (FileInfo info in infos) //We sum up the size of all files in the folder
                {
                    size += (int)info.Length;
                }
                job.TotalFileSize = size;
                currentJobs[index] = job;
                SimpleWrite(currentJobs, jobFile);
                return true;
            }
            catch
            {
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
            string folderPath = "../../../../EasySave/datas/logs/";
            string[] files = Directory.GetFiles(folderPath);
            List<string> lstLogs = new List<string>();

            foreach (string file in files)
            {
                if (file.EndsWith(".json"))
                {
                    String fileEdit = new string(file);
                    fileEdit = fileEdit.Replace("../../../../EasySave/datas/logs/", ""); //as the .exe is in the bin file we have to "climb up" the path
                    fileEdit = fileEdit.Replace(".json", "");
                    lstLogs.Add(fileEdit);
                }
            }

            return lstLogs;
        }


        public FileInfo[] getLogsFiles()
        {
            List<string> lstLogsFiles = new List<string>();
            string folderPath         = "../../../../EasySave/datas/logs/";
            FileInfo[] dinfos         = new DirectoryInfo(folderPath).GetFiles().Where(file => !file.Name.EndsWith(".xml")).ToArray();
            return dinfos;
        }

        public List<Log> getLogsLst(string fileName)
        {
            string folderPath = "../../../../EasySave/datas/logs/";
            List<Log> lstLogs = new List<Log>();
            if (Path.GetExtension(fileName) == ".xaml") return new List<Log>();
            lstLogs = JsonConvert.DeserializeObject<List<Log>>(File.ReadAllText(folderPath + fileName));

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
                SimpleWrite(jsonObj, this.jobFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static readonly JsonSerializerSettings _options = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };
        public static void SimpleWrite(object obj, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(obj, _options);
            File.WriteAllText(fileName, jsonString);
        }


        // Progress Bar CLI
        public void updateProgressBar(int progress)
        {
            Console.Clear();
            Console.Write("[ ");
            int space = 100 - progress; 
           
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < progress; i++)
            {
                Console.Write("#");
            }
            for (int i = 0; i < space; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" ] ");
            Console.Write(progress + " %");
            Console.WriteLine();
        }

        public bool executeJobs(List<Job> jobs, string[] extensions)
        {
            try
            {
                // We create one Thread per Job
                for (int i = 0; i < jobs.Count; i++)
                {
                    Thread newJob = new Thread(new ThreadStart(jobs[i].Execute));

                    // Set infos
                    jobs[i].setVM(this.viewModel);
                    jobs[i].setPriorities(this.lstPriorities);
                    jobs[i].setBusinessSoft(this.lstBusinessSoft);
                    jobs[i].setBigFileLength(this.maxFileSizeSim);
                    jobs[i].setCryptoSoftPath(this.cryptoSoftPath);
                    jobs[i].setExtensions(extensions);

                    // Start
                    newJob.Start();
                    this.lstThreadJobs.Add(newJob, jobs[i]);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Error in the process
                return false;
            }
            return true;
        }

        public bool pauseJobs(List<Job> jobs)
        {
            if (jobs == null) return false;

            foreach(Job j in jobs)
            {
                foreach (KeyValuePair<Thread, Job> JobThread in this.lstThreadJobs)
                {
                    if (j.Name.Equals(JobThread.Value.Name))
                    {
                        if (JobThread.Value.State == "Paused")
                            JobThread.Value.State = "Running";
                        else
                            JobThread.Value.State = "Paused";
                    }
                }
            }

            return true;
        }

        public bool stopJobs(List<Job> jobs)
        {
            if (jobs == null) return false;

            foreach (Job j in jobs)
            {
                foreach (KeyValuePair<Thread, Job> JobThread in this.lstThreadJobs)
                {
                    if (j.Name.Equals(JobThread.Value.Name))
                    {
                        JobThread.Value.State = "Stopped";
                        this.lstThreadJobs.Remove(JobThread.Key);
                    }
                }
            }
            return true;
        }
        public void RunServer()
        {
            Socket servsocket = server.Initialize();
            Socket accepted = server.AcceptConnexion(servsocket);
            server.serverSocket = accepted;
        }
        public (string result, List<Job> jobs) ServerListen()
        {
            (string res, List<Job> jobs) = server.ListenNetwork(getJobs());
            return (res, jobs);
        }
        public void SendJobs(string name, string state, int progression) { server.SendJobs(name, state, progression); }

       public void CloseSocket() { server.CloseSocket(); }
    }
}
