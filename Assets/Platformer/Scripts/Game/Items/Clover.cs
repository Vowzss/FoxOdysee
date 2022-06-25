using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clover", menuName = "Shop/Clover")]
public class Clover : Item
{
    public Clover(Sprite _icon, string _itemName, int _price, int _attribute)
    {
        icon = _icon;
        itemName = _itemName;
        price = _price;
        attribute = _attribute;
    }
}
