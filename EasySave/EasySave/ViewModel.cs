using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace EasySave
{
    class ViewModel
    {
        private View view { get; set; }
        private View model { get; set; }
        private List<Save> lstSave { get; set; }
        private List<Log> lstLogs { get; set; }

        public ViewModel(View view)
        {
            this.view    = view;
            //5this.model   = new Model(this);
            this.lstLogs = new List<Log>();
            this.lstSave = new List<Save>();
        }

    }
}
