using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairMovement : MonoBehaviour
{
    [SerializeField]
    bool debugMouse = false;
    [SerializeField]
    Text mousePosition;
    [SerializeField]
    Text crosshairEnabled;
    [SerializeField]
    Text distance;
    [SerializeField]
    GameObject crosshair;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    void Start()
    {
        //Initialze the text component for the mouse position
        mousePosition = GameObject.Find("Mouse position").GetComponent<Text>();
        //Initialze the text component for the crosshair enabled
        crosshairEnabled = GameObject.Find("Crosshair enabled").GetComponent<Text>();
        //Initialze the text component for the distance to player
        distance = GameObject.Find("Distance").GetComponent<Text>();
        //Initialze the game object for the crosshair
        crosshair = transform.GetChild(0).gameObject;
        //Initialze the sprite renderer for the crosshair
        spriteRenderer = crosshair.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveCrosshair();
    }

    void MoveCrosshair()
    {
        //The mouse position, it converts your mouse position to world point
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Calculates the distance between the player and the mouse position
        float mouseDistance = Vector3.Distance(mousePos, transform.position) - 10;
        //Sets the position of the crosshair to the mouse position
        crosshair.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        //Checks if the distance of the mouse is higher then 10.1 if so dont show the crosshair
        spriteRenderer.enabled = mouseDistance < 0.1f;
        //Checks if the mouse debug is enabled
        if (debugMouse)
        {
            mousePosition.text = "Mouse position: " + mousePos.ToString();
            crosshairEnabled.text = "Crosshair enabled: " + spriteRenderer.enabled;
            distance.text = "Distance to player: " + mouseDistance;
        } else
        {
            mousePosition.text = "";
            crosshairEnabled.text = "";
            distance.text = "";
        }
    }
}
