using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePyramidGenerator : MonoBehaviour
{
    public GameObject cubePrefab;   // Prefab for the cube object
    public int pyramidSize;         // Number of cubes in the pyramid
    public float cubeSpacing = 1f;  // Spacing between cubes
    public List<Transform> positionList = new List<Transform>();
    public Transform holder;
    private void Start()
    {
        GenerateCubePyramid();
    }

    private void GenerateCubePyramid()
    {
        int cubesPerLayer = 1;            // Number of cubes in the current layer
        int currentLayer = 0;             // Current layer index
        int cubesGenerated = 0;           // Total number of cubes generated

        while (cubesGenerated < pyramidSize)
        {
            for (int i = 0; i < cubesPerLayer; i++)
            {
                // Calculate the position of the cube based on the current layer and cube index
                float xPos = -currentLayer * cubeSpacing / 2f + i * cubeSpacing;
                xPos += this.transform.position.x;
                float zPos = currentLayer * cubeSpacing;
                zPos += this.transform.position.z;

                // Instantiate the cube at the calculated position
              positionList.Add(Instantiate(cubePrefab, new Vector3(xPos, 0f, zPos), Quaternion.identity, holder.transform).transform);


                cubesGenerated++;

                if (cubesGenerated >= pyramidSize)
                    break;
            }

            currentLayer++;
            cubesPerLayer++;
        }
        holder.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, positionList[positionList.Count - 1].transform.position.z);
        holder.transform.rotation = Quaternion.Euler(0, 180f, 0);
    }
}
