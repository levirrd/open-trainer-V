using System;
using System.Runtime.Remoting.Channels;
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
        private readonly NativeItem HealPlayer;
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
            
            FixVehicle = new NativeItem("Fix Vehicle", "Repairs and cleans the vehicle");
            ClearWanted = new NativeItem("Clear Wanted", "Clears Wanted Level");
            GodMode = new NativeCheckboxItem("God Mode", "Sets God Mode",false);
            HealPlayer = new NativeItem("Heal Player", "Heals the player (armor+health)");
            //add items to menus or submenus
            PlayerOptions.Add(ClearWanted);
            PlayerOptions.Add(GodMode);
            PlayerOptions.Add(HealPlayer);
            VehicleOptions.Add(FixVehicle);
            //funcs
            GodMode.CheckboxChanged += (sender, e) => 
            {
               if(GodMode.Checked) PlayerFunctions.ToggleGodMode(true);
               else PlayerFunctions.ToggleGodMode(false);
            };

            HealPlayer.Activated += (sender, e) => PlayerFunctions.HealPlayer();
            ClearWanted.Activated += (sender, args) =>PlayerFunctions.ClearWantedLevel();
            
        }
        
        public void Tick()
        {
            menuPool.Process();
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
