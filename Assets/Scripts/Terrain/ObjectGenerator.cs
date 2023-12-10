using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public TerrainData terrainData;
    public GameObject prefabTest;
    public LayerMask layerMask;
    [Range(0,1)]
    public float treeStartHeight = 0;
    [Range(0,1)]
    public float treeStopHeight = 0.4f;
    [Range(0,1)] 
    public float numOfTrees = 0.5f;


    public void DeleteAll()
    {
        
    }
    public void GenerateObjects(float[,] heightMap)
    {
        // Destroy foliageParent if it already exists
        if (GameObject.Find("FoliageParent"))
        {
            DestroyImmediate(GameObject.Find("FoliageParent"));
        }
        GameObject foliageParent = new GameObject("FoliageParent");
        int uniformScale = terrainData.uniformScale;
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        width *= uniformScale;
        height *= uniformScale;
        float topLeftX = (width - 1) / -2f;
	    float topLeftZ = (height - 1) / 2f;
        int treeCount = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int originalX = x / uniformScale;
                int originalY = y / uniformScale;
                float currentHeight = heightMap[originalX, originalY];

                if (currentHeight >= treeStartHeight && currentHeight <= treeStopHeight)
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
                            treeCount += 1;
                        }
                    }
                    
                }
            }
        }
        Debug.Log("Trees Instantiated: " + treeCount);
    }


}
