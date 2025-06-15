using System;
using GTA;
using System.Windows.Forms;
namespace Open_Trainer_V
{
    public class OTV : Script
    {
        public MenuScript menuScript;

        public OTV()
        {
            menuScript = new MenuScript();
            this.KeyUp += OnKeyUp;
            this.Tick += OnTick;
        }
        private void OnTick(object sender, EventArgs e)
        {
            MenuScript.instance.Tick();
        }
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4) MenuScript.instance.OpenMenu();
        }


    }
}