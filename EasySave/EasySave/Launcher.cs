using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class Launcher
    {

        static void Main(string[] args)
        {
            /*Log logs = new Log("name",
                    "fileSource",
                    "fileTarget",
                    "destPath",
                    4552,
                   646
                   );*/
            logs.saveLogInFile();
            View view = new View();
            view.init();
        }
    }
}
