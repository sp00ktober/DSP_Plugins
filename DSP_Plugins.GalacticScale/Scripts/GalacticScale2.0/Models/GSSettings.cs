﻿using FullSerializer;
using System;
using System.Collections.Generic;
using UnityEngine;
using Patch = GalacticScale.Scripts.PatchStarSystemGeneration.Bootstrap;

namespace GalacticScale
{
    public class GSSettings
    {
        private static GSSettings instance = new GSSettings();
        [SerializeField]
        private int seed = 1;
        [SerializeField]
        private  List<GSStar> stars = new List<GSStar>();
        [SerializeField]
        private galaxyParams galaxyParams = new galaxyParams();
        public string version = "2.0";
        [NonSerialized]
        public bool imported = false;
        public static int PlanetCount { get => instance.getPlanetCount(); }
        public static int Seed { get => instance.seed; set => instance.seed = value; }
        public static List<GSStar> Stars { get => instance.stars; set => instance.stars = value; }
        public static int starCount { get => Stars.Count; }
        public static GSStar BirthStar { get => Stars[birthStarId]; }
        public static int birthStarId = 0;
        public static int birthPlanetId = 0;
        public static galaxyParams GalaxyParams { get => instance.galaxyParams; set => instance.galaxyParams = value; }
        public static GSThemeLibrary ThemeLibrary { get => instance.themeLibrary; set => instance.themeLibrary = value; }
        [SerializeField]
        private GSThemeLibrary themeLibrary = GS2.ThemeLibrary; //testing...this may break something
        //static GSSettings()
        //{

        //}
        //private GSSettings()
        //{

        //}
        public static GSSettings Instance { get { return instance; } set { instance = value; } }
      
        //public static void set(galaxyParams galaxyParams, int seed, GSStar birthStar, List<GSStar> stars)
        //{
        //    GalaxyParams = galaxyParams;
        //    //birthStarId = birthStar.assignedIndex;
        //    Stars = stars;
        //    Seed = seed;
        //}
        public static void Reset()
        {
            instance = new GSSettings();
            GalaxyParams = new galaxyParams();
            Stars.Clear();
        }
        public int getPlanetCount()
        {
            int count = 0;
            foreach (GSStar star in stars) count+=star.bodyCount;
            GS2.Log("GetPlanetCount = " + count);
            return count;
        }
    }
    public class galaxyParams
    {
        public int iterations = 4;
        public double minDistance = 2;
        public double minStepLength = 2.3;
        public double maxStepLength = 3.5;
        public double flatten = 0.18;
    }
}