using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickScript : MonoBehaviour
{
    public bool isSelected = false;
    private Vector3 targetPosition;
    private MovementScript movementScript;
    private ShootingBehavior shootingBehavior;
    public LayerMask includedLayers;
    // Start is called before the first frame update
    void Awake()
    {
        movementScript = gameObject.GetComponent<MovementScript>();
        shootingBehavior = gameObject.GetComponent<ShootingBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isSelected)
        {
                
            if (Input.GetMouseButtonDown(1)) // Check for right mouse button click
            {
                shootingBehavior.enemyManuallySelected = false;
                shootingBehavior.clearSelectedEnemy();
                if(!(Input.GetKey(KeyCode.LeftShift)))
                {
                    movementScript.stopAllCoroutines();
                }

                // Cast a ray from the mouse position to the world
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;  

                if (Physics.Raycast(ray, out hit, float.MaxValue, includedLayers))
                {
                    if(hit.collider.tag != "Enemy")
                    {
                        targetPosition = hit.point;
                        targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z );
                        movementScript.StartMoveToTargetCoroutine(targetPosition);
                    }                        
                    else if(hit.collider.tag == "Enemy")
                    {
                        Debug.Log("Enemy clicked");
                        GameObject hitObject = hit.collider.gameObject;
                        shootingBehavior.selectedEnemyToTarget(hitObject);
                        movementScript.StartMoveToUntilRangeCoroutine(hitObject);
                    }
                }
                    
                    
            }

        }
        
    }
}
