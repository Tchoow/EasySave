using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Collections;

namespace EasySave
{
    class Translate
    {
        private List<string> lstLanguages { get; set; }
        public Translate()
        {
            //get files of language ressources 
            string folderPath = "../../../../EasySave/datas/languages/";
            string[] files    = Directory.GetFiles(folderPath);
            this.lstLanguages = new List<string>();
            // get list of language in languages folder
            foreach (string file in files)
            {
                if (file.EndsWith(".resx"))
                {
                    String fileEdit = new string(file);
                    fileEdit = fileEdit.Replace("../../../../EasySave/datas/languages/", "");
                    fileEdit = fileEdit.Replace(".resx", "");
                    this.lstLanguages.Add(fileEdit);
                }
            }
        }

        public List<string> getLstLanguages() { return this.lstLanguages; }

        public string getTraduction(int indexLang, string tradKey)
        {
            // get lang from index
            string selectedLang = this.lstLanguages[indexLang];
            //set language file to the selected language
            ResourceManager rm = new ResourceManager("EasySave.datas.languages." + Path.GetFileName(selectedLang), typeof(Translate).Assembly);
            IDictionaryEnumerator enumerator = rm.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.Equals(tradKey))
                {
                    return enumerator.Value.ToString();
                }
            }
            return "";
        }
    }
}
