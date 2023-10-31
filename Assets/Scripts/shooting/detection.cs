using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detection : MonoBehaviour
{
    public ShootingBehavior shootingBehavior;
    private GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        shootingBehavior = GetComponent<ShootingBehavior>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("enemyUnit"))
        {
            Debug.Log("touch");
            enemyObject = enemy.gameObject;
            // Do something when a "Player" object enters the trigger area.
        }
    }
    void Update()
    {
        if (enemyObject != null)
        {
            Vector3 raycastOrigin = transform.position;
            Vector3 raycastTarget = enemyObject.transform.position - raycastOrigin;  
            RaycastHit hitInfo;
            if (Physics.Raycast(raycastOrigin, raycastTarget, out hitInfo))
            {
                // You can now access information about the hit, such as the object's name, tag, etc.
                Debug.Log("Raycast hit: " + hitInfo.collider.gameObject.name);
            }          
        }
        
    }
}
