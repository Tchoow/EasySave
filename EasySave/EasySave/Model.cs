using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class Model
    {
        private ViewModel viewModel { get; set; }
        private string    language { get; set; }

        public Model(ViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.language  = "";
        }
    }
}
