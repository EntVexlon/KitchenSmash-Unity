using UnityEngine;

public interface ICounter
{
    GameObject TryPickUpItem(Player ph);
    void TryDropItem(GameObject Item);
    bool CounterHaveItem { get; set; }

    void InteractAction();

    bool TryAddItemToPlate(GameObject Item, Object_Plate PlateObject);
    GameObject CurrentCounterItem { get; set; }


}