using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageHandler : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    PackageAndCustomerSpawner spawner;
    DeliverySystem deliverySystem;
    bool hasPackage;

    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] float destroyDelay = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawner = FindObjectOfType<PackageAndCustomerSpawner>();
        deliverySystem = FindObjectOfType<DeliverySystem>();
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
            deliverySystem.SetPackagesDelivered(1);
            deliverySystem.WinLevel();
        }
    }

    public bool GetHasPackage()
    {
        return hasPackage;
    }
}
