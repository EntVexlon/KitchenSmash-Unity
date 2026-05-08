using System;
using System.Collections.Generic;
using UnityEngine;


public class _SoundEffectGroup : ScriptableObject
{
    [SerializeField] public List<SfxCategory> SoundEffectList;

    [Serializable]
    public struct SfxCategory
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
    FootStep,
    Stove_Sizzel,
    CorrectDelivery,
    WrongDelivery,
    Warning,
    TrashBin
}
