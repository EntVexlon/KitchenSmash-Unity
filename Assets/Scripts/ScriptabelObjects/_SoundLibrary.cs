using System;
using System.Collections.Generic;
using UnityEngine;

public class _SoundLibrary : ScriptableObject
{
    [SerializeField] public List<SoundProfile> SoundList;

    [Serializable]
    public struct SoundProfile
    {
        public SoundId Id;
        public AudioClip[] Clip;
    }
}

[HideInInspector]
public enum SoundId
{
    Footstep,
    PickUp_Item,
    Drop_Item,
    Item_Cut,
    PanSizzle,
    CorrectDelivery,
    WrongDelivery,
    Warning,
    TrashBin,
}
