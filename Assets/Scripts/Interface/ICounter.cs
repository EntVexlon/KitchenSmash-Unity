using UnityEngine;

public interface ICounter
{
    GameObject TryPickUpItem(Player ph);
    void TryDropItem(GameObject Item);
    bool CounterHaveItem { get; set; }

    void InteractAction();

    bool TryAddItem(GameObject Item, Object_Plate PlateObject);
    //    ^  TryAddItemToPlate 

    GameObject CurrentCounterItem { get; set; }


}