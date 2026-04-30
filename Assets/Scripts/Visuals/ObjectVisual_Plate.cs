using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisual_Plate : MonoBehaviour
{
    [SerializeField] private Transform PlateTop;  
    [SerializeField] private Object_Plate PlateObject;

    [SerializeField] private List<CompleteItemVisual> ItemList;
    [Serializable] public struct CompleteItemVisual
    {
        public GameObject ItemVisualObject;
        public ScriptableObject ItemScriptableObject;
    }

    private void Update()
    {
        foreach (var visual_item in ItemList)
            foreach (var plate_Item in PlateObject.ObjectList)
            {
                if (plate_Item == visual_item.ItemScriptableObject)
                {
                    ObjectHandler[] Object_Handler = PlateTop.GetComponentsInChildren<ObjectHandler>();
                        foreach(var child in Object_Handler)
                        {
                            if (child._Object == visual_item.ItemScriptableObject)
                            {
                                Destroy(child.gameObject);
                                visual_item.ItemVisualObject.SetActive(true);
                            }
                        }
                }
            }
    }
}
