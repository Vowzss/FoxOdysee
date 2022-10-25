using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Shop/Key")]
public class Key : Item
{
    public Key(Sprite _icon, string _itemName, int _price, int _attribute)
    {
        icon = _icon;
        itemName = _itemName;
        price = _price;
        attribute = _attribute;
    }
}
