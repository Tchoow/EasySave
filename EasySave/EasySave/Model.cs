using System;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;

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
            this.cryptoSoftPath = "../../../../EasySave/CryptoSoft/CryptoSoft.exe";
            this.maxFileSizeSim  = 0;
        }

        private bool businessSoftIsRunning()
        {
            // Parcourt la liste des programmes d'entreprise
            foreach (string businessSoftName in this.lstBusinessSoft)
            {
                // Recherche un processus avec un nom similaire
                string softName = Path.GetFileNameWithoutExtension(businessSoftName);
                Process[] processes = Process.GetProcessesByName(softName);
                if (processes.Length > 0)
                {
                    // Le processus a été trouvé
                    return true;
                }
            }
            // Aucun processus trouvé pour les programmes d'entreprise
            return false;
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
                job.State = "Paused";
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

            if ( !businessSoftIsRunning() )
            {

                try
                {
                    // The execution of the job works
                    for (int i = 0; i < jobs.Count; i++)
                    {
                        Job newJob = jobs[i]; //This will be used for rewriting the jobs file
                                              //We get the type of file of the destination and the source of the job
                        FileAttributes attrDest = File.GetAttributes(jobs[i].DestinationFilePath);
                        FileAttributes attrSrc = File.GetAttributes(jobs[i].SourceFilePath);
                        string source;
                        if ((attrSrc & FileAttributes.Directory) == FileAttributes.Directory) //We handle the case where the source is a single file
                        {
                            source = jobs[i].SourceFilePath;
                        }
                        else
                        {
                            source = Path.GetDirectoryName(jobs[i].SourceFilePath);
                        }
                        FileInfo[] dinfos = new DirectoryInfo(source).GetFiles();
                        newJob.NbFilesLeftToDo = 0;
                        newJob.Progression = 0;
                        newJob.TotalFileToCopy = dinfos.Length;
                        newJob.State = "Running";
                        int size = 0;


                        foreach (FileInfo dinfo in dinfos)
                        {
                            size += (int)dinfo.Length;
                        }
                        newJob.TotalFileSize = size;
                        if ((attrDest & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            string[] files;

                            if ((attrSrc & FileAttributes.Directory) == FileAttributes.Directory) //We get all files in the path
                            {

                                files = Directory.GetFiles(jobs[i].SourceFilePath, "*", SearchOption.AllDirectories);

                            }
                            else
                            {
                                files = new string[] { jobs[i].SourceFilePath };
                            }
                            var TimestampStart = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                            long filesSize = 0;
                            DateTime startTime = DateTime.Now;

                            int EncryptionTime = 0;

                            Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, fileInfo =>
                            {
                                if (extensions.Contains(Path.GetExtension(fileInfo)) || extensions[0] == "")
                                {
                                    Process p = new Process();
                                    p.StartInfo.FileName = cryptoSoftPath;
                                    p.EnableRaisingEvents = true;
                                    p.StartInfo.Arguments = "\"" + fileInfo + "\"" + " " + "\"" + fileInfo + "\"";
                                    p.StartInfo.CreateNoWindow = true;
                                    p.Exited += new EventHandler((object sender, EventArgs e) => EncryptionTime += p.ExitCode);
                                    p.Start();
                                    p.WaitForExit();
                                }
                            });
                            Console.WriteLine(EncryptionTime);

                            for (int j = 0; j < files.Length; j++)
                            {

                                string fileName = files[j].Replace(jobs[i].SourceFilePath, "");//Path.GetFileName(files[j]);
                                string currentFile = jobs[i].DestinationFilePath + fileName;
                                string dipath = Path.GetDirectoryName(currentFile);
                                Directory.CreateDirectory(dipath);
                                int newmodif = 0;
                                if (File.Exists(currentFile) && jobs[i].SaveType == 1) // determine the save type
                                {
                                    File.Delete(currentFile);
                                }
                                if (File.Exists(currentFile) && jobs[i].SaveType == 2)
                                {
                                    DateTime modificationFileSrc = File.GetLastWriteTime(files[j]);
                                    DateTime modificationFileDest = File.GetLastWriteTime(currentFile);
                                    newmodif = DateTime.Compare(modificationFileSrc, modificationFileDest);
                                }
                                if (newmodif > 1)
                                {
                                    File.Delete(currentFile);
                                }
                                //var myfile = Directory.CreateDirectory(currentFile);
                                var myFile = File.Create(currentFile);
                                myFile.Close();
                                viewModel.saveFile(files[j], currentFile);

                                FileInfo fileInfos = new FileInfo(currentFile);
                                filesSize += fileInfos.Length;
                                newJob.NbFilesLeftToDo++;
                                // newJob.Progression = (newJob.NbFilesLeftToDo*100 / newJob.TotalFileToCopy);
                                //updateProgressBar(newJob.Progression);

                            }
                            Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, fileInfo =>
                            {
                                if (extensions.Contains(Path.GetExtension(fileInfo)) || extensions[0] == "")
                                {
                                    Process p = new Process();
                                    p.StartInfo.FileName = cryptoSoftPath;
                                    p.EnableRaisingEvents = true;
                                    p.StartInfo.Arguments = "\"" + fileInfo + "\"" + " " + "\"" + fileInfo + "\"";
                                    p.StartInfo.CreateNoWindow = true;
                                    p.Exited += new EventHandler((object sender, EventArgs e) => EncryptionTime += p.ExitCode);
                                    p.Start();
                                    p.WaitForExit();
                                }
                            });
                            DateTime endTime = DateTime.Now;
                            TimeSpan execTime = endTime - startTime;
                            // Logs
                            Log log = new Log("copy - " + jobs[i].Name, jobs[i].SourceFilePath + "\\", jobs[i].DestinationFilePath + "\\", "", filesSize, (long)execTime.TotalMilliseconds, EncryptionTime);
                            log.saveLogInFile();
                            try
                            {
                                List<Job> currentJobs = this.getJobs();
                                int index = currentJobs.IndexOf(currentJobs.Find(e => e.Name == newJob.Name));
                                if (i == jobs.Count - 1) newJob.State = "Ended";
                                this.setJobByIndex(newJob, index);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("La source ou la destination n'est pas un dossier");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
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
            else
            {
                return false;
            }
        }

    }
}
