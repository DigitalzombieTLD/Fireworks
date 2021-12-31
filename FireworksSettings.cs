using System.IO;
using System.Reflection;
using UnityEngine;
using ModSettings;

namespace Fireworks
{
    internal class FireworksSettingsMain : JsonModSettings
    {          
		[Section("General")]

		[Name("Enable fireworks")]
		[Description("Enable Fireworks")]
		public bool enableFireworks = true;

		[Name("Destroy original flare")]
		[Description("Hides the original flare projectile / glow")]
		public bool disableVanillaFlare = true;

		[Name("Fuse length")]
		[Description("Time until explosion; Default: 3")]
		[Slider(0, 6)]
		public float ignitionTimer = 3;

		[Name("Effect timer")]
		[Description("Time until fireworks effect disappears again")]
		[Slider(0, 180)]
		public int timeToLive = 20;


		[Section("WIP: Flare colors - Only works while flare is on the ground")]

		[Name("Enable custom options")]
		[Description("Custom color options")]
		public bool enableCustomColors = true;

		[Section("Light options")]

		[Name("Enable random color")]
		[Description("Random color")]
		public bool enableRandomLight = false;

		[Name("Red")]
		[Slider(0, 1f)]
		public float colorLightRed = 1;

		[Name("Green")]
		[Slider(0, 1f)]
		public float colorLightGreen = 0;

		[Name("Blue")]
		[Slider(0, 1f)]
		public float colorLightBlue = 0;

		[Section("WIP: Smoke options - Not really working yet")]

		[Name("Enable random color")]
		[Description("Random color")]
		public bool enableRandomSmoke = false;

		[Name("Red")]
		[Slider(0, 1f)]
		public float colorSmokeRed = 1;

		[Name("Green")]
		[Slider(0, 1f)]
		public float colorSmokeGreen = 0;

		[Name("Blue")]
		[Slider(0, 1f)]
		public float colorSmokeBlue = 0;


		protected override void OnConfirm()
        {
            base.OnConfirm();			
		}

		protected override void OnChange(FieldInfo field, object oldValue, object newValue)
		{
			
		}

	}

    internal static class Settings
    {
        public static FireworksSettingsMain options;

        public static void OnLoad()
        {
            options = new FireworksSettingsMain();
            options.AddToModSettings("Fireworks");
		}
    }
}
