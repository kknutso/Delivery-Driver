using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    void Update()
    {
        MoveCloud();
    }

    void MoveCloud()
    {
        float randomSpeed = Random.Range(15, 25);
        float cloudMoveSpeed = randomSpeed * Time.deltaTime;

        this.transform.Translate(cloudMoveSpeed, 0f, 0f);
    }
}
