using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    private ShootingBehavior shootingBehavior;
    private GameObject enemyObject;
    

    private List<GameObject> enemiesInRange = new List<GameObject>();


    void Awake()
    {
        shootingBehavior = transform.parent.gameObject.GetComponent<ShootingBehavior>();
    }

    private void OnTriggerEnter(Collider enemy)
    {

        if (enemy.CompareTag("Enemy"))
        {
            enemiesInRange.Add(enemy.gameObject);
            Debug.Log("enemy added");
        }
    }

    private void OnTriggerExit(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(enemy.gameObject);
            shootingBehavior.removeEnemy(enemy.gameObject);           
        }
    }
    

    void Update()
    {
        shootingBehavior.clearEnemies();
        foreach (GameObject go in enemiesInRange)
        {  
            Vector3 raycastOrigin = transform.position;
            Vector3 raycastTarget = go.transform.position - raycastOrigin;  
            RaycastHit hit;
            if (Physics.Raycast(raycastOrigin, raycastTarget, out hit) && hit.collider.CompareTag("Enemy"))                
            {
                shootingBehavior.addEnemy(go);           
            }

        }
 
        
    }
}
