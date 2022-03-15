using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerController playerController;
    bool canMove = true;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (canMove)
        {
            Vector2 inputVector = Vector2.zero;

            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            playerController.SetInputVector(inputVector);
        }
    }

    public void StopPlayerMovement()
    {
        canMove = false;
    }
}
