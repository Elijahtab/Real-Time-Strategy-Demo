using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    private int ammoCount = 20;
    private GameObject enemyTarget;
    private bool canFire = false;
    private EnemyClass enemyClass;
    public float reloadTime = 3f;
    public float reloadTimer = 3f;
    
    public List<GameObject> enemiesCanBeTargeted = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        canFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        reloadTimer += Time.deltaTime;
        if(enemiesCanBeTargeted.Count != 0 && ammoCount != 0 && reloadTimer >= reloadTime)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }

        if(canFire == true)
        {   
            
            reloadTimer = 0f;
            StartCoroutine(enemyToTarget(result => 
            {
                Debug.Log("Coroutine finished with result: " + result);
                StartCoroutine(fireAtTarget(result));
            }));
            
            
        }
    }
    public void addEnemy(GameObject enemyObject)
    {
        enemiesCanBeTargeted.Add(enemyObject);    
    }

    public void removeEnemy(GameObject enemyObject)
    {
        enemiesCanBeTargeted.Remove(enemyObject);
    }
    public void clearEnemies()
    {
        enemiesCanBeTargeted.Clear();
    }
    
    IEnumerator enemyToTarget(System.Action<GameObject> callback)
    {
        foreach(GameObject go in enemiesCanBeTargeted)
        {
            enemyClass = go.GetComponent<EnemyClass>();
            if(enemyClass.isTank == true)
            {
                callback(go);
                yield break;
            }
               
        }
        foreach(GameObject go in enemiesCanBeTargeted)
        {
            callback(go);
            yield break;
        }
        yield break;
        
    }
    IEnumerator fireAtTarget(GameObject target)
    {
        enemyTarget = target;
        Debug.Log("Firing at target");
        ammoCount--;    
        
        yield break;
    }
    
}
