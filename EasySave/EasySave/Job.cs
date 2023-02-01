using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class Job
    {
        public string name { get; set; }
        public string sourceFilePath { get; set; }
        public string destinationFilePath { get; set; }
        public string state {get; set; }
        public int totalFileToCopy { get; set; }
        public int totalFileSize { get; set; }
        public int nbFilesLeftToDo { get; set; }
        public int progression { get; set; }
        
        public Job(
            string name,
            string sourceFilePath,
            string destinationFilePath,
            string state,
            int totalFileToCopy,
            int totalFileSize,
            int nbFilesLeftToDo,
            int progression
        )
        {
            this.name = name;
            this.sourceFilePath = sourceFilePath;
            this.destinationFilePath = destinationFilePath;
            this.state = state;
            this.totalFileToCopy = totalFileToCopy;
            this.totalFileSize = totalFileSize;
            this.nbFilesLeftToDo = nbFilesLeftToDo;
            this.progression = progression;
        }
    }
}
