using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class Model
    {
        private ViewModel viewModel { get; set; }

        public Model(ViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
