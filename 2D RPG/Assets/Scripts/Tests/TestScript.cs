using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public class Data
    {
        public GameObject gObject;

        public int id;

        public int amount;

        public Data(GameObject gObject, int id, int amount = 1)
        {
            this.gObject = gObject;
            this.id = id;
            this.amount = amount;
        }
    }

    public Timer timer = new Timer(1000);

    public List<Data> testList = new List<Data>();

    void Start()
    {
        for (int index = 0; index < 10; index++)
            testList.Add(new Data(gameObject, index));

        timer.Elapsed += Timer_Elapsed;
        timer.Enabled = true;
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        //Does not work
        Debug.Log("[Elapsed] " + testList[0].gObject);
    }

    void Update()
    {
        //Works
        Debug.Log("[Update] " + testList[0].gObject);
    }
}