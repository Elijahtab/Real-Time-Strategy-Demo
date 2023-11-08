using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{
    
 
    private Vector3 targetPosition;
    public bool isSelected = false;
    public int movementSpeed = 5;
    private ShootingBehavior shootingBehavior;
    private float rangeOfUnit;

    private UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        shootingBehavior = gameObject.GetComponent<ShootingBehavior>();
        if(shootingBehavior != null)
        {
            rangeOfUnit = shootingBehavior.range - 0.01f;
        }       
    }
    void Update()
    {
        
    }
    public void stopAllCoroutines()
    {
        StopAllCoroutines();
        Debug.Log("all coroutines stopped");
    }
    public void StartMoveToTargetCoroutine(Vector3 targetPosition)
    {
        StartCoroutine(MoveToTarget(targetPosition));
    }
    public void StartMoveToUntilRangeCoroutine(GameObject enemyObject)
    {
        StartCoroutine(MoveToUntilRange(enemyObject));
    }
    public IEnumerator MoveToUntilRange(GameObject enemyObject)
    {
        while (enemyObject != null)
        {
            Debug.Log("while loop running");
            float distance = Vector3.Distance(transform.position, enemyObject.transform.position);
            
            if(distance <= rangeOfUnit)
            {
                RaycastHit hit; 
                if(Physics.Raycast(transform.position, enemyObject.transform.position - transform.position, out hit) && hit.collider.name == enemyObject.name)                
                {
                    Debug.Log("stopping movement");
                    agent.destination = transform.position;        
                } 
                else
                {
                    agent.destination = enemyObject.transform.position;
                }           
            }
            else
            {
                agent.destination = enemyObject.transform.position;
            }
            yield return null; // Yielding null means the coroutine will continue in the next frame.
        }
    }
    public IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        
        {
            while (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                agent.destination = targetPosition;
                yield return null;
            }
        }
    }


}