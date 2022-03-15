using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageAndCustomerSpawner : MonoBehaviour
{
    DeliverySystem checkHoldingPackage;
    TargetIndicator target;

    bool hasPackageSpawned;
    bool hasCustomerSpawned;

    GameObject currentPackageSpawnPos = null;
    GameObject currentCustomerSpawnPos = null;

    [SerializeField] GameObject playerPosition;
    [SerializeField] float distanceFromPlayer = 20;
    [SerializeField] GameObject[] packagePrefabs;
    [SerializeField] GameObject[] packageSpawnLocations;
    [SerializeField] GameObject[] customerPrefabs;
    [SerializeField] GameObject[] customerSpawnLocations;

    void Start()
    {
        checkHoldingPackage = FindObjectOfType<DeliverySystem>();
        target = FindObjectOfType<TargetIndicator>();
        currentPackageSpawnPos = packageSpawnLocations[0];
        currentCustomerSpawnPos = customerSpawnLocations[0];
    }

    void Update()
    {
        SpawnPackageAndCustomer(packagePrefabs, currentPackageSpawnPos, packageSpawnLocations, true);
        SpawnPackageAndCustomer(customerPrefabs, currentCustomerSpawnPos, customerSpawnLocations, false);
        SetTarget();
    }

    GameObject DetermineSpawnLocation(GameObject currentSpawnPos, GameObject[] spawnLocations)
    {
        GameObject tempSpawnPos;
        GameObject spawnPos;

        do
        {
            int randomNum = Random.Range(0, spawnLocations.Length);
            tempSpawnPos = spawnLocations[randomNum];
            Debug.Log("Temp: " + tempSpawnPos);
            Debug.Log("Current:" + currentSpawnPos);
        } while (tempSpawnPos.transform.position == currentSpawnPos.transform.position);

            spawnPos = tempSpawnPos;

        return spawnPos;
    }

    GameObject CheckDistanceToPlayer(GameObject currentSpawnPos, GameObject[] spawnLocations)
    {
        GameObject spawnPoint;

        spawnPoint = DetermineSpawnLocation(currentSpawnPos, spawnLocations);
        float distanceBetweenPlayerAndObject = Vector3.Distance(currentSpawnPos.transform.position, playerPosition.transform.position);

        if (distanceBetweenPlayerAndObject < distanceFromPlayer)
        {
            spawnPoint = DetermineSpawnLocation(currentSpawnPos, spawnLocations);
        }

        return spawnPoint;
    }

    void SpawnPackageAndCustomer(GameObject[] objectsPrefabs, GameObject currentSpawnPos, GameObject[] spawnLocations, bool isPackage)
    {
        if (!hasPackageSpawned || !hasCustomerSpawned)
        {
            bool hasPackage = checkHoldingPackage.GetHasPackage();
            int randomPrefab = Random.Range(0, objectsPrefabs.Length);
            GameObject spawnPoint = CheckDistanceToPlayer(currentSpawnPos, spawnLocations);

            if (isPackage)
            {
                if (!hasPackage && !hasPackageSpawned)
                {
                    currentPackageSpawnPos = spawnPoint;
                    Instantiate(objectsPrefabs[randomPrefab], spawnPoint.transform.position, spawnPoint.transform.rotation);
                    hasPackageSpawned = true;
                }
            }
            else
            {
                if (hasPackageSpawned && !hasCustomerSpawned)
                {
                    currentCustomerSpawnPos = spawnPoint;
                    Instantiate(objectsPrefabs[randomPrefab], spawnPoint.transform.position, spawnPoint.transform.rotation);
                    hasCustomerSpawned = true;
                }
            }
        }
    }

    void SetTarget()
    {
        bool hasPackage = checkHoldingPackage.GetHasPackage();

        Debug.Log("The current package spawn point is " + currentPackageSpawnPos);
        Debug.Log("The current customer spawn point is " + currentCustomerSpawnPos);

        if (!hasPackage)
        {
            if (currentPackageSpawnPos != null)
            {
                target.SetTarget(currentPackageSpawnPos);
            }
        }
        else
        {
            if (currentCustomerSpawnPos != null)
            {
                target.SetTarget(currentCustomerSpawnPos);
            }
        }
    }

    public void SetHasPackageSpawned()
    {
        hasPackageSpawned = false;
    }

    public void SetHasCustomerSpawned()
    {
        hasCustomerSpawned = false;
    }
}
