using System;

namespace EasySave
{
    class View
    {
        private ViewModel viewModel;

        public View()
        {
            this.viewModel = new ViewModel(this);
        }

        public void init()
        {
            string userInput;
            do
            {
                Console.WriteLine("Faites un choix  (entre 1 et 5) :");
                Console.WriteLine("[1] Nouvelle sauvegarde");
                Console.WriteLine("[2] Afficher les traveaux de sauvegardes.");
                Console.WriteLine("[3] Executer une ou plusieurs sauvegarde.");
                Console.WriteLine("[4] Afficher les logs journalières");
                Console.WriteLine("[5] Changer la langue.");
                Console.WriteLine("[6] Quitter le programme.");

                // User Input
                userInput = Console.ReadLine();

                switch(userInput)
                {
                    case "1":
                        Console.WriteLine("Nom de la sauvegarde:");
                        string saveName   = Console.ReadLine();
                        Console.WriteLine("Chemin du répertoire (Source) :");
                        string sourcePath = Console.ReadLine();
                        Console.WriteLine("Chemin du répertoire (Source) :");
                        string targetPath = Console.ReadLine();
                        this.viewModel.newSave();
                        break;
                    case "2":
                        Console.WriteLine("Voici les traveaux de sauvegardes :");
                        break;
                    case "3":
                        Console.WriteLine("3...");
                        break;
                    case "4":
                        Console.WriteLine("Logs journalières.");
                        Console.WriteLine("Liste des logs stockées sur le serveur:");
                        // Affiche l'ensemble des fichiers présent dans le serveur
                        Console.WriteLine("Quel fichier voulez vous lire ? :");
                        // Affiche le contenu du fichier selectionné.
                        break;
                    case "5":
                        Console.WriteLine("Liste des langues disponibles:");
                        // Affiche les fichiers dans le dossier languages
                        Console.WriteLine("Quelle langue voulez vous utiliser ? :");
                        // Selectionner le fichier de langue à utiliser
                        break;
                }

            // If we choose "5" it breaks the loop
            } while (userInput != "6");
            Console.WriteLine("Fin de l'éxécution du programme.");
        }
    }
}
