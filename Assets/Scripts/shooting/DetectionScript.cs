using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    private ShootingBehavior shootingBehavior;
    private GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        shootingBehavior = transform.parent.gameObject.GetComponent<ShootingBehavior>();
    }

    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Debug.Log("touch");
            enemyObject = enemy.gameObject;
            

        }
    }

    private void OnTriggerExit(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Debug.Log("detouch");
            enemyObject = null;
            

        }
    }

    void Update()
    {
        if (enemyObject != null)
        {
            Vector3 raycastOrigin = transform.position;
            Vector3 raycastTarget = enemyObject.transform.position - raycastOrigin;  
            RaycastHit hit;
            if (Physics.Raycast(raycastOrigin, raycastTarget, out hit))
            {
                if(hit.collider.CompareTag("Enemy"))
                {
                    shootingBehavior.fireAtEnemyObject(enemyObject);
                }
            }          
        }
        
    }
}
