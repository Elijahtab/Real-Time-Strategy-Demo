using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    private Transform targetPosition;
    private GameObject target;
    public GameObject Bullet;


    void Update()
    {
        
    }
    public void shootAt(GameObject target)
    {
        GameObject newBullet = Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, 0));
        Vector3 direction = (target.transform.position - newBullet.transform.position).normalized;
        newBullet.transform.LookAt(target.transform);

        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * 7f; // Use the normalized direction vector
        }

        Destroy(newBullet, 6f);

    }
}
