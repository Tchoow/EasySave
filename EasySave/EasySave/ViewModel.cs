using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace EasySave
{
    class ViewModel
    {
        private View view           { get; set; }
        private Model model { get; set; }
        public List<Log> lstLogs { get; set; }

        public ViewModel(View view)
        {
            this.view    = view;
            this.model   = new Model(this);
            this.lstLogs = new List<Log>();
        }


        // Traductions
        public void setLangueIndex(int indexLang) { this.model.currenLang = indexLang; }
        public bool saveFile(string source, string destination)
        {
            return model.setSave(source, destination);
        }
        public int getLanguageIndex() { return this.model.currenLang; }

        // Jobs
        public bool addNewJob(Job newJob) { return this.model.setJob(newJob);  }
        public List<Job> getJobsList()    {return this.model.getJobs(); }

        public bool deleteJobWithIndex(int jobIndex) { return this.model.deleteJob(jobIndex); }

        public List<string> getLogs() { return this.model.getLogs(); }

        public bool execJobs(int[] idsOfJobs)
        {
            return true;
        }



    }
}
