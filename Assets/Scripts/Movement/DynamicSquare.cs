using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSquare : MonoBehaviour
{
    public int unitNumber = 17;
    public float unitSpacing = 1f;

    void Start()
    {
        CalculatePositions();
    }

    void CalculatePositions()
    {
        int rowLength = Mathf.CeilToInt(Mathf.Sqrt(unitNumber));
        Debug.Log(rowLength);

        for (int i = 0; i < unitNumber; i++)
        {
            float xPos = (i % rowLength) * unitSpacing;
            float zPos = (i / rowLength) * unitSpacing;

            Vector3 unitPosition = new Vector3(xPos, 0f, zPos);
            Debug.Log($"Unit {i + 1} position: {unitPosition}");
        }
    }
}
