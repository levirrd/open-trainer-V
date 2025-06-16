using GTA;
using GTA.Native;
namespace Open_Trainer_V.Features
{
    public static class PlayerFunctions
    {
        private static Ped PlayerChar => Game.Player.Character;
        public static bool isInfiniteStaminaOn;
        public static bool isSuperJumpOn;
        public static bool isFastRunOn;
        
        public static void ShowStatus(string feature, bool isEnabled)
        {
            string statusText = isEnabled ? "~g~Enabled" : "~r~Disabled";
            GTA.UI.Notification.Show($"~b~{feature} {statusText}");
        }
        public static void ClearWantedLevel()
        {
            Game.Player.WantedLevel = 0;
            GTA.UI.Notification.Show("Wanted level has been cleared.");
            
        }
        public static void ToggleGodMode(bool isEnabled)
        {
            PlayerChar.IsInvincible = isEnabled;
            PlayerChar.CanRagdoll = !isEnabled;
            PlayerChar.IsBulletProof = isEnabled;
            PlayerChar.IsCollisionProof = isEnabled;
            PlayerChar.IsExplosionProof  = isEnabled;
            PlayerChar.IsFireProof  = isEnabled;
            PlayerChar.IsMeleeProof = isEnabled;
            PlayerChar.IsSmokeProof = isEnabled;
            PlayerChar.IsSteamProof = isEnabled;
            PlayerChar.IsWaterCannonProof  = isEnabled;
            ShowStatus("God Mode: ", isEnabled);
        }
        public static void HealPlayer()
        {
            int maxHealth = PlayerChar.MaxHealth;
            int maxArmor = Game.Player.MaxArmor;
            PlayerChar.Health = maxHealth;
            PlayerChar.Armor = maxArmor;
            GTA.UI.Notification.Show("Player has been healed.");
        }
        public static void InfiniteStamina(bool isEnabled)
        {
            isInfiniteStaminaOn = isEnabled;
            ShowStatus("Infinite Stamina: ", isEnabled);
        }
        public static void CleanPed()
        {
            PlayerChar.ClearBloodDamage();
            PlayerChar.ClearVisibleDamage();
            GTA.UI.Notification.Show("~b~Player has been cleaned.");
        }
        public static void SetPlayerVisibility(bool isInvisible)
        {
            PlayerChar.IsVisible = !isInvisible;
            ShowStatus("Player Visibility: ", isInvisible);
        }
        public static void SetPlayerCash()
        {
            string result = Game.GetUserInput();
            if (int.TryParse(result, out int value))
            {
                string statName;
                if (PlayerChar.Model.Hash == Game.GenerateHash("player_zero"))
                    statName = "SP0_TOTAL_CASH"; // Michael
                else if (PlayerChar.Model.Hash == Game.GenerateHash("player_one"))
                    statName = "SP1_TOTAL_CASH"; // Franklin
                else if (PlayerChar.Model.Hash == Game.GenerateHash("player_two"))
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
            if (isEnabled) { Game.Player.WantedLevel = 0; Game.MaxWantedLevel = 0; }
            else Game.MaxWantedLevel = 5;
            ShowStatus("Never Wanted: ", isEnabled);
        }
        public static void SuperJump(bool isEnabled)
        {
            isSuperJumpOn = isEnabled;
            ShowStatus("Super Jump: ", isEnabled);
        }
        public static void FastRun(bool isEnabled)
        {
            isFastRunOn = isEnabled;
            ShowStatus("Fast Run: ", isEnabled);
        }
        
        
    }
}