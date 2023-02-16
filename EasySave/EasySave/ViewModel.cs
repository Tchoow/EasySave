﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace EasySave
{
    public class ViewModel
    {
        private dynamic view           { get; set; }
        private Model model         { get; set; }
        public List<Log> lstLogs    { get; set; }

        public ViewModel(dynamic view)
        {
            this.view    = view;
            this.model   = new Model(this);
            this.lstLogs = new List<Log>();
        }

        // Traductions
        public void setLangueIndex(int indexLang) { this.model.setLanguageIndex(indexLang); }
        public int  getLanguageIndex() { return this.model.getLanguageIndex(); }
        public string getTraduction(string key) { return this.model.getTraduction(key);  }
        public List<string> getLstLanguages() { return this.model.getLstLanguages();  }

        // Jobs
        public bool addNewJob(Job newJob) { return this.model.setJob(newJob);  }
        public List<Job> getJobsList()    {return this.model.getJobs(); }

        public bool deleteJobWithIndex(int jobIndex) { return this.model.deleteJob(jobIndex); }
        public void updateJob(Job job,int index) { this.model.setJobByIndex(job, index); }

        public List<string> getLogs() { return this.model.getLogs(); }

        // Save
        public bool executeJobs(List<Job> jobs, string[] extensions) {return this.model.executeJobs(jobs, extensions); }
        public bool saveFile(string source, string destination) { return model.setSave(source, destination); }


        public List<Log> getLogsLst(string fileName) { return this.model.getLogsLst(fileName);  } 
        public FileInfo[] getLogsFiles() { return this.model.getLogsFiles();  }


    }
}
