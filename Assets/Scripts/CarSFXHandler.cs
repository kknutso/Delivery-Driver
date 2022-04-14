using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSFXHandler : MonoBehaviour
{
    [Header("Audio sources")]
    [SerializeField] AudioSource engineAudioSource;
    [SerializeField] AudioSource carHitAudioSource;
    [SerializeField] AudioSource packagePickUpAudioSource;
    [SerializeField] AudioSource packageDeliverAudioSource;

    float desiredEnginePitch = 0.5f;

    PlayerController playerController;
    DeliverySystem deliverySystem;

    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        deliverySystem = GetComponentInParent<DeliverySystem>();
    }

    void Update()
    {
        UpdateEngineSFX();
    }

    void UpdateEngineSFX()
    {
        //Handle engine SFX
        float velocityMagnitude = playerController.GetVelocityMagnitude();

        //Increase the engine volume as the car goes faster
        float desiredEngineVolume = velocityMagnitude * 0.05f;

        //But keep a minimum level so it plays even if the car is idle
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        //To add more variation to the engine sound we also change the pitch
        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Get the relative velocity of the collision
        float relativeVelocity = collision.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;

        carHitAudioSource.pitch = Random.Range(0.95f, 1.05f);
        carHitAudioSource.volume = volume;

        if (!carHitAudioSource.isPlaying)
            carHitAudioSource.Play();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool hasPackage = deliverySystem.GetHasPackage();
            Debug.Log(hasPackage);

        if (collision.tag == "Package")
        {
            if (!packagePickUpAudioSource.isPlaying)
                packagePickUpAudioSource.Play();
        }

        if (collision.tag == "Customer" && hasPackage)
        {
            if (!packageDeliverAudioSource.isPlaying)
                packageDeliverAudioSource.Play();
        }
    }
}
