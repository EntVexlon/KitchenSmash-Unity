using UnityEngine;
using System;
using System.Collections.Generic;

public class _AudioGroup : ScriptableObject
{
    public List<AudioCategory> AudioList;

    [Serializable]
    public struct AudioCategory
    {
        public AudioType type;
        public AudioClip[] clip;
    }
}

[HideInInspector] public enum AudioType
{
    pan_sizzle,
}