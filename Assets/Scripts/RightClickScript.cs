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
    private Vector3 mousePosition;
    private Vector3 currentMousePos;
    private Vector3 rowDelta;
    private int rowLength;
    private bool inDragFormation = false;
    private Vector3 posChange;
    private int numberOfIntervals;
    public Texture2D pointTexture;

    public int unitNumber = 0;
    public float unitSpacing = 1f;

    public GameObject uiPrefab;
    private List<GameObject> instantiatedUIObjects = new List<GameObject>();

    void Awake()
    {
        selectedDictionary = GetComponent<SelectedDictionary>(); 
        
    }
    void ClearUIList()
    {
        // Optionally, you can destroy the GameObjects associated with the list
        foreach (GameObject obj in instantiatedUIObjects)
        {
            Destroy(obj);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        

        if(inDragFormation)
        {
            
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit2;
            
            if (Physics.Raycast(ray2, out hit2, float.MaxValue, includedLayers))
            {
                currentMousePos = hit2.point;
                float totalDistance = Vector3.Distance(mousePosition, currentMousePos);
                numberOfIntervals = Mathf.FloorToInt(totalDistance / unitSpacing);
                numberOfIntervals += 1;
                int i = 0;
                ClearUIList();
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
                        float t = (i % numberOfIntervals) / (float)numberOfIntervals;
                        Vector3 direction = (mousePosition - currentMousePos).normalized;  
                        Vector3 perpendicularDirection = new Vector3(-direction.z, 0, -direction.x).normalized;
                        Vector3 pointOnLine = Vector3.Lerp(mousePosition - perpendicularDirection * (i / (numberOfIntervals)) * unitSpacing, currentMousePos - perpendicularDirection * (i / (numberOfIntervals)) * unitSpacing, t);
                        GameObject newObject = Instantiate(uiPrefab, pointOnLine, Quaternion.identity);
                        instantiatedUIObjects.Add(newObject);
                        i++;
                    }
                }
            }
            if(Input.GetMouseButtonUp(1))
            {
                //Debug.Log($"Number of intervals: {numberOfIntervals}");
                ClearUIList();
                int i = 0;
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
                        float t = (i % numberOfIntervals) / (float)numberOfIntervals;
                        Vector3 direction = (mousePosition - currentMousePos).normalized;  
                        Vector3 perpendicularDirection = new Vector3(-direction.z, 0, -direction.x).normalized;
                        //Debug.Log($"Perpendicular direction: {perpendicularDirection}");
                        Vector3 pointOnLine = Vector3.Lerp(mousePosition - perpendicularDirection * (i / (numberOfIntervals)) * unitSpacing, currentMousePos - perpendicularDirection * (i / (numberOfIntervals)) * unitSpacing, t);
                        
                        movementScript.StartMoveToTargetCoroutine(pointOnLine);
                        //Debug.Log($"Point {i + 1}: {pointOnLine}");
                        i++;
                    }
                }
                inDragFormation = false;
            }
            return;
        }   
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
                mousePosition = hit.point;
                //If we need to trigger movement
                if(hit.collider.tag != "Enemy")
                {
                    if(moreThanOneSelected)
                    {
                        
                        inDragFormation =  true;
                        return;
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