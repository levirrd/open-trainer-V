using GTA;
namespace Open_Trainer_V.Features
{
    public class WorldFunctions
    {
        public static void FreezeTime(bool isEnabled)
        {
            World.IsClockPaused = isEnabled;
            PlayerFunctions.ShowStatus("Freeze Time: ",  isEnabled);
        }
    }
}