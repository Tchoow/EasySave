﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Reflection;

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

        public string getLogsXML(Log log, string path) { return log.getFormattedXMLLogs(path); }
        public string getLogsJSON(Log log, string path) { Trace.WriteLine(path);  return log.getFormattedJSONLogs(path); }


        public bool deleteJobWithIndex(int jobIndex) { return this.model.deleteJob(jobIndex); }
        public void updateJob(Job job,int index) { this.model.setJobByIndex(job, index); }

        public List<string> getLogs() { return this.model.getLogs(); }


        // Configs
        public void updateLstPriorities(List<string> lstPriorities) { this.model.LstPriorities = lstPriorities; }
        public void updateLstBusinessSoft(List<string> lstBusinessSoft) { this.model.LstBusinessSoft = lstBusinessSoft; }
        public List<string> getLstPriorities() { return this.model.LstPriorities; }
        public List<string> getLstBusinessSoft() { return this.model.LstBusinessSoft; }
        public void updateCryptoSoftPath(string path) { this.model.CryptoSoftPath = path; }
        public string getCryptoSoftPath() { return this.model.CryptoSoftPath; }
        public void updateMaxFileSizeSim(int   fileSize) { this.model.MaxFileSizeSim = fileSize;   }
        public int getMaxFileSizeSim() { return this.model.MaxFileSizeSim; }


        // Save
        public bool executeJobs(List<Job> jobs, string[] extensions) {return this.model.executeJobs(jobs, extensions); }
        public bool saveFile(string source, string destination) { return model.setSave(source, destination); }


        public List<Log> getLogsLst(string fileName) { return this.model.getLogsLst(fileName);  } 
        public FileInfo[] getLogsFiles() { return this.model.getLogsFiles();  }

        public void sendJobProgressionChanged(int index, int value)
        {
            Trace.WriteLine("viewmodel");
            var type = this.view.GetType();
            if(type.GetMethod("acceptObserver") != null)
            {
                this.view.acceptObserver(index, value);
            }
        }

    }
}
