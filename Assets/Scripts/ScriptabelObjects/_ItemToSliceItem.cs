using UnityEngine;

[CreateAssetMenuAttribute(fileName = "SlicedObject", menuName = "SlicedObject/SlicedItem")]
public class _ItemToSliceItem : ScriptableObject
{
    public GameObject input;
    public GameObject output;
    public float RequiredSliceCount;
    public string InputObjectName;
    public string OutputObjectName;
}
