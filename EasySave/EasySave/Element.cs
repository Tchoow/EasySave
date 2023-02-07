using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    abstract class Element
    {
        private string name;
        private string sourcePath;
        private string targetPath;
        private DateTime created;

        public Element(string name, string sourcePath, string targetPath)
        {
            this.name       = name;
            this.sourcePath = sourcePath;
            this.targetPath = targetPath;
            this.created    = DateTime.Now;
        }
    }
}
