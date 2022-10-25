using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Topaz", menuName = "Shop/Topaz")]
public class Topaz : Item
{
    public Topaz(Sprite _icon, string _itemName, int _price, int _attribute)
    {
        icon = _icon;
        itemName = _itemName;
        price = _price;
        attribute = _attribute;
    }
}