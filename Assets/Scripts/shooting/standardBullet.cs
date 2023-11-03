using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class standardBullet : MonoBehaviour
{
    private Transform targetPosition;
    private GameObject target;

    void Update()
    {
        ShootingBehavior shootingBehavior = GetComponent<ShootingBehavior>();

        if (shootingBehavior != null)
        {
            target = shootingBehavior.GetTarget();
            if (target != null)
            {
                targetPosition = target.GetComponent<Transform>();
                Debug.Log("Firing at target");

                if (targetPosition != null)
                {
                    Debug.Log("Target Position: " + targetPosition.position);
                }
            }
        }
    }
}
