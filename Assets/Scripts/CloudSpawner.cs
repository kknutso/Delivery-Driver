using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] cloudPrefabs;

    float randomTimeInSeconds;
    bool isLooping = true;

    void Start()
    {
        StartCoroutine(SpawnCarsWithDelay());
    }

    void Update()
    {
        randomTimeInSeconds = Random.Range(30, 45);
    }

    IEnumerator SpawnCarsWithDelay()
    {
        do
        {
            yield return new WaitForSeconds(randomTimeInSeconds);
            int randomCloud = Random.Range(0, cloudPrefabs.Length);

            GameObject cloud = Instantiate(cloudPrefabs[randomCloud], transform.position, Quaternion.Euler(180, 0, 0));
            cloud.transform.SetParent(this.transform);
        } while (isLooping);
    }
}
