using GTA; 

namespace Open_Trainer_V.Features
{
    public static class PlayerFunctions
    {
        private static Ped playerChar = Game.Player.Character;
        public static void ClearWantedLevel()
        {
            Game.Player.WantedLevel = 0;
            GTA.UI.Notification.Show("Wanted level has been cleared.");
            
        }
        public static void ToggleGodMode(bool isEnabled)
        {
            if (isEnabled) playerChar.IsInvincible = true;
            else playerChar.IsInvincible = false;
            GTA.UI.Notification.Show("~b~God Mode: " + (isEnabled ? "~g~Enabled" : "~r~Disabled"));
        }

        public static void HealPlayer()
        {
            int maxHealth = playerChar.MaxHealth;
            int maxArmor = Game.Player.MaxArmor;
            playerChar.Health = maxHealth;
            playerChar.Armor = maxArmor;
            GTA.UI.Notification.Show("Player has been healed.");
        }
    }
}