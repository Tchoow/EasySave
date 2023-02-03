using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.Json;
using Json.Net;

namespace EasySave
{
    class Log
    {
        private DateTime _time;
        private string _name;
        private string _fileSource;
        private string _fileTarget;
        private string _destPath;
        private int _fileSize;
        private int _fileTransferTime;


        public DateTime time
        {
            get { return _time; }
            set { _time = value; }
        }

        public  string name
        {
            get { return _name; }
            set { _name = value; }
        }


        public string fileSource
        {
            get { return _fileSource; }
            set { _fileSource = value; }
        }

     

        public string fileTarget
        {
            get { return _fileTarget; }
            set { _fileTarget = value; }
        }


       

        public string destPath
        {
            get { return _destPath; }
            set { _destPath = value; }
        }
       

        public int fileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }

        public int fileTransferTime
        {
            get { return _fileTransferTime; }
            set { _fileTransferTime = value; }
        }

        

        public Log(
                   string name,
                   string fileSource,
                   string fileTarget,
                   string destPath,
                   int fileSize,
                   int fileTransferTime
             )
        {
            this._time = DateTime.Now;
            this._name = name;
            this._fileSource = fileSource;
            this._fileTarget = fileTarget;
            this._destPath   = destPath;
            this._fileSize   = fileSize;
            this._fileTransferTime = fileTransferTime;
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









            //string jsonFormated = JsonConvert.SerializeObject(this, Formatting.Indented);
            //File.AppendAllText(logsFilePath, jsonFormated);




        }


    }
}
