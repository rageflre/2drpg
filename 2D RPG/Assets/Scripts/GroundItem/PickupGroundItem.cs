using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGroundItem : MonoBehaviour
{
    [SerializeField]
    Collectable collectable;

    void Start()
    {
        collectable = transform.GetComponent<Collectable>();
        collectable.onClick += amount =>
        {
            int id = int.Parse(transform.name.Substring(7));
            Item item = new Item(id, amount);
            GameManager.GetInventory().Add(item);
            GameManager.GetGroundItems().GetItems().Remove(collectable.groundItem);
            Destroy(gameObject);
        };
    }
}
