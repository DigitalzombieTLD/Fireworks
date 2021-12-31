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
using System.Linq;

namespace Fireworks
{
	public class BangBang : MonoBehaviour
	{	
		public FlareGunRoundItem thisRound;
		public GameObject thisObject;
		public GameObject thisFX;
		public Transform roundTransform;

		public bool isEffectActive = false;
		public bool flareIsLaunched = false;

		public GameObject activeEffect;

		public float timeToBang = 3;
		public float timeToLive = 10;


		public BangBang(IntPtr intPtr) : base(intPtr) { }

		public void Start()
		{
			InitBang();
		}

		public void Awake()
		{
			InitBang();
		}

		public void InitBang()
		{
			thisRound = this.GetComponent<FlareGunRoundItem>();
						
			thisObject = thisRound.gameObject;
			thisFX = thisRound.m_FXGameObject;
			roundTransform = thisRound.gameObject.transform;

			timeToBang = Settings.options.ignitionTimer;
			timeToLive = Settings.options.timeToLive;

			MelonCoroutines.Start(StartBangTimer());
		}

		public IEnumerator StartBangTimer()
		{
			yield return new WaitForSeconds(timeToBang);

			BangNow();

			yield return null;
		}

		public IEnumerator StartLiveTimer()
		{		
			yield return new WaitForSeconds(timeToLive);

			DestroyBang();

			yield return null;
		}

		public void Update()
		{
			
		}

		public void DestroyOriginalFlare()
		{
			if(Settings.options.disableVanillaFlare)
			{
				thisRound.BurnOut();
				thisFX.SetActive(false);
			}
		}

		public void DestroyBang()
		{
			if(isEffectActive)
			{
				isEffectActive = false;
				UnityEngine.GameObject.Destroy(activeEffect);
			}
		}


		public void BangNow()
		{
			if (!isEffectActive)
			{
				MelonCoroutines.Start(StartLiveTimer());
				DestroyOriginalFlare();

				isEffectActive = true;

				//activeEffect = Instantiate(FireworksMain.allTheFireworks[FireworksMain.currentEffectCounter]);
				activeEffect = Instantiate(FireworksMain.allTheFireworks[UnityEngine.Random.Range(0, FireworksMain.allTheFireworks.Length-1)]);

				activeEffect.transform.position = roundTransform.position;

				if(activeEffect.name.Contains("Eff_"))
				{
					activeEffect.transform.localScale = new Vector3(10f, 10f, 10f);
				}
				else
				{
					activeEffect.transform.localScale = new Vector3(10, 10f, 10f);
				}

				activeEffect.SetActive(true);

				MelonLogger.Msg("Playing: " + activeEffect.name);

				/*FireworksMain.currentEffectCounter++;

				if (FireworksMain.currentEffectCounter >= FireworksMain.allTheFireworks.Length)
				{
					FireworksMain.currentEffectCounter = 0;
				}*/
			}
		}
		
	}
}