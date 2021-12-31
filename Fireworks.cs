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
    public class FireworksMain : MelonMod
    {
		public static AssetBundle FireworksBundle;		
		public static GameObject[] allTheFireworks;

		public static int currentEffectCounter = 0;

		public override void OnApplicationStart()
        {
			ClassInjector.RegisterTypeInIl2Cpp<BangBang>(); 
			Fireworks.Settings.OnLoad();

			FireworksBundle = AssetBundle.LoadFromFile("Mods\\fireworks.unity3d");
			UnityEngine.Object.DontDestroyOnLoad(FireworksBundle);

		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			allTheFireworks = FireworksBundle.LoadAllAssets<GameObject>();

			for (int x = 1; x < allTheFireworks.Length; x++)
			{
				UnityEngine.Object.DontDestroyOnLoad(allTheFireworks[x-1]);
			}
		}

		public override void OnUpdate()
		{
			
			
		}		
	}
}
