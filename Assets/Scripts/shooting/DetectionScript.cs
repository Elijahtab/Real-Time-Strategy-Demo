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

    //Whenever the unit sphere collider(its range) collides with an enemy, add it to the list of enemies in range
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            enemiesInRange.Add(enemy.gameObject);
        }
    }
    //Whenever the unit sphere collider(its range) exits with an enemy, remove it from the list of enemies in range
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
        //For each enemy in range, check if the raycast from the unit to the enemy hits the enemy
        foreach (GameObject go in enemiesInRange)
        {  
            if(go != null)
            {
                Vector3 raycastOrigin = transform.position;
                Vector3 raycastTarget = go.transform.position - raycastOrigin;  
                RaycastHit hit;
                if (Physics.Raycast(raycastOrigin, raycastTarget, out hit) && hit.collider.CompareTag("Enemy"))                
                {
                    //Add the enemy to the list of enemies that can be targeted, in the ShootingBehavior Script
                    shootingBehavior.addEnemy(go);           
                }
            }
            

        }
 
        
    }
}
