using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class Settings
    {
        private static Settings instanceSingleton = null;
        private Translate Language{get; set;}
        Settings()
        {

        }
        public static Settings InstanceSingleton
        {
            get
            {
                if (instanceSingleton == null)
                {
                    instanceSingleton = new Settings();
                }
                return instanceSingleton;
            }
        }
    }
}
