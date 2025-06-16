using System;
using System.Globalization;
using GTA;
using LemonUI.Menus;

namespace Open_Trainer_V.Features
{
    public class WeaponFunctions
    {
        private static  Ped PlayerChar = Game.Player.Character;
        public static void GiveAllWeapons()
        {
            foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
            {
                try { PlayerChar.Weapons.Give(weapon, 9999, false, false); }
                catch{ }
            } 
            GTA.UI.Notification.Show("~b~All weapons given.");
        }

        public static void RemoveAllWeapons()
        {
            PlayerChar.Weapons.RemoveAll();
        }

        public static void SelectWeapon(WeaponHash weapon)
        {
            try
            {
                PlayerChar.Weapons.Give(weapon, 999, true, true);
                GTA.UI.Notification.Show($"~b~Gave weapon: ~g~{weapon}");
            }
            catch
            {
                GTA.UI.Notification.Show("~r~Failed to give weapon.");
            }
        }

        public static void InfiniteAmmo(bool isEnabled)
        {
            Weapon currWeapon = PlayerChar.Weapons.Current;
            if (currWeapon != null)
            {
                currWeapon.InfiniteAmmo = isEnabled;
            }
            PlayerFunctions.ShowStatus("Player Visibility: ", isEnabled);
        }

        public static void SetAmmoInput()
        {
            Weapon currWeapon = PlayerChar.Weapons.Current;
            string result = Game.GetUserInput();
            if (currWeapon != null )
            {
                if (int.TryParse(result, out int value))
                {
                    currWeapon.Ammo = value;
                }
                else GTA.UI.Notification.Show("~b~Invalid ~r~input.");
                GTA.UI.Notification.Show("~b~Ammo given.");
            }
            else GTA.UI.Notification.Show("~r~Invalid ~r~Weapon.");
        }
    }
}