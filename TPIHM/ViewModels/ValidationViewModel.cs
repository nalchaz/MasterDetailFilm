using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIHM.Events;

namespace TPIHM.ViewModels
{
    public class ValidationViewModel : NotifyPropertyChangedBase
    {
        public DelegateCommand YesCommand { get; set; }
        public DelegateCommand NoCommand { get; set; }

        public bool Valid { get; set; }

        public string Text { get; set; }

        public ValidationViewModel(string text)
        {
            Text = text;

            YesCommand = new DelegateCommand(OnYesCommand, CanYesCommand);
            NoCommand = new DelegateCommand(OnNoCommand, CanNoCommand);
        }

        private void OnYesCommand(object o)
        {
            CommandChangedEvent.GetEvent().OnButtonPressedActionHandler(EventArgs.Empty);
            Valid = true;
        }


        private bool CanYesCommand(object o)
        {

            return true;
        }

        private void OnNoCommand(object o)
        {
            CommandChangedEvent.GetEvent().OnButtonPressedActionHandler(EventArgs.Empty);
        }


        private bool CanNoCommand(object o)
        {
            return true;
        }
    }
}
