using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crown", menuName = "Shop/Crown")]
public class Crown : Item
{
    public Crown(Sprite _icon, string _itemName, int _price, int _speed)
    {
        icon = _icon;
        itemName = _itemName;
        price = _price;
        attribute = _speed;
    }
}
