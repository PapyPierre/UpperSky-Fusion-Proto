using System;
using System.Collections.Generic;
using Fusion;
using NaughtyAttributes;
using UnityEngine;

namespace World
{
    public class WorldManager : MonoBehaviour
    {
        public static WorldManager instance;

        public Dictionary<PlayerRef, int> playersIslandsCount;
        
        [field: SerializeField]
        public int MaxBuildingsPerIslands { get; private set; }

        public IslandTypesClass[] islandTypes;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError(name);
                return;
            }
        
            instance = this;
        }
    }

    public enum IslandTypesEnum
    {
        Starting,
        Basic,
        Living,
        Mythic,
        JungleSanctuary,
        DesertSanctuary,
        MontainSanctuary
    }
    
    [Serializable]
    public class IslandTypesClass
    {
        public IslandTypesEnum type;
        public Gradient colorGradient;
        [MinMaxSlider(0,100)] public Vector2 rarity;
    }
}
