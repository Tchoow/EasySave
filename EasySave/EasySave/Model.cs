using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class Model
    {
        private ViewModel viewModel;
        private Job job;
        public Model(ViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        public void setJob(Job job)
        {
            this.job = job;
        }

        public Job getJob()
        {
            return this.job;
        }
    }
}
