using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    static GameObject player;

    public static GameObject GetPlayer()
    {
        return player;
    }

    [SerializeField]
    static Inventory inventory;

    public static Inventory GetInventory()
    {
        return inventory;
    }

    [SerializeField]
    static GroundItemManager groundItems;

    public static GroundItemManager GetGroundItems()
    {
        return groundItems;
    }

    void Awake()
    {
        player = GameObject.Find("Player");

        inventory = player.GetComponent<Inventory>();

        groundItems = GetComponent<GroundItemManager>();
    }
}
