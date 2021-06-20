﻿using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GalacticScale
{
    public partial class PatchOnUIGalaxySelect : MonoBehaviour
    {
        public static Delayer delayer;
        public class Delayer : MonoBehaviour
        {
            public bool active = false;
            public bool mouseDown = false;
            public void Update()
            {
                mouseDown = Input.GetMouseButton(0);
            }
            public void Wait()
            {
                if (!active)
                {
                    active = true;
                    StartCoroutine(WaitAWhile());
                    StartCoroutine(WaitUntilMouseUp());
                }
            }
            public IEnumerator WaitAWhile()
            {
                yield return new WaitForSecondsRealtime(1f);
                if (active)
                {
                    UIRoot.instance.galaxySelect.SetStarmapGalaxy();
                    active = false;
                }
            }
            public IEnumerator WaitUntilMouseUp()
            {
                yield return new WaitUntil(() => !mouseDown);
                if (active)
                {
                    UIRoot.instance.galaxySelect.SetStarmapGalaxy();
                    active = false;
                }
            }
        }
        [HarmonyPrefix, HarmonyPatch(typeof(UIGalaxySelect), "OnStarCountSliderValueChange")]
        public static bool OnStarCountSliderValueChange(UIGalaxySelect __instance, ref Slider ___starCountSlider,
            ref GameDesc ___gameDesc, float val)
        {
            if (delayer == null)
            {
                delayer = ___starCountSlider.gameObject.AddComponent<Delayer>();
            }
            delayer.Wait();
            var num = (int)(val + 0.1f);
            if (num == ___gameDesc.starCount)
            {
                return false;
            }
            __instance.starCountText.text = num.ToString();
            num = Mathf.Clamp(num, GS2.generator.Config.MinStarCount, GS2.generator.Config.MaxStarCount);
            ___gameDesc.starCount = num;
            GS2.gameDesc = ___gameDesc;

            return false;
        }
    }
}