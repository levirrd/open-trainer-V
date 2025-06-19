using System;
using GTA;

namespace Open_Trainer_V.Features
{
    public class WeaponFunctions
    {
        private static  Ped PlayerChar => Game.Player.Character;
        public static bool isInfiniteAmmoEnabled;
        public static void GiveAllWeapons()
        {
            foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
            {
                if (weapon == WeaponHash.Unarmed) continue;
                try { PlayerChar.Weapons.Give(weapon, 9999, false, false); }
                catch{ }
            } 
            GTA.UI.Notification.Show("~b~All weapons given.");
        }

        public static void RemoveAllWeapons()
        {
            PlayerChar.Weapons.RemoveAll();
            GTA.UI.Notification.Show("~b~All weapons Removed.");
            
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
            if (currWeapon != null && currWeapon!= WeaponHash.Unarmed)
            {
                currWeapon.InfiniteAmmo = isEnabled;
                isInfiniteAmmoEnabled = isEnabled;
            }
        }

        public static void SetAmmoInput()
        {
            Weapon currWeapon = PlayerChar.Weapons.Current;
            string result = Game.GetUserInput();
            if (currWeapon != null && currWeapon!= WeaponHash.Unarmed)
            {
                if (int.TryParse(result, out int value))
                {
                    currWeapon.Ammo = value;
                    GTA.UI.Notification.Show("~b~Ammo given.");
                }
                else GTA.UI.Notification.Show("~b~Invalid ~r~input.");
            }
            else GTA.UI.Notification.Show("~r~Invalid ~r~Weapon.");
        }

        public static void RemoveCurrentWeapon()
        {
            Weapon currWeapon = PlayerChar.Weapons.Current;
            if (currWeapon != null && currWeapon != WeaponHash.Unarmed)
            {
                PlayerChar.Weapons.Remove(currWeapon.Hash);
                GTA.UI.Notification.Show($"~b~Removed weapon: ~g~{currWeapon.Hash}");
            }
            else GTA.UI.Notification.Show("~r~No weapon to remove.");
        }
    }
}