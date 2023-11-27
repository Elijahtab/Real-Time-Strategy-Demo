using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickScript : MonoBehaviour
{
    public bool isSelected = false;
    public bool moreThanOneSelected = false;
    private Vector3 targetPosition;
    private MovementScript movementScript;
    private ShootingBehavior shootingBehavior;
    private SelectedDictionary selectedDictionary;
    public LayerMask includedLayers;

    public int unitNumber = 0;
    public float unitSpacing = 1f;
    
    void Awake()
    {
        selectedDictionary = GetComponent<SelectedDictionary>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(selectedDictionary.SelectedTable.Count > 1)
        {
            moreThanOneSelected = true;
        }
        else
        {
            moreThanOneSelected = false;
        }

        if (Input.GetMouseButtonDown(1)) // Check for right mouse button click
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;  
           
            if (Physics.Raycast(ray, out hit, float.MaxValue, includedLayers))
            {
                Debug.Log(hit.point);
                //If we need to trigger movement
                if(hit.collider.tag != "Enemy")
                {
                    if(moreThanOneSelected)
                    {
                        int i = 0;
                        unitNumber = selectedDictionary.SelectedTable.Count;
                        int rowLength = Mathf.CeilToInt(Mathf.Sqrt(unitNumber));
                        foreach(KeyValuePair<int,GameObject> pair in selectedDictionary.SelectedTable)
                        {   
                            
                            movementScript = pair.Value.GetComponent<MovementScript>();
                            shootingBehavior = pair.Value.GetComponent<ShootingBehavior>();
                            shootingBehavior.enemyManuallySelected = false;
                            
                            float xPos = (i % rowLength) * unitSpacing;
                            float zPos = (i / rowLength) * unitSpacing;
                            targetPosition = hit.point;
                            Vector3 unitPosition = new Vector3(targetPosition.x + xPos, targetPosition.y, targetPosition.z + zPos);

                            if(!(Input.GetKey(KeyCode.LeftShift)))
                            {
                                movementScript.stopAllCoroutines();
                            }
                            movementScript.StartMoveToTargetCoroutine(unitPosition);
                            Debug.Log($"Unit {i + 1} position: {unitPosition}");
                            i++;
                            
                        }
                    }
                    else
                    {
                        foreach(KeyValuePair<int,GameObject> pair in selectedDictionary.SelectedTable)
                        {
                            if(pair.Value != null)
                            {
                                shootingBehavior = pair.Value.GetComponent<ShootingBehavior>();
                                movementScript = pair.Value.GetComponent<MovementScript>();
            
                                shootingBehavior.enemyManuallySelected = false;
                                if(!(Input.GetKey(KeyCode.LeftShift)))
                                {
                                    movementScript.stopAllCoroutines();
                                }
                                targetPosition = hit.point;
                                targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
                                movementScript.StartMoveToTargetCoroutine(targetPosition);
                            }
                        }
                    }
                    
                }                     
                //If we need to trigger shooting and or chasing an enemy   
                else if(hit.collider.tag == "Enemy")
                {
                    
                    GameObject hitObject = hit.collider.gameObject;
                    foreach(KeyValuePair<int,GameObject> pair in selectedDictionary.SelectedTable)
                    {
                        if(pair.Value != null)
                        {
                            shootingBehavior = pair.Value.GetComponent<ShootingBehavior>();
                            movementScript = pair.Value.GetComponent<MovementScript>();

                            shootingBehavior.enemyManuallySelected = false;
                            shootingBehavior.clearSelectedEnemy();
                            if(!(Input.GetKey(KeyCode.LeftShift)))
                            {
                                movementScript.stopAllCoroutines();
                            }
                            shootingBehavior.selectedEnemyToTarget(hitObject);
                            movementScript.StartMoveToUntilRangeCoroutine(hitObject);
                        }
                    }
                }
            }         
        }  
    }
}
