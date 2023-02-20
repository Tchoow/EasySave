using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;

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


        [JsonConstructor]
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


        public string getFormattedJSONLogs(string path)
        {
            Trace.WriteLine(path);
            List<Log> jsonObj = JsonConvert.DeserializeObject<List<Log>>(File.ReadAllText(path));
            return JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        }

        public string getFormattedXMLLogs(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            return XDocument.Parse(xmlDocument.OuterXml).ToString();
        }

        public void saveLogInFile()
        {
            // path creation
            DateTime currentDate = DateTime.Now;
            string namefile = currentDate.ToString("MM-dd-yyyy") + ".json";
            string namefileXML = currentDate.ToString("MM-dd-yyyy") + ".xml";

            string projectPath = Path.GetFullPath(@"../../../../EasySave");
            string logsFilePath = Path.Combine(projectPath, @"datas/logs/", namefile);
            string logsXMLFilePath = Path.Combine(projectPath, @"datas/logs/", namefileXML);

            // test if daily file exist, if not create it
            if (!File.Exists(logsFilePath))
            {
                using (FileStream fs = File.Create(logsFilePath))
                {


                }
            }
            if (!File.Exists(logsXMLFilePath))
            {
                using (FileStream fs = File.Create(logsXMLFilePath))
                {


                }
            }
            // create new list of Log object with json data
            List<Log> jsonObj = JsonConvert.DeserializeObject<List<Log>>(File.ReadAllText(logsFilePath));
            // if jsonobj file is empty create new list of log (to have valid format json)
            if (jsonObj == null) jsonObj = new List<Log>();
            // add currently data log to list
            jsonObj.Add(this);
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement rootElement = xmlDocument.CreateElement("data");
            xmlDocument.AppendChild(rootElement);

            // Ajouter chaque objet du tableau en tant que nœud dans le document XML
            foreach (var dataObject in jsonObj)
            {
                XmlElement dataNode = xmlDocument.CreateElement("item");

                // Parcourir chaque propriété de l'objet et les ajouter en tant qu'attributs du nœud
                var properties = dataObject.GetType().GetProperties();
                foreach (var property in properties)
                {
                    string propertyName = property.Name;
                    object propertyValue = property.GetValue(dataObject);

                    XmlAttribute attribute = xmlDocument.CreateAttribute(propertyName);
                    attribute.Value = propertyValue.ToString();

                    dataNode.Attributes.Append(attribute);
                }

                rootElement.AppendChild(dataNode);
            }
            //serialize and write file with the list of logs
            xmlDocument.Save(logsXMLFilePath);
            File.WriteAllText(logsFilePath, JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented));
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