using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hourglass", menuName = "Shop/Hourglass")]
public class Hourglass : Item
{
    public Hourglass(Sprite _icon, string _itemName, int _price, int _attribute)
    {
        icon = _icon;
        itemName = _itemName;
        price = _price;
        attribute = _attribute;
    }
}
