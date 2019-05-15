using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Action<int> onClick = delegate { };

    public void CollectItem(int amount = 1)
    {
        onClick(amount);
    }

    [SerializeField]
    public GroundItem groundItem;

    [SerializeField]
    GameObject test;

    private void Start()
    {
        //test = new GameObject()

        groundItem = new GroundItem(new Item(1), gameObject, true);
        GameManager.GetGroundItems().GetItems().Add(groundItem);
    }
}
