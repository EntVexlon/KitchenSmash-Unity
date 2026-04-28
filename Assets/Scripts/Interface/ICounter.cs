using UnityEngine;

public interface ICounter
{
    GameObject TryPickUpItem(Player ph);
    void TryDropItem(GameObject Item);
    //void TryPlaceItem(GameObject Item);
    bool CounterHaveItem { get; set; }
    //GameObject CurrentCounterItem { get; set; }

    void InteractAction();
    // I Should be do in Better way uhh any way..

    bool TryAddIngredientToPlate(GameObject Item, Object_Plate PlateObject);
    GameObject CurrentCounterItem { get; set; }


}