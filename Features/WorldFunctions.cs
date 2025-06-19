using System;
using System.Collections.Generic;
using GTA;
using LemonUI.Menus;

namespace Open_Trainer_V.Features
{
    public class WorldFunctions
    {
        private static readonly string[] weathers = {
            "Blizzard", "Christmas", "Clear", "Clearing", "Clouds", "ExtraSunny", "Foggy", "Halloween",
            "Neutral", "Overcast", "Raining", "Smog", "Snowing", "Snowlight", "ThunderStorm"
        };

        public static List<NativeItem> CreateWeatherMenuItems()
        {
            var items = new List<NativeItem>();

            foreach (var weatherName in weathers)
            {
                var item = new NativeItem(weatherName);
                item.Activated += (sender, args) =>
                {
                    if (Enum.TryParse(weatherName, out Weather weatherEnum))
                    {
                        World.Weather = weatherEnum;
                        GTA.UI.Notification.Show($"Weather set to: {weatherName}");
                    }
                    else
                    {
                        GTA.UI.Notification.Show($"~r~Invalid weather: {weatherName}");
                    }
                };
                items.Add(item);
            }

            return items;
        }
        public static void FreezeTime(bool isEnabled)
        {
            World.IsClockPaused = isEnabled;
            PlayerFunctions.ShowStatus("Freeze Time: ",  isEnabled);
        }

        public static void SetHour(int hour)
        {
            TimeSpan currentTime = GTA.World.CurrentTimeOfDay;
            TimeSpan updatedTime = new TimeSpan(hour, currentTime.Minutes, currentTime.Seconds);
            GTA.World.CurrentTimeOfDay = updatedTime;
            GTA.UI.Notification.Show("Set Time: " + GTA.World.CurrentTimeOfDay.Hours.ToString());
        }
        public static void SetMinutes(int minutes)
        {
            TimeSpan currentTime = GTA.World.CurrentTimeOfDay;
            TimeSpan updatedTime = new TimeSpan(currentTime.Hours,minutes, currentTime.Seconds);
            GTA.World.CurrentTimeOfDay = updatedTime;
            GTA.UI.Notification.Show("Set Time: " + GTA.World.CurrentTimeOfDay.Minutes.ToString());
        }
        public static void SetSeconds(int seconds)
        {
            TimeSpan currentTime = GTA.World.CurrentTimeOfDay;
            TimeSpan updatedTime = new TimeSpan(currentTime.Hours,currentTime.Minutes, seconds);
            GTA.World.CurrentTimeOfDay = updatedTime;
            GTA.UI.Notification.Show("Set Time: " + GTA.World.CurrentTimeOfDay.Seconds.ToString());
        }

        public static void SetLowGravity(bool isEnabled)
        {
            if (isEnabled) GTA.World.GravityLevel = 4;
            else GTA.World.GravityLevel = 0;
        }
    }
}