using UnityEngine;

[CreateAssetMenuAttribute(menuName = "KitchenObject/CutItem", order = 2)]

public class _CutItem : ScriptableObject
{
    public GameObject input;
    public GameObject output;
    public float RequiredSliceCount;
    public string InputObjectName;
    public string OutputObjectName;
}
