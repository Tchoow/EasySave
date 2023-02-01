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
                Console.WriteLine("Faites un choix :");


                userInput = Console.ReadLine();

            // If we choose "5" it breaks the loop
            } while (userInput != "5");

            Console.WriteLine("Fin de l'éxécution du programme.");
        }
    }
}
