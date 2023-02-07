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
    class Log
    {
        public DateTime time { get; set; }
        public string name { get; set; }
        public string fileSource { get; set; }
        public string fileTarget { get; set; }
        public string destPath { get; set; }
        public int fileSize { get; set; }
        public int fileTransferTime { get; set; }

        public Log(
                   string name,
                   string fileSource,
                   string fileTarget,
                   string destPath,
                   int fileSize,
                   int fileTransferTime
             )
        {
            this.time = DateTime.Now;
            this.name = name;
            this.fileSource = fileSource;
            this.fileTarget = fileTarget;
            this.destPath = destPath;
            this.fileSize = fileSize;
            this.fileTransferTime = fileTransferTime;
        }


        public void saveLogInFile()
        {
            DateTime currentDate = DateTime.Now;
            string namefile = currentDate.ToString("MM-dd-yyyy") + ".json";
            string projectPath = Path.GetFullPath(@"../../../");
            string logsFilePath = Path.Combine(projectPath, @"datas/logs/", namefile);

            if (!File.Exists(logsFilePath))
            {
                using (FileStream fs = File.Create(logsFilePath))
                {


                }
            }
            List<Log> jsonObj = JsonConvert.DeserializeObject<List<Log>>(File.ReadAllText(logsFilePath));
            if (jsonObj == null) jsonObj = new List<Log>();

            jsonObj.Add(this);
            File.WriteAllText(logsFilePath, JsonConvert.SerializeObject(jsonObj, Formatting.Indented));

        }
    }
}