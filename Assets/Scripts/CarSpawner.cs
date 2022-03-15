using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] bool isGoingDown;
    [SerializeField] GameObject[] carPrefabs;

    float randomTimeInSeconds; 
    bool isLooping = true;

    void Start()
    {
        StartCoroutine(SpawnCarsWithDelay());
    }

    void Update()
    {
        randomTimeInSeconds = Random.Range(3, 5);
    }

    IEnumerator SpawnCarsWithDelay()
    {
        do
        {
            yield return new WaitForSeconds(randomTimeInSeconds);
            int randomCar = Random.Range(0, carPrefabs.Length);

            if (!isGoingDown)
            {
                GameObject car = Instantiate(carPrefabs[randomCar], transform.position, transform.rotation);
                car.transform.SetParent(this.transform);
            }
            else
            {
                GameObject car = Instantiate(carPrefabs[randomCar], transform.position, Quaternion.Euler(180, 0, 0));
                car.transform.SetParent(this.transform);
            }
        } while (isLooping);
    }

    public bool GetIsGoingDown()
    {
        return isGoingDown;
    }
}
