using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIHM.Events
{
    public class CommandChangedEvent
    {

        private static CommandChangedEvent _myEvent { get; set; }

        private CommandChangedEvent() { }


        public static CommandChangedEvent GetEvent() {
                if(_myEvent == null)
            {
                _myEvent = new CommandChangedEvent();
            }
            return _myEvent;
        }
        public event EventHandler Handler;

        public void OnButtonPressedActionHandler(EventArgs e)
        {
            if(Handler != null)
            {
                Handler(this, e);
            }
        }
    }
}