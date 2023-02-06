using System;

namespace EasySave
{
    class View
    {
        private ViewModel viewModel;
        private Translate translate;
        public View()
        {
            this.viewModel = new ViewModel(this);
            this.translate = new Translate();
        }


        public void printJobInfos()
        {
            for(int i=0; i < this.viewModel.getJobsList().Count; i++)
            {
                Console.WriteLine("+---------+ Traveaux n° " + (i+1) + " +---------+");
                Console.WriteLine("Nom : " + this.viewModel.getJobsList()[i].name);
                Console.WriteLine("Repertoire/Fichier source : " + this.viewModel.getJobsList()[i].sourceFilePath);
                Console.WriteLine("Repertoire de destination : " + this.viewModel.getJobsList()[i].destinationFilePath);
                Console.WriteLine("Type de sauvegarde : " + this.viewModel.getJobsList()[i].saveType);
                Console.WriteLine("Etat : " + this.viewModel.getJobsList()[i].state);
                Console.WriteLine("Date de création : " + this.viewModel.getJobsList()[i].created);
                Console.WriteLine("+---------------------------------------------+");
            }
        }


        public void init()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("`........                                 `.. ..                                 ");
            Console.WriteLine("`..                                     `..    `..                               ");
            Console.WriteLine("`..         `..     `.... `..   `..      `..         `..    `..     `..   `..    ");
            Console.WriteLine("`......   `..  `.. `..     `.. `..         `..     `..  `..  `..   `..  `.   `.. ");
            Console.WriteLine("`..      `..   `..   `...    `...             `.. `..   `..   `.. `..  `..... `..");
            Console.WriteLine("`..      `..   `..     `..    `..       `..    `..`..   `..    `.`..   `.        ");
            Console.WriteLine("`........  `.. `...`.. `..   `..          `.. ..    `.. `...    `..      `....   ");
            Console.ForegroundColor = ConsoleColor.White;

            string userInput;
            do
            {
                Console.WriteLine("+-------------------------------------------------------+");
                Console.WriteLine("| "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_0"))+"|") ;
                Console.WriteLine("| [1] "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_1"))+"|");
                Console.WriteLine("| [2] "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_2"))+"|");
                Console.WriteLine("| [3] "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_3"))+"|");
                Console.WriteLine("| [4] "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_4"))+"|");
                Console.WriteLine("| [5] "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_5"))+"|");
                Console.WriteLine("| [6] "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_6"))+"|");
                Console.WriteLine("| [7] "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_7"))+"|");
                Console.WriteLine("+-------------------------------------------------------+");
                Console.Write("> "+ (this.translate.getTraduction(this.viewModel.getLanguageIndex(), "action")));

                // User Input
                userInput = Console.ReadLine();

                switch(userInput)
                {
                    case "1":
                        /* Jobs verifications */
                        if (this.viewModel.getJobsList().Count < 5)
                        {

                            /* User Inputs */
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "savename"));
                            string jobName = Console.ReadLine();
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "sourcedirectory"));
                            string sourcePath = Console.ReadLine();
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "destinationdirectory"));
                            string targetPath = Console.ReadLine();
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "savetype"));
                            Console.WriteLine("[1] - "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "full")));
                            Console.WriteLine("[2] - "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "differential")));
                            string inputSaveType = Console.ReadLine();
                            try
                            {
                                int saveType = Int16.Parse(inputSaveType);
                                /* Job Creation */
                                if (this.viewModel.addNewJob(new Job(jobName, sourcePath, targetPath, saveType)))
                                {
                                    // Success
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "createsuccess"));
                                }
                                else
                                {
                                    // Error
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "createfail"));
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                                /* Job Creation */
                            }
                            catch // if its not int
                            {

                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "incorectwrite"));
                                Console.ForegroundColor = ConsoleColor.White;
                                break;

                            }
                            /* User Inputs */


                        }
                        else
                        {
                            // Limit Error
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "5save"));
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "pleasedelete"));
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        /* Jobs verifications */

                        break;
                    case "2":
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "choose"));
                        printJobInfos();
                        break;
                    case "3":
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "choose"));
                        printJobInfos();
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "choose"));
                        Console.Write(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "savenum"));
                        string inputIndexJob = Console.ReadLine();
                        int indexJob = Int16.Parse(inputIndexJob);

                        if (this.viewModel.deleteJobWithIndex(indexJob))
                        {
                            // Success
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "deletesucces"));
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "deletefail"));
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "4":
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "execsave"));
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "listsave"));
                        break;
                    case "5":
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "daylog"));
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "listlog"));
                        // Affiche l'ensemble des fichiers présent dans le serveur
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "readwhat"));
                        // Affiche le contenu du fichier selectionné.
                        break;
                    case "6":
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "language"));
                        for (int i = 0; i < this.translate.lstLanguages.Count; i++)
                        {
                            Console.WriteLine("[" + i + "] : " + this.translate.lstLanguages[i]);
                        }

                        /* User inputs */
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "chooselanguage"));
                        string inputIndexLang = Console.ReadLine();
                        int indexLangue       = Int16.Parse(inputIndexLang);
                        /* User inputs */

                        /* Set the language */
                        if (indexLangue > -1 && indexLangue < this.translate.lstLanguages.Count)
                            this.viewModel.setLangueIndex(indexLangue);
                        /* Set the language */

                        break;
                }

                // If we choose "7" it breaks the loop
            } while (userInput != "7");
            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "ch1"));
        }
    }
}
