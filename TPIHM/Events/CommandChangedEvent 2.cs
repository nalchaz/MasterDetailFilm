using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIHM.Events
{
    public class CommandChangedEvent2
    {

        private static CommandChangedEvent2 _myEvent2 { get; set; }

        private CommandChangedEvent2() { }


        public static CommandChangedEvent2 GetEvent() {
                if(_myEvent2 == null)
            {
                _myEvent2 = new CommandChangedEvent2();
            }
            return _myEvent2;
        }
        public event EventHandler Handler;

        public void OnButtonPressedActionHandler2(EventArgs e)
        {
            if(Handler != null)
            {
                Handler(this, e);
            }
        }
    }
}