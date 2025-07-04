﻿using GTA;
using GTA.Native;
using GTA.Math;
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
            var player = Game.Player;
            player.WantedLevel = 0;
            GTA.UI.Notification.Show("Wanted level has been cleared.");
        }

        public static void ToggleGodMode(bool isEnabled)
        {
            PlayerChar.IsInvincible = isEnabled;
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
            Function.Call(Hash.SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER, 0, isEnabled ? 1.49f : 1.0f);
            ShowStatus("Fast Run: ", isEnabled);
        }
        public static void TeleportToWayPoint()
        {
            if (!Game.IsWaypointActive)
            {
                GTA.UI.Notification.Show("~r~Error:~s~ No Waypoint set.");
                return;
            }
            Vector3 waypoint = World.WaypointPosition;
            Entity entity = PlayerChar;
            if (PlayerChar.IsInVehicle()) entity = PlayerChar.CurrentVehicle;
            float groundZ = 0f;
            bool foundGround = false;
            float[] testHeights = { 100f, 150f, 200f, 250f, 300f, 400f, 500f, 600f, 800f };
            foreach (float height in testHeights)
            {
                Vector3 testPos = new Vector3(waypoint.X, waypoint.Y, height);
                entity.Position = testPos;
                Script.Wait(100); 
                OutputArgument groundZArg = new OutputArgument();
                bool success = Function.Call<bool>(
                    Hash.GET_GROUND_Z_FOR_3D_COORD,
                    waypoint.X,
                    waypoint.Y,
                    height,
                    groundZArg
                );
                if(success)
                {
                    groundZ = groundZArg.GetResult<float>();
                    foundGround = true;
                    break;
                }
            }
            float finalZ = foundGround ? groundZ + 1.0f : waypoint.Z + 1.0f;
            entity.Position = new Vector3(waypoint.X, waypoint.Y, finalZ);
        }
    }
}