using System;
using System.Collections.Generic;
using UnityEngine;


public class _SoundEffectGroup : ScriptableObject
{
    [SerializeField] public List<SoundEffectCategory> SoundEffectList;

    [Serializable]
    public struct SoundEffectCategory
    {
        public SfxType Type;
        public AudioClip[] Clip;
    }
}

[HideInInspector]
public enum SfxType
{
    Item_Cut,
    PickUp_Item,
    Drop_Item,
    CorrectDelivery,
    WrongDelivery,
    Warning,
    Footstep,
    TrashBin
}
