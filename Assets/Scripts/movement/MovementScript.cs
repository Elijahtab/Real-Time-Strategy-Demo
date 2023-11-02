using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{
    
 
    private Vector3 targetPosition;
    public bool isSelected = false;
    public int movementSpeed = 5;

    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
       
    }
    void Update()
    {
        if (isSelected)
        {
            
            if (Input.GetMouseButtonDown(1)) // Check for right mouse button click
            {
                if(!(Input.GetKey(KeyCode.LeftShift)))
                {
                    StopAllCoroutines();
                }
                
                
                // Cast a ray from the mouse position to the world
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;  
                int layerMask = 1 << 8;

                if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
                {
                    
                    // Set the target position to the point where the ray hit
                    targetPosition = hit.point;
                    targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z );
                    StartCoroutine(MoveToTarget(targetPosition));
                }
                
                
            }
            


        }
        
    }

    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        
        {
            while (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Debug.Log(targetPosition);
                agent.destination = targetPosition;
                
                yield return null;
            }
        }
    }


}