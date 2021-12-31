using System;
using MelonLoader;
using Harmony;
using UnityEngine;
using System.Reflection;
using System.Xml.XPath;
using System.Globalization;
using UnhollowerRuntimeLib;
using ModSettings;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace Fireworks
{
	/*[HarmonyLib.HarmonyPatch(typeof(FlareGunRoundItem), "Awake")]
	public class ComponentAdder
	{
		public static void Postfix(ref FlareGunRoundItem __instance)
		{
			BangBang thisBang = __instance.gameObject.AddComponent<BangBang>();

			thisBang.thisRound = __instance;
			thisBang.thisObject = __instance.gameObject;
			thisBang.thisFX = __instance.m_FXGameObject;
			thisBang.roundTransform = __instance.gameObject.transform;

			thisBang.InitBang();
		}
	}*/

	[HarmonyLib.HarmonyPatch(typeof(FlareGunRoundItem), "Fire")]
	public class FireTrigger
	{
		public static void Postfix(ref FlareGunRoundItem __instance)
		{
			if(Settings.options.enableFireworks)
			{
				BangBang thisBang = __instance.gameObject.AddComponent<BangBang>();
			}

			if (Settings.options.enableCustomColors)
			{
				Light flareLight = __instance.m_Light.GetComponent<Light>();
				
				if (!Settings.options.enableRandomLight)
				{
					flareLight.color = new Color(Settings.options.colorLightRed, Settings.options.colorLightGreen, Settings.options.colorLightBlue);
				}
				else
				{
					flareLight.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
				}

				ParticleSystem thisSmoke = __instance.gameObject.GetComponentInChildren<ParticleSystem>();
				//thisSmoke.emission.enabled = false;
				//thisSmoke.Stop();
				


				if (!Settings.options.enableRandomSmoke)
				{					
					thisSmoke.main.startColor = new Color(Settings.options.colorSmokeRed, Settings.options.colorSmokeGreen, Settings.options.colorSmokeBlue);
					__instance.m_SmokeColorWhenExtinguished = new Color(Settings.options.colorSmokeRed, Settings.options.colorSmokeGreen, Settings.options.colorSmokeBlue);
					__instance.m_SmokeCoreColorWhenExtinguished = new Color(Settings.options.colorSmokeRed, Settings.options.colorSmokeGreen, Settings.options.colorSmokeBlue);

				}
				else
				{
					thisSmoke.main.startColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
					__instance.m_SmokeColorWhenExtinguished = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
					__instance.m_SmokeCoreColorWhenExtinguished = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
				}
			}
		}
	}


	[HarmonyLib.HarmonyPatch(typeof(FlareGunRoundItem), "FallFree")]
	public class FlarePatcher
	{
		public static void Postfix(ref FlareGunRoundItem __instance)
		{
			if (Settings.options.enableFireworks)
			{
				BangBang thisBang = __instance.gameObject.GetComponent<BangBang>();

				thisBang.BangNow();
			}
		}
	}

	[HarmonyLib.HarmonyPatch(typeof(FlareGunRoundItem), "OnCollisionEnter")]
	public class FlarePatcher2
	{
		private static void Postfix(ref FlareGunRoundItem __instance, ref Collision collision)
		{
			if (Settings.options.enableFireworks)
			{
				BangBang thisBang = __instance.gameObject.GetComponent<BangBang>();

				thisBang.BangNow();
			}
		}
	}
}