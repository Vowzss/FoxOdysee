using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public int price;

    public int attribute;
}
