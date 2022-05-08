using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficMovementSystem : MonoBehaviour
{
    Rigidbody2D rb2d;
    CarSpawner carSpawner;
    
    [SerializeField] float carSpeed = 10f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        carSpawner = GetComponentInParent<CarSpawner>();
    }

    void FixedUpdate()
    {
        MoveCar();
    }

    void MoveCar()
    {
        bool isGoingDown = carSpawner.GetIsGoingDown();

        if (!isGoingDown)
        {
            rb2d.MovePosition(new Vector2(transform.position.x, transform.position.y + carSpeed * Time.deltaTime));
        }
        else
        {
            rb2d.MovePosition(new Vector2(transform.position.x, transform.position.y - carSpeed * Time.deltaTime));
        }
    }
}
