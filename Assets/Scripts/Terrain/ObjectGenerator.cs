using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject prefabTest;
    public GameObject foliageParent;
    List<GameObject> prefabList = new List<GameObject>();    public LayerMask layerMask;
    [Range(0,1)] public float numOfTrees = 0.5f;
    

    public void DeleteAll()
    {
        
    }
    public void GenerateObjects(float[,] heightMap)
    {
        
        // Destroy previous instances
        foreach (GameObject prefabInstance in prefabList)
        {
            UnityEngine.Object.DestroyImmediate(prefabInstance);
        }

        // Clear the list after destroying instances
        prefabList.Clear();

        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float topLeftX = (width - 1) / -2f;
	    float topLeftZ = (height - 1) / 2f;

        Debug.Log("Generating Objects");

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float currentHeight = heightMap[x, y];

                if (currentHeight >= 0.4f)
                {
                    if (Random.value < numOfTrees)
                    {
                        Ray ray = new Ray(new Vector3(topLeftX + x, 100, topLeftZ - y), Vector3.down);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                        {
                            // Instantiate a new prefab for each condition
                            GameObject instantiatedPrefab = Instantiate(prefabTest, hit.point, Quaternion.identity);
                            Transform parentTransform = foliageParent.transform;
                            instantiatedPrefab.transform.SetParent(parentTransform);
                            // Add the instantiated prefab to the list
                            prefabList.Add(instantiatedPrefab);
                        }
                    }
                    
                }
            }
        }
    }


}
