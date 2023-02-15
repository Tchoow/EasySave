using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace EasySave
{
    public class Log : Element
    {
        private DateTime time { get; set; }
        private string name { get; set; }
        private string fileSource { get; set; }
        private string fileTarget { get; set; }
        private string destPath { get; set; }
        private long fileSize { get; set; }
        private long fileTransferTime { get; set; }
        public int encrpytionTime;

        public Log(
                   string name,
                   string fileSource,
                   string fileTarget,
                   string destPath,
                   long fileSize,
                   long fileTransferTime,
                   int encrpytionTime
            ) : base ( name, fileSource, fileTarget)
        {
            this.time = DateTime.Now;
            this.name = name;
            this.fileSource = fileSource;
            this.fileTarget = fileTarget;
            this.destPath = destPath;
            this.fileSize = fileSize;
            this.fileTransferTime = fileTransferTime;
            this.encrpytionTime = encrpytionTime;
        }


        public void saveLogInFile()
        {
            // path creation
            DateTime currentDate = DateTime.Now;
            string namefile = currentDate.ToString("MM-dd-yyyy") + ".json";
            string projectPath = Path.GetFullPath(@"../../../");
            string logsFilePath = Path.Combine(projectPath, @"datas/logs/", namefile);
            // test if daily file exist, if not create it
            if (!File.Exists(logsFilePath))
            {
                using (FileStream fs = File.Create(logsFilePath))
                {


                }
            }
            // create new list of Log object with json data
            List<Log> jsonObj = JsonConvert.DeserializeObject<List<Log>>(File.ReadAllText(logsFilePath));
            // if jsonobj file is empty create new list of log (to have valid format json)
            if (jsonObj == null) jsonObj = new List<Log>();
            // add currently data log to list
            jsonObj.Add(this);
            //serialize and write file with the list of logs
            File.WriteAllText(logsFilePath, JsonConvert.SerializeObject(jsonObj, Formatting.Indented));
        }

        public DateTime Time { get => time; set => time = value; }
        public string Name { get => name; set => name = value; }
        public string FileSource { get => fileSource; set => fileSource = value; }
        public string FileTarget { get => fileTarget; set => fileTarget = value; }
        public string DestPath { get => destPath; set => destPath = value; }
        public long FileSize { get => fileSize; set => fileSize = value; }
        public long FileTransferTime { get => fileTransferTime; set => fileTransferTime = value; }
    }
}