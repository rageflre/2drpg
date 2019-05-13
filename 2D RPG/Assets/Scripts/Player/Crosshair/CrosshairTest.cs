using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTest : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    public static Action<CrosshairTest> OnClick = delegate { };
    [SerializeField]
    private List<Hittable> hittablesInRange = null;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        hittablesInRange = new List<Hittable>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spriteRenderer.enabled)
        {
            OnClick(this);

            // Might cause problems! Multithreading..
            foreach (Hittable hittable in hittablesInRange.ToArray())
            {
                Collectable collectable = hittable.GetComponent<Collectable>();
                if(collectable != null) collectable.CollectItem();
                else hittable.ReceiveHit(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hittable hittable = other.GetComponent<Hittable>();
        if (null != hittable && spriteRenderer.enabled)
        {
            hittablesInRange.Add(hittable);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Hittable hittable = other.GetComponent<Hittable>();
        if (null != hittable && spriteRenderer.enabled)
        {
            hittablesInRange.Add(hittable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Hittable hittable = other.GetComponent<Hittable>();
        if (null != hittable)
        {
            hittablesInRange.Remove(hittable);
        }
    }
}
