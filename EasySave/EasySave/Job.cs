using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EasySave
{
    class Job : Element
    {
        private string name { get; set; }
        private string sourceFilePath { get; set; }
        private string destinationFilePath { get; set; }
        private int saveType { get; set; }
        private string state { get; set; }
        private int totalFileToCopy { get; set; }
        private int totalFileSize { get; set; }
        private int nbFilesLeftToDo { get; set; }
        private int progression { get; set; }
        private DateTime created { get; set; }
        private string uuid { get; set; }


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
            this.uuid = Guid.NewGuid().ToString();
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

        public int TotalFileSize
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
    }
}
