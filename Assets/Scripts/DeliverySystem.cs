using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySystem : MonoBehaviour
{
    PackageAndCustomerSpawner spawner;
    bool hasPackage;

    [SerializeField] float destroyDelay = 0.5f;

    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);

    [SerializeField] int packagesToBeDelivered;
    [SerializeField] Image[] packages;

    [SerializeField] Canvas winLabel;
    [SerializeField] Canvas loseLabel;

    SpriteRenderer spriteRenderer;
    PlayerInputHandler player;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerInputHandler>();
        spawner = FindObjectOfType<PackageAndCustomerSpawner>();
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.GetFillFraction();
        DisplayPackages();
        LoseLevel();       
    }

    void DisplayPackages()
    {
        for (int i = 0; i < packages.Length; i++)
        {
            if(i < packagesToBeDelivered)
            {
                packages[i].enabled = true;
            }
            else
            {
                packages[i].enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Package" && !hasPackage)
        {
            Destroy(collision.gameObject, destroyDelay);
            spriteRenderer.color = hasPackageColor;
            hasPackage = true;
        }

        if (collision.tag == "Customer" && hasPackage)
        {
            Destroy(collision.gameObject, destroyDelay);
            spriteRenderer.color = noPackageColor;
            hasPackage = false;
            spawner.SetHasCustomerSpawned();
            spawner.SetHasPackageSpawned();
            packagesToBeDelivered--;
            WinLevel();
        }
    }

    void WinLevel()
    {
        if(packagesToBeDelivered <= 0)
        {
            player.StopPlayerMovement();
            timer.CancelTimer();
            winLabel.gameObject.SetActive(true);
        }
    }

    void LoseLevel()
    {
        float timeLeft = timer.GetTimerValue();

        if (timeLeft <= 0)     
        {
            if (winLabel.gameObject.activeSelf == false)
            {
                player.StopPlayerMovement();
                loseLabel.gameObject.SetActive(true);
            }
        }
    }

    public bool GetHasPackage()
    {
        return hasPackage;
    }
}
