using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace EasySave
{
    class ViewModel
    {
        private View           view;
        private Model         model;
        private Job job { get; set; }
        private List<Log>   lstLogs;
        private List<Save>  lstSave;

        public ViewModel(View view)
        {
            this.view    = view;
            this.model   = new Model(this);
            this.lstLogs = new List<Log>();
            this.lstSave = new List<Save>();
        }

        public void CreateJob()
        {
            this.model.setJob(new Job("test", "/", "//", "START", 15, 1472, 12, 0));
        }
        
        public Job retrieveJob()
        {
            return this.model.getJob();
        }

    }
}
