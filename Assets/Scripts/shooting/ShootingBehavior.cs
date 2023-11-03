using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    public int ammoCount = 200;
    private bool canFire = false;
    private EnemyClass enemyClass;
    public float reloadTime = 3f;
    public float reloadTimer = 3f;
    private GameObject selectedEnemy;
    
    public List<GameObject> enemiesCanBeTargeted = new List<GameObject>(); //List of enemies that the unit can fire at

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
            //Find enemy to target
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
                break;
            }
               
        }
        if (selectedEnemy == null)
        {
            // If no tank was found, choose the first enemy in the list
            if (enemiesCanBeTargeted.Count > 0)
            {
                selectedEnemy = enemiesCanBeTargeted[0];
            }
        }

        if (selectedEnemy != null)
        {
            callback(selectedEnemy);
        }
        yield break;
    }
    public IEnumerator fireAtTarget(GameObject target)
    {
        Debug.Log("Firing at target");
        ammoCount--;
        StandardBullet standardBullet = GetComponent<StandardBullet>();
        standardBullet.shooting(target);
        yield break;
    }
    
}
