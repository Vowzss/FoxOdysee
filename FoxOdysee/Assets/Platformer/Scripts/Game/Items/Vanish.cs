using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vanish", menuName = "Shop/Vanish")]
public class Vanish : Item
{
    public Vanish(Sprite _icon, string _itemName, int _price, int _attribute)
    {
        icon = _icon;
        itemName = _itemName;
        price = _price;
        attribute = _attribute;
    }
}
