using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTest : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    bool crosshairEnabled;
    [SerializeField]
    public static Action<CrosshairTest> OnClick = delegate { };
    [SerializeField]
    private List<Hittable> hittablesInRange = null;
    [SerializeField]
    private List<Collectable> collectablesInRange = null;

    void AddHittable(Collider2D other)
    {
        //Grabs the hitable component from the object you collided with
        Hittable hittable = other.GetComponent<Hittable>();
        //Checks if its not null & the crosshair is enabled
        if (null != hittable && crosshairEnabled)
        {
            //Checks if this hitable already exists in the list
            if (hittablesInRange.Contains(hittable)) return;
            //Adds the hittable to the list
            hittablesInRange.Add(hittable);
        }
    }

    void RemoveHittable(Collider2D other)
    {
        //Grabs the hitable component from the object you collided with
        Hittable hittable = other.GetComponent<Hittable>();
        //Checks if its not null
        if (null != hittable)
            //Removes the hittable from the list
            hittablesInRange.Remove(hittable);
    }

    void AddCollectacble(Collider2D other)
    {
        //Grabs the collectable component from the collided object
        Collectable collectable = other.GetComponent<Collectable>();
        //Checks if collectable isnt null & the crosshair is enabled
        if (null != collectable && crosshairEnabled)
        {
            //Checks if the collectable doesnt already exist in the list
            if (collectablesInRange.Contains(collectable)) return;
            //Adds the collectable to the list
            collectablesInRange.Add(collectable);
        }
    }

    void RemoveCollectable(Collider2D other)
    {
        //Grabs the collectable component from the collided object
        Collectable collectable = other.GetComponent<Collectable>();
        //Checks if the collectable isnt null
        if (null != collectable)
            //Removes the collectable from the list
            collectablesInRange.Remove(collectable);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        hittablesInRange = new List<Hittable>();

        collectablesInRange = new List<Collectable>();
    }

    void Update()
    {
        crosshairEnabled = spriteRenderer.enabled;
        if (Input.GetKeyDown(KeyCode.Space) && crosshairEnabled)
        {
            OnClick(this);
            // Might cause problems! Multithreading..
            foreach (Hittable hittable in hittablesInRange.ToArray())
                hittable.ReceiveHit(1);
            // Might cause problems! Multithreading..
            foreach (Collectable collectable in collectablesInRange.ToArray())
                collectable.CollectItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AddHittable(other);
        AddCollectacble(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        AddHittable(other);
        AddCollectacble(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        RemoveHittable(other);
        RemoveCollectable(other);
    }
}
