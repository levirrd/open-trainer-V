using GTA;
using GTA.Math;

namespace Open_Trainer_V.Features
{
    public class VehicleFunctions
    {
        private static Ped PlayerChar => Game.Player.Character;
        private static readonly string notInVehicle = "~b~Player is not in a ~r~Vehicle";

        public static void SpawnVehicle()
        {
            string input = Game.GetUserInput("Enter vehicle model");
            if (string.IsNullOrEmpty(input)) return;
            Model vehicleModel = new Model(input);
            if (!vehicleModel.IsValid || !vehicleModel.IsInCdImage)
            {
                GTA.UI.Notification.Show($"~b~Invalid vehicle: ~r~{input}");
                return;
            }
            vehicleModel.Request(500);
            int timeout = 30;
            while (!vehicleModel.IsLoaded && timeout > 0)
            {
                Script.Wait(100);
                timeout--;
            }
            if (!vehicleModel.IsLoaded)
            {
                GTA.UI.Notification.Show("~r~Failed to load vehicle model.");
                return;
            }
            Vector3 spawnPos = PlayerChar.Position + PlayerChar.ForwardVector * 5f;
            Vehicle vehicle = World.CreateVehicle(vehicleModel, spawnPos);
            if (vehicle == null)
            { 
                GTA.UI.Notification.Show("~r~Failed to create vehicle.");
                return;
            }
            vehicle.PlaceOnGround();
            vehicle.MarkAsNoLongerNeeded();
            GTA.UI.Notification.Show($"~b~Spawned: ~g~{input}");
            PlayerChar.SetIntoVehicle(vehicle, VehicleSeat.Driver);
        }
        public static void VehicleGodMode(bool isEnabled)
        {
            if (PlayerChar.IsInVehicle())
            {
                Vehicle currVehicle = PlayerChar.CurrentVehicle;
                currVehicle.IsInvincible = isEnabled;
                currVehicle.CanBeVisiblyDamaged = !isEnabled;
                currVehicle.CanTiresBurst = !isEnabled;
                currVehicle.CanEngineDegrade = !isEnabled;
                currVehicle.CanWheelsBreak = !isEnabled;
                string statusText = isEnabled ? "~g~Enabled" : "~r~Disabled";
                GTA.UI.Notification.Show($"~b~Vehicle God Mode: {statusText}");
            }
            else GTA.UI.Notification.Show($"{notInVehicle}");
        }
        public static void FixVehicle()
        {
            if (PlayerChar.IsInVehicle())
            {
                Vehicle currVehicle = PlayerChar.CurrentVehicle;
                currVehicle.Repair();
                currVehicle.Wash();
                GTA.UI.Notification.Show("~b~Vehicle has been repaired + cleaned.");
            }
            else GTA.UI.Notification.Show($"{notInVehicle}");
        }
        public static void CleanVehicle()
        {
            if (PlayerChar.IsInVehicle())
            {
                Vehicle currVehicle = PlayerChar.CurrentVehicle;
                currVehicle.Wash();
                GTA.UI.Notification.Show("~b~Vehicle has been cleaned.");
                
            }
            else GTA.UI.Notification.Show($"{notInVehicle}");
        }

        public static void DeleteCurrentVehicle()
        {
            Vehicle currVehicle = PlayerChar.CurrentVehicle;
            if (currVehicle != null)
            {
                currVehicle.Delete();
                GTA.UI.Notification.Show("~b~Vehicle has been deleted.");
            }
            else GTA.UI.Notification.Show($"{notInVehicle}");
        }
    }
}