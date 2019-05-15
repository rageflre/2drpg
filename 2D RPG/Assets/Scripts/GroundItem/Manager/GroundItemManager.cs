using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GroundItemManager : MonoBehaviour
{
    //Checks if an update is required
    bool update;
    //Creates a new timer with a 1 second interval
    public Timer processTimer = new Timer(1000);
    //A list of all ground items
    List<GroundItem> groundItems = new List<GroundItem>();

    public List<GroundItem> GetItems()
    {
        return groundItems;
    }

    public void Add(GroundItem groundItem)
    {
        groundItems.Add(groundItem);
        //TODO Spawn the gameobject
        
    }

    public void Remove(GroundItem groundItem, GameObject gObject)
    {

        GameManager.GetInventory().Add(groundItem.GetItem());
        groundItems.Remove(groundItem);
        Destroy(gObject);
    }

    public void Pickup(GroundItem groundItem)
    {

    }

    public void Start()
    {
        processTimer.Elapsed += ProcessTimer_Elapsed;
        processTimer.Enabled = true;
    }

    void Update()
    {
        //Checks if an update is required
        if (update)
        {
            List<GroundItem> toRemove = new List<GroundItem>();
            foreach (GroundItem groundItem in groundItems)
            {
                //Checks if the ground item is null
                if (groundItem == null) continue;
                //Checks if the timer of the object is higher then 0
                if (groundItem.GetTimer() > 0)
                {
                    //Decreases the timer of the item
                    groundItem.DecreaseTimer();
                    //Checks if the time reached 0
                    if (groundItem.GetTimer() == 0)
                    {
                        //Checks for the state of the object
                        switch (groundItem.GetState())
                        {
                            case "PUBLIC":
                                if (groundItem.DoesRespawn() == false)
                                {
                                    Destroy(groundItem.GetObject());
                                    toRemove.Add(groundItem);
                                    continue;
                                }
                                break;

                            case "HIDDEN":
                                groundItem.SetState("PUBLIC");
                                groundItem.SetTimer(120);
                                break;
                        }
                    }
                }
            }
            //Loops though all items that have to be removed from the ground items list
            foreach(GroundItem groundItem in toRemove)
                groundItems.Remove(groundItem);
            toRemove.Clear();
            update = false;
        }
    }

    //This causes issues with printing the data of GetObject()
    private void ProcessTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        update = true;
    }

    private void OnDisable()
    {
        processTimer.Dispose();
    }
}
