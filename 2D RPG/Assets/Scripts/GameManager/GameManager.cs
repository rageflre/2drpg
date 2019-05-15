using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    static InputManager input;

    public static InputManager GetInput()
    {
        return input;
    }

    [SerializeField]
    static GroundItemManager groundItems;

    public static GroundItemManager GetGroundItems()
    {
        return groundItems;
    }

    void Awake()
    {
        input = GetComponent<InputManager>();
        groundItems = GetComponent<GroundItemManager>();
    }
}
