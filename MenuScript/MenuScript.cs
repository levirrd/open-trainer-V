using GTA.UI;
using LemonUI;
using LemonUI.Menus;
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
        private readonly NativeItem ClearWanted;
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
            //add items to menus or submenus
            PlayerOptions.Add(ClearWanted);
            VehicleOptions.Add(FixVehicle);
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
