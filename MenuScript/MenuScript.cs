﻿using GTA;
using GTA.Native;
using GTA.UI;
using LemonUI;
using LemonUI.Menus;
using Open_Trainer_V.Features; 
namespace Open_Trainer_V
{
    public class MenuScript
    {
         //menus + pool
         private readonly ObjectPool menuPool;
         private readonly NativeMenu TrainerMenu;
         private readonly NativeMenu PlayerOptions;
         private readonly NativeMenu VehicleOptions;
         private readonly NativeMenu WeaponOptions;
         private readonly NativeMenu WorldOptions;
         private readonly NativeMenu WantedOptions;
         public static MenuScript instance;

        private readonly NativeItem ClearWanted;
        private readonly NativeCheckboxItem GodMode;
        private readonly NativeCheckboxItem InfiniteStamina;
        private readonly NativeItem HealPlayer;
        private readonly NativeItem CleanPlayer;
        private readonly NativeCheckboxItem PlayerVisibility;
        private readonly NativeItem SetMoney;
        private readonly NativeSliderItem SetWantedLevel;
        private readonly NativeCheckboxItem NeverWanted;
        private readonly NativeCheckboxItem SuperJump;
        private readonly NativeCheckboxItem FastRunning;
        private readonly NativeItem CleanVehicle;
        private readonly NativeCheckboxItem VehicleGodMode;
        private readonly NativeItem SpawnVehicle;
        private readonly NativeItem FixVehicle;
        public MenuScript()
        {
            #region Menu Initialization
            TrainerMenu = new NativeMenu("Open Trainer V", "MAIN MENU", "OpenSource Trainer for V");
            PlayerOptions = new NativeMenu("Player Options", "Player Options", "Options related to the Player");
            WeaponOptions = new NativeMenu("Weapon Options", "Weapon Options", "Options related to Weapons");
            VehicleOptions = new NativeMenu("Vehicle Options", "Vehicle Options", "Options related to Vehicles");
            WorldOptions = new NativeMenu("World Options", "World Options", "Options related to the World");
            WantedOptions = new NativeMenu("Wanted Options", "Wanted Options", "Options related to the Wanted System");
            menuPool = new ObjectPool();
            instance = this;
            menuPool.Add(TrainerMenu);
            menuPool.Add(PlayerOptions);
            menuPool.Add(VehicleOptions);
            menuPool.Add(WeaponOptions);
            menuPool.Add(WorldOptions);
            menuPool.Add(WantedOptions);
            //submenus
            TrainerMenu.AddSubMenu(PlayerOptions);
            TrainerMenu.AddSubMenu(VehicleOptions);
            TrainerMenu.AddSubMenu(WeaponOptions);
            TrainerMenu.AddSubMenu(WorldOptions);
            PlayerOptions.AddSubMenu(WantedOptions);
            //fonts
            TrainerMenu.BannerText.Font = Font.ChaletComprimeCologne;
            PlayerOptions.BannerText.Font = Font.ChaletComprimeCologne;
            WeaponOptions.BannerText.Font = Font.ChaletComprimeCologne;
            VehicleOptions.BannerText.Font = Font.ChaletComprimeCologne;
            WorldOptions.BannerText.Font = Font.ChaletComprimeCologne;
            WantedOptions.BannerText.Font = Font.ChaletComprimeCologne;
            
            TrainerMenu.MouseBehavior = MenuMouseBehavior.Movement;
            #endregion
            
            #region Menu Items
            //player options
            GodMode = new NativeCheckboxItem("God Mode", "Sets God Mode to Player",false);
            HealPlayer = new NativeItem("Heal Player", "Heals the player (armor+health)");
            InfiniteStamina = new NativeCheckboxItem("Infinite Stamina", "Sets Infinite Stamina to Player", false);
            CleanPlayer = new NativeItem("Clean Player", "Cleans the player");
            PlayerVisibility = new NativeCheckboxItem("Player Visibility", "Sets player visibility to Player",false);
            SetMoney = new NativeItem("Money Input", "Money Input");
            SetWantedLevel = new NativeSliderItem("Set Wanted Level", "Sets Player Wanted Level", 5,0);
            ClearWanted = new NativeItem("Clear Wanted", "Clears Wanted Level for the Player");
            NeverWanted = new  NativeCheckboxItem("Never Wanted", "Disables Wanted Level",false);
            SuperJump = new  NativeCheckboxItem("Super Jump", "Makes the Player Jump HIGH",false);
            FastRunning = new  NativeCheckboxItem("Fast Running", "Makes the Player run faster",false);
            //vehicle options
            SpawnVehicle = new NativeItem("Spawn Vehicle", "Enter a vehicle model to spawn");
            VehicleGodMode = new NativeCheckboxItem("God Mode", "Sets vehicle God Mode",false);
            FixVehicle = new NativeItem("Fix Vehicle", "Repairs and cleans the vehicle");
            CleanVehicle =  new  NativeItem("Clean Vehicle", "Cleans the vehicle");
            #endregion
            
            #region Add Items to Menus
            //player
            WantedOptions.Add(SetWantedLevel);
            WantedOptions.Add(ClearWanted);
            WantedOptions.Add(NeverWanted);
            PlayerOptions.Add(GodMode);
            PlayerOptions.Add(InfiniteStamina);
            PlayerOptions.Add(PlayerVisibility);
            PlayerOptions.Add(SuperJump);
            PlayerOptions.Add(FastRunning);
            PlayerOptions.Add(HealPlayer);
            PlayerOptions.Add(CleanPlayer);
            PlayerOptions.Add(SetMoney);
            //vehicle
            VehicleOptions.Add(VehicleGodMode);
            VehicleOptions.Add(SpawnVehicle);
            VehicleOptions.Add(FixVehicle);
            VehicleOptions.Add(CleanVehicle);
            #endregion

            #region Event Handlers
            //player options
            GodMode.CheckboxChanged += (sender, args) => PlayerFunctions.ToggleGodMode(GodMode.Checked);
            InfiniteStamina.CheckboxChanged += (sender, args) => PlayerFunctions.InfiniteStamina(InfiniteStamina.Checked);
            HealPlayer.Activated += (sender, args) => PlayerFunctions.HealPlayer();
            ClearWanted.Activated += (sender, args) =>PlayerFunctions.ClearWantedLevel();
            CleanPlayer.Activated += (sender, args) => PlayerFunctions.CleanPed();
            PlayerVisibility.CheckboxChanged += (sender, args) => PlayerFunctions.SetPlayerVisibility(PlayerVisibility.Checked);
            SetMoney.Activated += (sender, args) => PlayerFunctions.SetPlayerCash();
            SetWantedLevel.ValueChanged += (sender, args) => PlayerFunctions.SetPlayerWantedLevel(SetWantedLevel.Value);
            NeverWanted.CheckboxChanged += (sender, args) => PlayerFunctions.NeverWanted(NeverWanted.Checked);
            SuperJump.CheckboxChanged += (sender, args) => PlayerFunctions.SuperJump(SuperJump.Checked);
            FastRunning.CheckboxChanged += (sender, args) => PlayerFunctions.FastRun(FastRunning.Checked);
            //vehicle options
            SpawnVehicle.Activated += (sender, args) => VehicleFunctions.SpawnVehicle();
            VehicleGodMode.CheckboxChanged += (sender, args) => VehicleFunctions.VehicleGodMode(VehicleGodMode.Checked);
            FixVehicle.Activated += (sender, args) => VehicleFunctions.FixVehicle();
            CleanVehicle.Activated += (sender, args) => VehicleFunctions.CleanVehicle();
            #endregion
        }
        public void Tick()
        {
            menuPool.Process();
            if (PlayerFunctions.isInfiniteStaminaOn && InfiniteStamina.Checked) Function.Call(Hash.RESET_PLAYER_STAMINA, Game.Player);
            
            if (PlayerFunctions.isSuperJumpOn && SuperJump.Checked) Function.Call(Hash.SET_SUPER_JUMP_THIS_FRAME, Game.Player.Handle);

            if (PlayerFunctions.isFastRunOn && FastRunning.Checked) Function.Call(Hash.SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER, Game.Player, 1.7f);
        }

        public void OpenMenu()
        {
            if (!menuPool.AreAnyVisible) TrainerMenu.Visible = !TrainerMenu.Visible;
        }
    }
}
