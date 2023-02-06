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
                Console.WriteLine("| Liste des actions  (entre 1 et 5) :                   |");
                Console.WriteLine("| [1] Créer un traveaux de sauvegardes                  |");
                Console.WriteLine("| [2] Afficher les traveaux de sauvegardes.             |");
                Console.WriteLine("| [3] Supprimer un traveaux de sauvegardes.             |");
                Console.WriteLine("| [4] Executer une ou plusieurs sauvegarde.             |");
                Console.WriteLine("| [5] Afficher les logs journalières                    |");
                Console.WriteLine("| [6] Changer la langue.                                |");
                Console.WriteLine("| [7] Quitter le programme.                             |");
                Console.WriteLine("+-------------------------------------------------------+");
                Console.Write    ("> Veuillez choisir une action :"                          );

                // User Input
                userInput = Console.ReadLine();

                switch(userInput)
                {
                    case "1":
                        /* Jobs verifications */
                        if (this.viewModel.getJobsList().Count < 5)
                        {

                            /* User Inputs */
                            Console.WriteLine("Nom du travail de sauvegarde:");
                            string jobName = Console.ReadLine();
                            Console.WriteLine("Chemin du répertoire (Source) :");
                            string sourcePath = Console.ReadLine();
                            Console.WriteLine("Chemin du répertoire (Destination) :");
                            string targetPath = Console.ReadLine();
                            Console.WriteLine("Type de la sauvegarde :");
                            Console.WriteLine("[1] - Complète");
                            Console.WriteLine("[2] - Différentielle");
                            string inputSaveType = Console.ReadLine();
                            try
                            {
                                int saveType = Int16.Parse(inputSaveType);
                                /* Job Creation */
                                if (this.viewModel.addNewJob(new Job(jobName, sourcePath, targetPath, saveType)))
                                {
                                    // Success
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Création du traveaux effectuée.");
                                }
                                else
                                {
                                    // Error
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Création du traveaux echouée.");
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                                /* Job Creation */
                            }
                            catch // if its not int
                            {

                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Saisi incorrecte.");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;

                            }
                            /* User Inputs */


                        }
                        else
                        {
                            // Limit Error
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Il existe déjà 5 traveaux de sauvegarde.");
                            Console.WriteLine("Merci d'en supprimer pour pouvoir en créer de nouveaux");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        /* Jobs verifications */

                        break;
                    case "2":
                        Console.WriteLine("Voici les traveaux de sauvegardes :");
                        printJobInfos();
                        break;
                    case "3":
                        Console.WriteLine("Voici les traveaux de sauvegardes :");
                        printJobInfos();
                        Console.WriteLine("Veuillez choisir le numéro de sauvegarde que vous voulez supprimer.");
                        Console.Write("Traveaux n°:");
                        string inputIndexJob = Console.ReadLine();
                        int indexJob = Int16.Parse(inputIndexJob);

                        if (this.viewModel.deleteJobWithIndex(indexJob))
                        {
                            // Success
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Suppression effectuée.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Suppression echouée.");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "4":
                        Console.WriteLine("Executer un traveaux de sauvegarde");
                        Console.WriteLine("Liste des travaux de sauvegarde...");
                        break;
                    case "5":
                        Console.WriteLine("Logs journalières.");
                        Console.WriteLine("Liste des logs stockées sur le serveur:");
                        // Affiche l'ensemble des fichiers présent dans le serveur
                        Console.WriteLine("Quel fichier voulez vous lire ? :");
                        // Affiche le contenu du fichier selectionné.
                        break;
                    case "6":
                        Console.WriteLine("Liste des langues disponibles:");
                        for (int i = 0; i < this.translate.lstLanguages.Count; i++)
                        {
                            Console.WriteLine("[" + i + "] : " + this.translate.lstLanguages[i]);
                        }

                        /* User inputs */
                        Console.WriteLine("Quelle langue voulez vous utiliser ? :");
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
