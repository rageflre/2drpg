using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GroundItemManager : MonoBehaviour
{
    //Creates a new timer with a 1 second interval
    public Timer processTimer = new Timer(1000);
    //A list of all ground items
    List<GroundItem> groundItems = new List<GroundItem>();

    public List<GroundItem> GetItems()
    {
        return groundItems;
    }

    public void Start()
    {
        processTimer.Elapsed += ProcessTimer_Elapsed;
        processTimer.Enabled = true;
    }

    //This causes issues with printing the data of GetObject()
    private void ProcessTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        //Wont print in this method but will print in the update function
        print("[elapsed]: " + groundItems[0].GetObject().name);
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
                                groundItems.Remove(groundItem);
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
    }

    private void OnDisable()
    {
        processTimer.Dispose();
    }
}
