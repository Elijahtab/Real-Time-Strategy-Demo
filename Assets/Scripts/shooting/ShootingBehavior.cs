using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    public int ammoCount = 200;
    private EnemyClass enemyClass;
    public float reloadTime = 3f;
    public float reloadTimer = 3f;
    private GameObject selectedEnemy;
    private StandardBullet standardBullet;
    public float range = 1f;
    public GameObject detectionZone;
    public bool enemyManuallySelected = false;
    
    public List<GameObject> enemiesCanBeTargeted = new List<GameObject>(); //List of enemies that the unit can fire at

    // Start is called before the first frame update
    void Awake()
    {
        //Set the scale of the detection zone to the range of the unit
        detectionZone.transform.localScale = new Vector3(range, range, range);
    }

    // Update is called once per frame
    void Update()
    {
        
        reloadTimer += Time.deltaTime;
        if(enemiesCanBeTargeted.Count != 0 && ammoCount >= 0 && reloadTimer >= reloadTime)
        {
            isEnemyManuallySelected();
            isSelectedEnemyInRange();
            reloadTimer = 0f;
            if(selectedEnemy == null)
            {
                enemyToTarget();
            }
            fireAtTarget(selectedEnemy);
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
    
    private void enemyToTarget()
    {
        Debug.Log("finding enemy to target");
        if(selectedEnemy == null)
        {
            foreach(GameObject go in enemiesCanBeTargeted)
            {
                enemyClass = go.GetComponent<EnemyClass>();
                if(enemyClass.isTank == true)
                {
                    selectedEnemy = go;
                    return;
                }
                   
            }
            if(enemiesCanBeTargeted.Count > 0)
            {
                selectedEnemy = enemiesCanBeTargeted[0];
            }
             
        }

    }

    public void selectedEnemyToTarget(GameObject enemyObject)
    {
        Debug.Log("enemy manually selected");
        selectedEnemy = enemyObject;
        enemyManuallySelected = true;
    }

    private void fireAtTarget(GameObject target)
    {
        ammoCount--;
        StandardBullet standardBullet = GetComponent<StandardBullet>();
        standardBullet.shootAt(target);
    }
    private void isEnemyManuallySelected()
    {
        if(enemyManuallySelected == true && selectedEnemy == null)
        {
            enemyManuallySelected = false;
        }
    }
    private void isSelectedEnemyInRange()
    {
        
        if((!(enemiesCanBeTargeted.Contains(selectedEnemy)) && enemyManuallySelected == false))
        {
            Debug.Log("enemy set to null no in range");
            selectedEnemy = null;
        }
    }
    
    
}
