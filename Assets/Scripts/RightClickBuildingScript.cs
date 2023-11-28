using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickBuildingScript : MonoBehaviour
{
    public LayerMask includedLayers;
    private Vector3 mousePosition;
    private Vector3 currentMousePos;
    private Vector3 direction;
    private int numberOfIntervals;
    public float roadSize = 4f;
    public GameObject uiRoadPrefab;
    public GameObject roadPrefab;
    private bool inRealTimeFeedback = false;
    private bool roadSelected = true;


    private List<GameObject> instantiatedUIObjects = new List<GameObject>();
    void Awake()
    {
        inRealTimeFeedback = false;
    }

    void ClearUIList()
    {
        // Destroy the GameObjects associated with the list
        foreach (GameObject obj in instantiatedUIObjects)
        {
            Destroy(obj);
        }
    }
    void RoadInstantiatorLoop(GameObject prefab)
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2; 
        if (Physics.Raycast(ray2, out hit2, float.MaxValue, includedLayers))
        {
            currentMousePos = hit2.point;
            float totalDistance = Vector3.Distance(mousePosition, currentMousePos);
            numberOfIntervals = Mathf.FloorToInt(totalDistance / roadSize);
            numberOfIntervals += 1;
            ClearUIList();
            //Create UI objects that show where the roads will go if you unclick right mouse button
            for(int i = 0; i < numberOfIntervals; i++)
            {
                float t = i / (float)numberOfIntervals;
                Vector3 direction = (mousePosition - currentMousePos).normalized;  
                Vector3 pointOnLine = Vector3.Lerp(mousePosition, currentMousePos, t);
                
                Vector3 offset = direction * roadSize;
                pointOnLine += offset;

                Quaternion rotation = Quaternion.LookRotation(direction);
                GameObject newObject = Instantiate(prefab, pointOnLine, rotation);

                //Set scale of prefab based on roadSize
                Vector3 newScale = newObject.transform.localScale;
                newScale.z = roadSize;
                newObject.transform.localScale = newScale;

                instantiatedUIObjects.Add(newObject);
            }
        }
    }
        
    
    void Update()
    {
        if(inRealTimeFeedback)
        {
            
            // Show real time feedback of road location
            RoadInstantiatorLoop(uiRoadPrefab);

            //On mouse button up instantiate the road permenantly
            if(Input.GetMouseButtonUp(1))
            {
                RoadInstantiatorLoop(roadPrefab);
                inRealTimeFeedback = false;
            }
            return;
            
        }
        // Check for mouse input
        if (Input.GetMouseButtonDown(1))
        {
            if(roadSelected)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit; 
                if (Physics.Raycast(ray, out hit, float.MaxValue, includedLayers))
                {
                    mousePosition = hit.point;
                    inRealTimeFeedback = true;
                    return;
                }
            }  
            
        }
        
    }
}
