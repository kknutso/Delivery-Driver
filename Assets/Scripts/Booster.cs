using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    PlayerController player;
    [SerializeField] float boosterForce = 50;
    [SerializeField] bool isNotPointingUp;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isNotPointingUp)
        {
            player.IncreaseForce(-boosterForce);
        }
        else
        {
            player.IncreaseForce(boosterForce);
        }
    }
}
