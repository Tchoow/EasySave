using System;
using System.IO;
using System.Collections.Generic;

namespace EasySave
{
    class View
    {
        private bool success = false;
        private ViewModel viewModel;
        private Translate translate;
        public View()
        {
            this.viewModel = new ViewModel(this);
            this.translate = new Translate();
        }

        public void executeJobs(List<Job> jobs)
        {
            for (int i = 0; i < jobs.Count; i++)
            {
                Console.WriteLine(jobs[i].name);
                FileAttributes attrDest = File.GetAttributes(jobs[i].destinationFilePath);
                FileAttributes attrSrc = File.GetAttributes(jobs[i].sourceFilePath);
                if ((attrSrc & FileAttributes.Directory) == FileAttributes.Directory && (attrSrc & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string[] files = Directory.GetFiles(jobs[i].sourceFilePath);
                    for(int j = 0; j < files.Length; j++)
                    {
                        string fileName = Path.GetFileName(files[j]);
                        var myFile = File.Create(jobs[i].destinationFilePath+"\\"+fileName);
                        myFile.Close();
                        viewModel.saveFile(files[j], jobs[i].destinationFilePath+"\\"+fileName);
                    }
                    Console.WriteLine("Sauvegarde complète");
                }
                else
                {
                    Console.WriteLine("La source ou la destination n'est pas un dossier");
                }
            }
        }

        public void printJobInfos()
        {
            for(int i=0; i < this.viewModel.getJobsList().Count; i++)
            {
                Console.WriteLine("+---------------------------+");
                Console.WriteLine(String.Format("| {0, -25} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "job") + (i + 1)));
                Console.WriteLine("+---------------------------+---------------------------+-------------------------------------------------------+");
                Console.WriteLine(String.Format("| {0, -109} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "name") + " : " + this.viewModel.getJobsList()[i].name));
                Console.WriteLine(String.Format("| {0, -109} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "fromdir") + " : " + this.viewModel.getJobsList()[i].sourceFilePath));
                Console.WriteLine(String.Format("| {0, -109} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "todir") + " : " + this.viewModel.getJobsList()[i].destinationFilePath));
                Console.WriteLine(String.Format("| {0, -109} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "savetype") + " : " + this.viewModel.getJobsList()[i].saveType));
                Console.WriteLine(String.Format("| {0, -109} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "state") + " : " + this.viewModel.getJobsList()[i].state));
                Console.WriteLine("+-------------------------------------------------------+-------------------------------------------------------+");
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
                Console.WriteLine(String.Format("| {0, -53} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(),     "menu_0")));
                Console.WriteLine(String.Format("| [1] {0, -49} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_1")));
                Console.WriteLine(String.Format("| [2] {0, -49} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_2")));
                Console.WriteLine(String.Format("| [3] {0, -49} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_3")));
                Console.WriteLine(String.Format("| [4] {0, -49} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_4")));
                Console.WriteLine(String.Format("| [5] {0, -49} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_5")));
                Console.WriteLine(String.Format("| [6] {0, -49} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_6")));
                Console.WriteLine(String.Format("| [7] {0, -49} |", this.translate.getTraduction(this.viewModel.getLanguageIndex(), "menu_7")));
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
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "fromdir"));
                            string sourcePath = Console.ReadLine();
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "todir"));
                            string targetPath = Console.ReadLine();
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "savetype"));
                            Console.WriteLine("[1] - "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "full")));
                            Console.WriteLine("[2] - "+(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "diff")));
                            string inputSaveType = Console.ReadLine();
                            try
                            {
                                int saveType = Int16.Parse(inputSaveType);
                                /* Job Creation */
                                if (this.viewModel.addNewJob(new Job(jobName, sourcePath, targetPath, saveType)))
                                {
                                    // Success
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "creatsucc"));
                                }
                                else
                                {
                                    // Error
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "creatfail"));
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                                /* Job Creation */
                            }
                            catch // if its not int
                            {

                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "wronginput"));
                                Console.ForegroundColor = ConsoleColor.White;
                                break;

                            }
                            /* User Inputs */
                        }
                        else
                        {
                            // Limit Error
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "maxsave"));
                            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "askdel"));
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        /* Jobs verifications */

                        break;
                    case "2":
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "deletesave"));
                        printJobInfos();
                        break;
                    case "3":
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "deletesave"));
                        printJobInfos();
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "deletesave"));
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
                        Console.WriteLine("Executer un traveaux de sauvegarde");
                        printJobInfos();
                        Console.WriteLine("Choisissez le numéro job à executer. (Entrez 0 pour tous les lancer)");
                        string userExecutionChoice = Console.ReadLine();
                        switch(userExecutionChoice)
                        {
                            case "0":
                                executeJobs(viewModel.getJobsList());
                                break;
                            default:
                                executeJobs(new List<Job>(1) { viewModel.getJobsList()[Convert.ToInt32(userExecutionChoice) - 1] });
                                break;
                        }
                        break;
                    case "5":
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "daylog"));
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "listlog"));
                        for (int i = 0; i < this.viewModel.getLogs().Count; i++)
                        {
                            Console.WriteLine("[" + (i + 1) + "] - logs du:" + this.viewModel.getLogs()[i]);
                        }
                        
                        // Affiche l'ensemble des fichiers présent dans le serveur
                        // Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "readwhat"));
                        // Affiche le contenu du fichier selectionné.
                        break;
                    case "6":
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "language"));
                        for (int i = 0; i < this.translate.lstLanguages.Count; i++)
                        {
                            Console.WriteLine("[" + i + "] : " + this.translate.lstLanguages[i]);
                        }

                        /* User inputs */
                        Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "selectlanguage"));
                        string inputIndexLang = Console.ReadLine();
                        try
                        {
                            int indexLangue = Int16.Parse(inputIndexLang);
                            /* User inputs */
                            /* Set the language */
                            if (indexLangue > -1 && indexLangue < this.translate.lstLanguages.Count)
                            {
                                this.viewModel.setLangueIndex(indexLangue);
                                /* Set the language */
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Ok !");
                            }
                            else
                            {
                                /* Set the language */
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Langue non trouvée");
                            }
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error !");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
                // If we choose "7" it breaks the loop
            } while (userInput != "7");
            Console.WriteLine(this.translate.getTraduction(this.viewModel.getLanguageIndex(), "ch1"));
        }
    }
}
