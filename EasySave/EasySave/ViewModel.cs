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
        private List<Save> lstSave { get; set; }
        public List<Log> lstLogs { get; set; }

        public Job job { get; set; }

        public ViewModel(View view)
        {
            this.view    = view;
            this.model   = new Model(this);
            this.lstLogs = new List<Log>();
            this.lstSave = new List<Save>();
        }


        public void newSave(string name, string source, string destination)
        {
            this.job = new Job(name, source, destination, "PAUSED", 1, 1, 1, 0);
            this.model.setJob(job);
            this.model.getJobs();
        }







    }
}
