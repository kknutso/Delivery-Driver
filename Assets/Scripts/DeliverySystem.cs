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

    [SerializeField] int packageLimit = 10;
    int packagesDelivered;

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
        LoseLevel();       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Package" && !hasPackage)
        {
            Debug.Log("You picked up a package!");
            Destroy(collision.gameObject, destroyDelay);
            spriteRenderer.color = hasPackageColor;
            hasPackage = true;
        }

        if (collision.tag == "Customer" && hasPackage)
        {
            Debug.Log("You delivered the package.");
            Destroy(collision.gameObject, destroyDelay);
            spriteRenderer.color = noPackageColor;
            hasPackage = false;
            spawner.SetHasCustomerSpawned();
            spawner.SetHasPackageSpawned();
            packagesDelivered++;
            WinLevel();
        }
    }

    void WinLevel()
    {
        if(packagesDelivered >= packageLimit)
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
