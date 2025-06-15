using System;
using System.Runtime.Remoting.Channels;
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
        
        public static MenuScript instance;
        //items 
        //player
        private readonly NativeItem ClearWanted;
        private readonly NativeCheckboxItem GodMode;
        private readonly NativeCheckboxItem InfiniteStamina;
        private readonly NativeItem HealPlayer;
        private readonly NativeItem CleanPlayer;
        private readonly NativeCheckboxItem PlayerVisibility;
        private readonly NativeItem SetMoney;
        private readonly NativeSliderItem SetWantedLevel;
        private readonly NativeCheckboxItem NeverWanted;
        
        private readonly NativeItem FixVehicle;
        public MenuScript()
        {
            instance = this;
            menuPool = new ObjectPool();
            //menus + submenus
            TrainerMenu = new NativeMenu("Open Trainer V", "MAIN MENU", "OpenSource Trainer for V");
            PlayerOptions = new NativeMenu("Player Options", "Player Options", "Options related to the player");
            WeaponOptions = new NativeMenu("Weapon Options", "Weapon Options", "Options related to weapons");
            VehicleOptions = new NativeMenu("Vehicle Options", "Vehicle Options", "Options related to the vehicle");
            WorldOptions = new NativeMenu("World Options", "World Options", "Options related to the world");
            //pool
            menuPool.Add(TrainerMenu);
            menuPool.Add(PlayerOptions);
            menuPool.Add(WeaponOptions);
            menuPool.Add(VehicleOptions);
            menuPool.Add(WorldOptions);
            //add submenus
            TrainerMenu.AddSubMenu(PlayerOptions);
            TrainerMenu.AddSubMenu(WeaponOptions);
            TrainerMenu.AddSubMenu(VehicleOptions);
            TrainerMenu.AddSubMenu(WorldOptions);
            TrainerMenu.BannerText.Font = Font.ChaletComprimeCologne;
            TrainerMenu.MouseBehavior = MenuMouseBehavior.Movement;
            FixVehicle = new NativeItem("Fix Vehicle", "Repairs and cleans the vehicle");
            ClearWanted = new NativeItem("Clear Wanted", "Clears Wanted Level");
            GodMode = new NativeCheckboxItem("God Mode", "Sets God Mode",false);
            HealPlayer = new NativeItem("Heal Player", "Heals the player (armor+health)");
            InfiniteStamina = new NativeCheckboxItem("Infinite Stamina", "Infinite Stamina Level", false);
            CleanPlayer = new NativeItem("Clean Player", "Cleans the player");
            PlayerVisibility = new NativeCheckboxItem("Player Visibility", "Sets player visibility",false);
            SetMoney = new NativeItem("Money Input", "Money Input");
            SetWantedLevel = new NativeSliderItem("Set Wanted Level", "Sets Wanted Level", 5,0);
            NeverWanted = new  NativeCheckboxItem("Never Wanted", "Never Wanted",false);
            
            //add items to menus or submenus
            PlayerOptions.Add(ClearWanted);
            PlayerOptions.Add(GodMode);
            PlayerOptions.Add(InfiniteStamina);
            PlayerOptions.Add(HealPlayer);
            PlayerOptions.Add(CleanPlayer);
            PlayerOptions.Add(PlayerVisibility);
            PlayerOptions.Add(SetWantedLevel);
            PlayerOptions.Add(SetMoney);
            PlayerOptions.Add(NeverWanted);
            
            VehicleOptions.Add(FixVehicle);
            
            //funcs
            GodMode.CheckboxChanged += (sender, e) =>
            {
                PlayerFunctions.ToggleGodMode(GodMode.Checked);
            };
            InfiniteStamina.CheckboxChanged += (sender, e) =>
            {
                PlayerFunctions.InfiniteStamina(InfiniteStamina.Checked);
            };
            HealPlayer.Activated += (sender, e) => PlayerFunctions.HealPlayer();
            ClearWanted.Activated += (sender, args) =>PlayerFunctions.ClearWantedLevel();
            CleanPlayer.Activated += (sender, args) => PlayerFunctions.CleanPed();
            PlayerVisibility.CheckboxChanged += (sender, args) =>
            {
                PlayerFunctions.SetPlayerVisible(PlayerVisibility.Checked);
            };
            SetMoney.Activated += (sender, args) =>
            {
                PlayerFunctions.SetPlayerCash();
            };
            SetWantedLevel.ValueChanged += (sender, args) =>
            {
                PlayerFunctions.SetPlayerWantedLevel(SetWantedLevel.Value);
            };
            NeverWanted.CheckboxChanged += (sender, args) =>
            {
                PlayerFunctions.NeverWanted();
            };
        }
        
        public void Tick()
        {
            menuPool.Process();
            if (PlayerFunctions.isInfiniteStaminaOn && InfiniteStamina.Checked)
            {
                PlayerFunctions.InfiniteStamina(InfiniteStamina.Checked);
            } 
        }

        public void OpenMenu()
        {
            if (!menuPool.AreAnyVisible)
            {
                TrainerMenu.Visible = !TrainerMenu.Visible;
            }
        }
    }
}
