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
        private List<Log>   lstLogs;
        private List<Save>  lstSave;

        public ViewModel(View view)
        {
            this.view    = view;
            this.model   = new Model(this);
            this.lstLogs = new List<Log>();
            this.lstSave = new List<Save>();
        }

    }
}
