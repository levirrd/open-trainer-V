using GTA;
using GTA.Native;

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
            playerChar.IsInvincible = isEnabled;
            playerChar.CanRagdoll = !isEnabled;
            playerChar.IsBulletProof = isEnabled;
            playerChar.IsCollisionProof = isEnabled;
            playerChar.IsExplosionProof  = isEnabled;
            playerChar.IsFireProof  = isEnabled;
            playerChar.IsMeleeProof = isEnabled;
            playerChar.IsSmokeProof = isEnabled;
            playerChar.IsSteamProof = isEnabled;
            playerChar.IsWaterCannonProof  = isEnabled;
            string statusText = isEnabled ? "~g~Enabled" : "~r~Disabled";
            GTA.UI.Notification.Show($"~b~God Mode: {statusText}");
        }
        public static void HealPlayer()
        {
            int maxHealth = playerChar.MaxHealth;
            int maxArmor = Game.Player.MaxArmor;
            playerChar.Health = maxHealth;
            playerChar.Armor = maxArmor;
            GTA.UI.Notification.Show("Player has been healed.");
        }
        public static bool isInfiniteStaminaOn;
        public static void InfiniteStamina(bool isEnabled)
        {
            isInfiniteStaminaOn = isEnabled;
            string statusText = isEnabled ? "~g~Enabled" : "~r~Disabled";
            GTA.UI.Notification.Show($"~b~Infinite stamina: {statusText}");
        }
        public static void CleanPed()
        {
            playerChar.ClearBloodDamage();
            playerChar.ClearVisibleDamage();
            GTA.UI.Notification.Show("~b~Player has been cleaned.");
        }

        public static void SetPlayerVisible(bool IsVisible)
        {
            playerChar.IsVisible = !IsVisible;
            string statusText = IsVisible ? "~g~Invisible" : "~r~Visible";
            GTA.UI.Notification.Show($"~b~Player visibility: {statusText}");
        }

        public static void SetPlayerCash()
        {
            string result = Game.GetUserInput();
            if (int.TryParse(result, out int value))
            {
                string statName;
                if (playerChar.Model.Hash == Game.GenerateHash("player_zero"))
                    statName = "SP0_TOTAL_CASH"; // Michael
                else if (playerChar.Model.Hash == Game.GenerateHash("player_one"))
                    statName = "SP1_TOTAL_CASH"; // Franklin
                else if (playerChar.Model.Hash == Game.GenerateHash("player_two"))
                    statName = "SP2_TOTAL_CASH"; // Trevor
                else
                {
                    GTA.UI.Notification.Show("~r~Unknown character model. Cannot set cash.");
                    return;
                }
                Function.Call(Hash.STAT_SET_INT, Game.GenerateHash(statName), value, true);
                GTA.UI.Notification.Show($"~g~Set money to: ~s~${value}");
            }
            else
            {
                GTA.UI.Notification.Show("~r~Invalid number entered.");
            }
        }

        public static void SetPlayerWantedLevel(int wantedLevel)
        {
            Game.Player.WantedLevel = wantedLevel;
            GTA.UI.Notification.Show($"~b~Wanted Level Set : {wantedLevel}");
        }

        public static void NeverWanted(bool isEnabled)
        {
            Game.Player.WantedLevel = 0;
            Game.MaxWantedLevel = 0;
            string statusText = isEnabled ? "~g~Enabled" : "~r~Disabled";
            GTA.UI.Notification.Show($"~b~Never Wanted: {statusText}");
        }
        public static bool isSuperJumpOn;

        public static void SuperJump(bool isEnabled)
        {
            isSuperJumpOn = isEnabled;
            string statusText = isEnabled ? "~g~Enabled" : "~r~Disabled";
            GTA.UI.Notification.Show($"~b~Super Jump: {statusText}");
        }
        public static bool isFastRunOn;
        public static void FastRun(bool isEnabled)
        {
            isFastRunOn = isEnabled;
            string statusText = isEnabled ? "~g~Enabled" : "~r~Disabled";
            GTA.UI.Notification.Show($"~b~Fast Running: {statusText}");
        }
    }
}