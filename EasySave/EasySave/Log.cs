using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class Log
    {
        private DateTime          time;
        private string            name;
        private string      fileSource;
        private string      fileTarget;
        private string        destPath;
        private int           fileSize;
        private int   fileTransferTime;

        public Log(string name,
                   string fileSource,
                   string fileTarget,
                   string destPath,
                   int fileSize,
                   int fileTransferTime)
        {
            this.time = new DateTime();
            this.name = name;
            this.fileSource = fileSource;
            this.fileTarget = fileTarget;
            this.destPath   = destPath;
            this.fileSize   = fileSize;
            this.fileTransferTime = fileTransferTime;
        }

        public bool storeLogInFile()
        {
            // if the file exist
            // if not create one
            // if yes fill eat with infos
            // return true

            return false;
        }
        

    }
}
