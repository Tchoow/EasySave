using EasySave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasySave;

namespace EasySaveGUI
{

    public partial class PageSett : Page
    {
        private List<string> lstPriorities;
        private List<string> lstBusinessProgram;
        private ViewModel    viewModel;

        public PageSett(ViewModel viewModel)
        {
            this.lstPriorities      = new List<string>();
            this.lstBusinessProgram = new List<string>();
            this.viewModel          = viewModel;
            InitializeComponent();

            // load Comboboxes

            foreach (string priority in this.viewModel.getLstPriorities())
            {
                cbPriority.Items.Add(priority);
            }

            foreach (string software in this.viewModel.getLstBusinessSoft())
            {
                cbBusinessProgram.Items.Add(software);
            }

        }




        /* Priority */
        private void addPriority(object sender, RoutedEventArgs e)
        {
            string newPriority = txtBoxPriority.Text;

            // Ajoute la nouvelle entrée à la liste de priorités
            this.lstPriorities.Add(newPriority);

            // Ajoute la nouvelle entrée à la ComboBox
            cbPriority.Items.Add(newPriority);

            // Update Priority List
            this.viewModel.updateLstPriorities(this.lstPriorities);


            // Efface la TextBox
            txtBoxPriority.Text = "";

        }


        private void removePriority(object sender, RoutedEventArgs e)
        {
            string selectedPriority = cbPriority.SelectedItem as string;

            // Supprime l'élément sélectionné de la liste de priorités
            this.lstPriorities.Remove(selectedPriority);

            // Update Priority List
            this.viewModel.updateLstPriorities(this.lstPriorities);


            // Supprime l'élément sélectionné de la ComboBox
            int selectedIndex = cbPriority.SelectedIndex;
            if (selectedIndex != -1)
            {
                cbPriority.Items.RemoveAt(selectedIndex);
            }
        }


        /* Business Program */
        private void addBusinessSoft(object sender, RoutedEventArgs e)
        {
            string newBusinessProgram = txtBoxBusinessProgram.Text;

            // Ajoute la nouvelle entrée à la liste des programmes d'entreprise
            this.lstBusinessProgram.Add(newBusinessProgram);

            // Ajoute la nouvelle entrée à la ComboBox
            cbBusinessProgram.Items.Add(newBusinessProgram);

            // Update Business Soft priority
            this.viewModel.updateLstBusinessSoft(this.lstBusinessProgram);

            // Efface la TextBox
            txtBoxBusinessProgram.Text = "";
        }

        private void removeBusinessSoft(object sender, RoutedEventArgs e)
        {
            string selectedBusinessProgram = cbBusinessProgram.SelectedItem as string;

            // Supprime l'élément sélectionné de la liste des programmes d'entreprise
            this.lstBusinessProgram.Remove(selectedBusinessProgram);

            // Update Business Soft priority
            this.viewModel.updateLstBusinessSoft(this.lstBusinessProgram);

            // Supprime l'élément sélectionné de la ComboBox
            int selectedIndex = cbBusinessProgram.SelectedIndex;
            if (selectedIndex != -1)
            {
                cbBusinessProgram.Items.RemoveAt(selectedIndex);
            }
        }
    }
}
