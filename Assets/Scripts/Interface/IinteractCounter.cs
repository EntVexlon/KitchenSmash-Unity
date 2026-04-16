using UnityEngine;

public interface IinteractCounter
{
    GameObject TryPickUpItem(Player ph);
    void TryPlaceItem(GameObject Item);
    //void TryPlaceItem(GameObject Item);
    bool CounterHaveItem { get; set; }
    GameObject CurrentCounterItem { get; set; }

    void InteractAction();
    // I Should be do in Better way uhh any way..

}