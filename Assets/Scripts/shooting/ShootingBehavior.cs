using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    private int ammoCount = 20;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void fireAtEnemyObject(GameObject enemyTarget)
    {
        Debug.Log(enemyTarget.transform.position);
    }
    
}
