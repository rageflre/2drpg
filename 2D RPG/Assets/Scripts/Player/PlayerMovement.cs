using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float speed = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        //Checks if the console is opened
        if (Console.consoleOpened) return;
        //Handles the regular movement of the player
        rb.velocity = new Vector2(GameManager.GetInput().horizontalMovement * speed, GameManager.GetInput().verticalMovement * speed);
    }
}
