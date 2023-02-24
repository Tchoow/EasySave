using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EasySave
{
    public class Job : Element
    {
        private string name { get; set; }
        private string sourceFilePath { get; set; }
        private string destinationFilePath { get; set; }
        private int saveType { get; set; }
        private string state { get; set; }
        private int totalFileToCopy { get; set; }
        private long totalFileSize { get; set; }
        private int nbFilesLeftToDo { get; set; }
        private int progression { get; set; }
        private DateTime created { get; set; }
        private string uuid { get; set; }
        private string[] extensionsToEncrypt { get; set; }

        // utils data
        private List<string> lstPriorities;
        private List<string> lstBusinessSoft;
        private long         bigFileLength;
        private string       cryptoPath;

        private ViewModel viewModel { get; set; }

        [JsonConstructor]
        public Job(
            string name,
            string sourceFilePath,
            string destinationFilePath,
            string state,
            int totalFileToCopy,
            int totalFileSize,
            int nbFilesLeftToDo,
            int progression
        ) : base(name, sourceFilePath, destinationFilePath)
        {
            this.name = name;
            this.sourceFilePath = sourceFilePath;
            this.destinationFilePath = destinationFilePath;
            this.state = state;
            this.totalFileToCopy = totalFileToCopy;
            this.totalFileSize = totalFileSize;
            this.nbFilesLeftToDo = nbFilesLeftToDo;
            this.progression = progression;
            this.created = DateTime.Now;
            this.uuid = Guid.NewGuid().ToString();
            this.extensionsToEncrypt = new string[] { "" };
        }

        public Job(
            string name,
            string sourceFilePath,
            string destinationFilePath,
            int saveType,
            string state
            ) : base(name, sourceFilePath, destinationFilePath)
        {
            // User inputs
            this.name = name;
            this.sourceFilePath = sourceFilePath;
            this.destinationFilePath = destinationFilePath;
            this.saveType = saveType;
            this.state = state;

            // Auto
            this.created = DateTime.Now;
            this.uuid    = Guid.NewGuid().ToString();
        }

        public void setVM(ViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public void setPriorities(List<string> newLstPriorities)
        {
            this.lstPriorities = newLstPriorities;
        }

        public void setBusinessSoft(List<string> newLstBusinessSoft)
        {
            this.lstBusinessSoft = newLstBusinessSoft;
        }

        public void setBigFileLength(long fileLength)
        {
            this.bigFileLength = fileLength;
        }

        public void setExtensions(string[] extensions)
        {
            this.extensionsToEncrypt = extensions;
        }

        public void setCryptoSoftPath(string path)
        {
            this.cryptoPath = path;
        }

        private int encryptDecrypt(List<string> pathToFiles)
        {
            int EncryptionTime = 0;

            Parallel.ForEach(pathToFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, fileInfo =>
            {
                if (this.extensionsToEncrypt.Contains(Path.GetExtension(fileInfo)) || this.extensionsToEncrypt[0] == "*")
                {
                    Process p = new Process();
                    p.StartInfo.FileName = this.cryptoPath;
                    p.EnableRaisingEvents = true;
                    p.StartInfo.Arguments = "\"" + fileInfo + "\"" + " " + "\"" + fileInfo + "\"";
                    p.StartInfo.CreateNoWindow = true;
                    p.Exited += new EventHandler((object sender, EventArgs e) => EncryptionTime += p.ExitCode);
                    p.Start();
                    p.WaitForExit();
                }
            });

            return EncryptionTime;
        }



        private string[] getAllFiles(FileAttributes attrSrc)
        {
            string[] files;
            if ((attrSrc & FileAttributes.Directory) == FileAttributes.Directory) //We get all files in the path
            {
                files = Directory.GetFiles(this.SourceFilePath, "*", SearchOption.AllDirectories);
            }
            else
            {
                files = new string[] { this.SourceFilePath };
            }
            return files;
        }

        private List<string> sortFiles(List<string> files)
        {
            // Sort With files extentions
            List<string> sortedList = new List<string>();
            sortedList = files.OrderBy(x => this.lstPriorities.IndexOf(Path.GetExtension(x))).ToList();
            sortedList.Reverse();

            // Sort With files size (big files)
            foreach (string file in files)
            {
                long fileLength = new FileInfo(file).Length;
                if (fileLength >= this.bigFileLength)
                {
                    sortedList.Remove(file);
                    sortedList.Add   (file);
                }
            }
            return sortedList;
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



        public void Execute()
        {

            try
            {
                // Get All Files   //We get the type of file of the destination and the source of the job
                FileAttributes attrDest = File.GetAttributes(this.destinationFilePath);
                FileAttributes attrSrc  = File.GetAttributes(this.sourceFilePath);

                string source;
                if ((attrSrc & FileAttributes.Directory) == FileAttributes.Directory) //We handle the case where the source is a single file
                {
                    source = this.sourceFilePath;
                }
                else
                {
                    source = Path.GetDirectoryName(this.sourceFilePath);
                }

                FileInfo[] dinfos = new DirectoryInfo(source).GetFiles();
                // Reset job infos
                this.NbFilesLeftToDo  = 0;
                this.Progression      = 0;
                this.TotalFileToCopy  = dinfos.Length;
                this.State            = "Running";
                int size              = 0;

                // get all file size
                foreach (FileInfo dinfo in dinfos) size += (int)dinfo.Length;
                this.totalFileSize = size;


                // If folder
                if ((attrDest & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    // Get All files
                    string[] lstFiles  = getAllFiles(attrSrc);

                    var TimestampStart = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                    long filesSize     = 0;
                    DateTime startTime = DateTime.Now;
                    int EncryptionTime = 0;

                    // Sort the Files
                    List<string> sortedFiles = sortFiles(lstFiles.ToList());

                    // Check if business Soft is Running and State is running
                    if (this.State == "Running")
                    {
                        // Encrypt
                        encryptDecrypt(sortedFiles);

                        // Copy Files
                        for (int j = 0; j < sortedFiles.Count; j++)
                        {
                            // Stopped
                            if (this.State == "Stopped")
                            {
                                this.encryptDecrypt(sortedFiles);
                                this.progression = 0;
                                this.viewModel.sendJobObserver(this.name, this.state, this.progression);
                                return;
                            }

                            // If business software we paused
                            if (businessSoftIsRunning())
                            {
                                this.State = "Paused";
                            }
                            

                            // Wait if paused
                            while (this.State == "Paused")
                            {
                                Thread.Sleep(500);
                            }


                            string fileName = sortedFiles[j].Replace(this.SourceFilePath, "");//Path.GetFileName(files[j]);
                            string currentFile = this.DestinationFilePath + fileName;
                            string dipath = Path.GetDirectoryName(currentFile);
                            Directory.CreateDirectory(dipath);
                            int newmodif = 0;
                            if (File.Exists(currentFile) && this.SaveType == 1) // determine the save type
                            {
                                File.Delete(currentFile);
                            }
                            if (File.Exists(currentFile) && this.SaveType == 2)
                            {
                                DateTime modificationFileSrc = File.GetLastWriteTime(sortedFiles[j]);
                                DateTime modificationFileDest = File.GetLastWriteTime(currentFile);
                                newmodif = DateTime.Compare(modificationFileSrc, modificationFileDest);
                            }
                            if (newmodif > 1)
                            {
                                File.Delete(currentFile);
                            }
                            var myFile = File.Create(currentFile);
                            myFile.Close();
                            viewModel.saveFile(sortedFiles[j], currentFile);

                            FileInfo fileInfos = new FileInfo(currentFile);
                            filesSize += fileInfos.Length;
                            this.NbFilesLeftToDo++;
                            this.Progression = this.NbFilesLeftToDo*100 / this.TotalFileToCopy;

                            // Update
                            this.viewModel.sendJobObserver(this.name, this.state, this.progression);
                        }
                        // Decrypt
                        encryptDecrypt(sortedFiles);

                        // Logs
                        DateTime endTime  = DateTime.Now;
                        TimeSpan execTime = endTime - startTime;
                        // Logs
                        Log log = new Log("copy - " + this.Name, this.SourceFilePath + "\\", this.DestinationFilePath + "\\", "", filesSize, (long)execTime.TotalMilliseconds, EncryptionTime);
                        log.saveLogInFile();
                    }

                }
                else // Error
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("La source ou la destination n'est pas un dossier");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            // Finish
            this.state = "Ended";
            this.viewModel.sendJobObserver(this.name, this.state, this.progression);
            Thread.Sleep(500);
            this.Progression = 0;
            this.viewModel.sendJobObserver(this.name, this.state, this.progression);
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string SourceFilePath
        {
            get { return this.sourceFilePath; }
            set { this.sourceFilePath = value; }
        }

        public string DestinationFilePath
        {
            get { return this.destinationFilePath; }
            set { this.destinationFilePath = value; }
        }

        public int SaveType
        {
            get { return this.saveType; }
            set { this.saveType = value; }
        }

        public string State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public int TotalFileToCopy
        {
            get { return this.totalFileToCopy; }
            set { this.totalFileToCopy = value; }
        }

        public long TotalFileSize
        {
            get { return this.totalFileSize; }
            set { this.totalFileSize = value; }
        }

        public int NbFilesLeftToDo
        {
            get { return this.nbFilesLeftToDo; }
            set { this.nbFilesLeftToDo = value; }
        }

        public int Progression
        {
            get { return this.progression; }
            set { this.progression = value; }
        }

        public DateTime Created
        {
            get { return this.created; }
            set { this.created = value; }
        }

        public bool IsSelect { get; set; }
    }
}
