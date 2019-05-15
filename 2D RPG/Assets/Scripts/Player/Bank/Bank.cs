using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank instance;

    [SerializeField]
    GameObject parent;
    [SerializeField]
    GameObject bagObject;
    [SerializeField]
    ItemBag bank;
    [SerializeField]
    int slots = 120;

    public ItemBag GetBank()
    {
        return bank;
    }

    private void Awake()
    {
        instance = this;
        bank = new ItemBag(new Container(slots, bagObject), parent);
    }

    private void Update()
    {
        if (GameManager.GetInput().bankPressed)
        {
            bank.Open();
        }
    }
}
