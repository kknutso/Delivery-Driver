using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    Transform target = null;
    [SerializeField] float hideDistance;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;

            if (direction.magnitude < hideDistance)
            {
                SetChildrenActive(false);
            }
            else
            {
                SetChildrenActive(true);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    void SetChildrenActive(bool value)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }

    public void SetTarget(GameObject currentTarget)
    {
        target = currentTarget.transform;
    }
}
