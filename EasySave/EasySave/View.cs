using System;
using System.IO;
using System.Collections.Generic;

namespace EasySave
{
   public class View
    {
        private ViewModel viewModel;
        public View()
        {
            this.viewModel = new ViewModel(this);
        }
        public void printJobInfos()
        {
            for (int i = 0; i < this.viewModel.getJobsList().Count; i++)
            {
                Console.WriteLine("+---------------------------+");
                Console.WriteLine(String.Format("| {0, -25} |", this.viewModel.getTraduction("job") + (i + 1)));
                Console.WriteLine("+---------------------------+---------------------------+-------------------------------------------------------+");
                Console.WriteLine(String.Format("| {0, -109} |", this.viewModel.getTraduction("name") + this.viewModel.getJobsList()[i].Name));
                Console.WriteLine(String.Format("| {0, -109} |", this.viewModel.getTraduction("fromdir") + this.viewModel.getJobsList()[i].SourceFilePath));
                Console.WriteLine(String.Format("| {0, -109} |", this.viewModel.getTraduction("todir") + this.viewModel.getJobsList()[i].DestinationFilePath));
                Console.WriteLine(String.Format("| {0, -109} |", this.viewModel.getTraduction("savetype") + this.viewModel.getJobsList()[i].SaveType));
                Console.WriteLine(String.Format("| {0, -109} |", this.viewModel.getTraduction("state") + this.viewModel.getJobsList()[i].State));
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
                Console.WriteLine(String.Format("| {0, -53} |", this.viewModel.getTraduction("menu_0")));
                Console.WriteLine(String.Format("| [1] {0, -49} |", this.viewModel.getTraduction("menu_1")));
                Console.WriteLine(String.Format("| [2] {0, -49} |", this.viewModel.getTraduction("menu_2")));
                Console.WriteLine(String.Format("| [3] {0, -49} |", this.viewModel.getTraduction("menu_3")));
                Console.WriteLine(String.Format("| [4] {0, -49} |", this.viewModel.getTraduction("menu_4")));
                Console.WriteLine(String.Format("| [5] {0, -49} |", this.viewModel.getTraduction("menu_5")));
                Console.WriteLine(String.Format("| [6] {0, -49} |", this.viewModel.getTraduction("menu_6")));
                Console.WriteLine(String.Format("| [7] {0, -49} |", this.viewModel.getTraduction("menu_7")));
                Console.WriteLine("+-------------------------------------------------------+");
                Console.Write("> " + (this.viewModel.getTraduction("action")));

                // User Input
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        /* Jobs verifications */
                        if (this.viewModel.getJobsList().Count < 5)
                        {
                            /* User Inputs */
                            Console.Write(this.viewModel.getTraduction("savename"));
                            string jobName = Console.ReadLine();

                            Console.Write(this.viewModel.getTraduction("fromdir"));
                            string sourcePath = Console.ReadLine();

                            Console.Write(this.viewModel.getTraduction("todir"));
                            string targetPath = Console.ReadLine();

                            // UNC structure
                            if (!sourcePath.Contains("\\") || !targetPath.Contains("\\"))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(this.viewModel.getTraduction("errorpath"));
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            }


                            Console.WriteLine(this.viewModel.getTraduction("savetype"));
                            Console.WriteLine("[1] - " + (this.viewModel.getTraduction("full")));
                            Console.WriteLine("[2] - " + (this.viewModel.getTraduction("diff")));
                            string inputSaveType = Console.ReadLine();
                            try
                            {
                                int saveType = Int16.Parse(inputSaveType);
                                /* Job Creation */
                                if (this.viewModel.addNewJob(new Job(jobName, sourcePath, targetPath, saveType, "Paused")) || (saveType > 0 && saveType < 1))
                                {
                                    // Success
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(this.viewModel.getTraduction("creatsucc"));
                                }
                                else
                                {
                                    // Error
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine(this.viewModel.getTraduction("creatfail"));
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                                /* Job Creation */
                            }
                            catch // if its not int
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(this.viewModel.getTraduction("wronginput"));
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            }
                            /* User Inputs */
                        }
                        else
                        {
                            // Limit Error
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(this.viewModel.getTraduction("maxsave"));
                            Console.WriteLine(this.viewModel.getTraduction("askdel"));
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        /* Jobs verifications */

                        break;
                    case "2":
                        Console.WriteLine(this.viewModel.getTraduction("listsave"));
                        printJobInfos();
                        break;
                    case "3":
                        Console.WriteLine(this.viewModel.getTraduction("deletesave"));
                        printJobInfos();
                        Console.WriteLine(this.viewModel.getTraduction("deletesave"));
                        Console.Write(this.viewModel.getTraduction("savenum"));

                        try
                        {
                            string inputIndexJob = Console.ReadLine();
                            int indexJob = Int16.Parse(inputIndexJob);
                            if (this.viewModel.deleteJobWithIndex(indexJob))
                            {
                                // Success
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(this.viewModel.getTraduction("deletesucces"));
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(this.viewModel.getTraduction("deletefail"));
                            }
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine(this.viewModel.getTraduction("wronginput"));
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "4":
                        Console.WriteLine(this.viewModel.getTraduction("jobtittle"));
                        printJobInfos();
                        Console.WriteLine(this.viewModel.getTraduction("execsave"));
                        string userExecutionChoice = Console.ReadLine();
                        try
                        {
                            int jobExecutionIndex = Int16.Parse(userExecutionChoice);
                            switch (jobExecutionIndex)
                            {
                                case 0: // All jobs executed
                                    if (this.viewModel.executeJobs(this.viewModel.getJobsList()))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(this.viewModel.getTraduction("savesucc"));
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine(this.viewModel.getTraduction("saverror"));
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    break;
                                default: // Single job executed
                                    if (this.viewModel.getJobsList().Count > 0 && jobExecutionIndex <= this.viewModel.getJobsList().Count)
                                    {
                                        if (this.viewModel.executeJobs(new List<Job>(1) { viewModel.getJobsList()[Convert.ToInt32(userExecutionChoice) - 1] }))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine(this.viewModel.getTraduction("savesucc"));
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine(this.viewModel.getTraduction("saverror"));
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine(this.viewModel.getTraduction("wronginput"));
                                    }
                                    break;
                            }
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine(this.viewModel.getTraduction("wronginput"));
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "5":
                        Console.WriteLine(this.viewModel.getTraduction("daylog"));
                        Console.WriteLine(this.viewModel.getTraduction("listlog"));
                        for (int i = 0; i < this.viewModel.getLogs().Count; i++)
                        {
                            Console.WriteLine("[" + (i + 1) + "] - log:" + this.viewModel.getLogs()[i]);
                        }

                        // Affiche l'ensemble des fichiers présent dans le serveur
                        // Console.WriteLine(this.viewModel.getTraduction("readwhat"));
                        // Affiche le contenu du fichier selectionné.
                        break;
                    case "6":
                        Console.WriteLine(this.viewModel.getTraduction("language"));
                        for (int i = 0; i < this.viewModel.getLstLanguages().Count; i++)
                        {
                            Console.WriteLine("[" + i + "] : " + this.viewModel.getLstLanguages()[i]);
                        }

                        /* User inputs */
                        Console.WriteLine(this.viewModel.getTraduction("selectlanguage"));
                        string inputIndexLang = Console.ReadLine();
                        try
                        {
                            int indexLangue = Int16.Parse(inputIndexLang);
                            /* User inputs */
                            /* Set the language */
                            if (indexLangue > -1 && indexLangue < this.viewModel.getLstLanguages().Count)
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
                                Console.WriteLine(this.viewModel.getTraduction("faillang"));
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
        }
    }
}
