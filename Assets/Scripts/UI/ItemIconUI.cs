using UnityEngine;
using UnityEngine.UI;

public class ItemIconUI : MonoBehaviour
{
    //For Plate Item IconUI
    [SerializeField] private Transform IconTemplate;
    [SerializeField] private LayerMask ItemIconLayer;


    private void Awake() => IconTemplate.gameObject.SetActive(false);
    public void SetItemIcon(_BaseItem _item) {
        Transform IconUI = Instantiate(IconTemplate, transform);
        //I know this is not the efficient way  
        foreach (Transform child in IconUI.GetComponentsInChildren<Transform>())
            if ( ((1 << child.gameObject.layer) & ItemIconLayer) != 0 )
                child.GetComponent<Image>().sprite = _item.Icon;
        IconUI.gameObject.SetActive(true);
    }
}
