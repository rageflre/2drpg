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

        };
    }
}
