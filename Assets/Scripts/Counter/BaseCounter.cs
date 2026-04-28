using UnityEngine;

public class BaseCounter : MonoBehaviour , ICounter
{
    public GameObject CurrentCounterItem;
    public  bool CounterHaveItem { get; set; }  = false;
    public  virtual void TryDropItem(GameObject Item) { }
    public  virtual GameObject TryPickUpItem(Player ph) => null;
    public  virtual void InteractAction() { }


}
