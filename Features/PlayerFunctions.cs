using GTA;
using GTA.Native;
namespace Open_Trainer_V.Features
{
    public static class PlayerFunctions
    {
        public static void ClearWantedLevel()
        {
            Game.Player.WantedLevel = 0;
            GTA.UI.Notification.Show("Wanted level has been cleared.");
            
        }
    }
}