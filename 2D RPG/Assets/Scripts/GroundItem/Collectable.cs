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

    private void Start()
    {
        groundItem = new GroundItem(new Item(1), new Vector2(0, 0), gameObject, false);
        GameManager.GetGroundItems().GetItems().Add(groundItem);
    }
}
