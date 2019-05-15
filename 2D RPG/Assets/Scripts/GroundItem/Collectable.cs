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
    GameObject test;

    private void Start()
    {

    }
}
