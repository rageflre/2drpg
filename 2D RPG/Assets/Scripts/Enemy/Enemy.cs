using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    int maxHealth;
    [SerializeField]
    int currentHealth;

    private void Start()
    {
        //??
        GetComponent<Hittable>().OnHit += damage =>
        {
            if (--currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        };
    }

}
