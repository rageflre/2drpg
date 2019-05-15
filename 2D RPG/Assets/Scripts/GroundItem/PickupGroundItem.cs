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
            GameManager.GetGroundItems().Pickup(collectable.groundItem);
            //GameManager.GetGroundItems().Remove(collectable.groundItem, gameObject);
        };
    }
}
