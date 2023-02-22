using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveGUI
{
    using System.Windows;
    public abstract class Message
    {
        public abstract void Afficher(string message);

        public static Message CreerMessage(MessageType type)
        {
            switch (type)
            {
                case MessageType.Erreur:
                    return new MessageErreur();
                case MessageType.Information:
                    return new MessageInformation();
                case MessageType.Avertissement:
                    return new MessageAvertissement();
                default:
                    throw new NotSupportedException("Type de message non pris en charge : " + type);
            }
        }
    }

    public enum MessageType
    {
        Erreur,
        Information,
        Avertissement
    }

    public class MessageErreur : Message
    {
        public override void Afficher(string message)
        {
            MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public class MessageInformation : Message
    {
        public override void Afficher(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class MessageAvertissement : Message
    {
        public override void Afficher(string message)
        {
            MessageBox.Show(message, "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}


