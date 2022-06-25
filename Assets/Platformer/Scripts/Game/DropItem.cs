using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Transform dropHandler;
    [SerializeField] public GameObject[] dropableItem;
    [SerializeField] private GameObject itemToDrop;

    private void Start()
    {
        // initialise first then rely on Invoke to generate random dropable item. // quick fix but disguting fix
        itemToDrop = dropableItem[0];
    }

    // will determine if drop or not
    private void randomizer()
    {
        if (Random.value >= 0.5)
            randomizerTwo();
        else
            return;
    }

    private void LateUpdate()
    {
        Invoke("randomizer", 10);
    }

    // will determine what drop to choose
    private void randomizerTwo()
    {
        if (Random.value >= 0.5)
            itemToDrop = dropableItem[0];
        else
            itemToDrop = dropableItem[1];
    }

    public void Drop(Transform objectPosition)
    {
        ItemToDrop(objectPosition);
    }

    private void ItemToDrop(Transform objectPosition)
    {
        GameObject droppedItem = Instantiate(itemToDrop, objectPosition.position, objectPosition.rotation, dropHandler);
    }
}