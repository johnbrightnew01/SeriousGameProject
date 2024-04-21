using UnityEngine;

public class PyramidGenerator2 : MonoBehaviour
{
    public GameObject cubePrefab;
    public int layer;
    public int baseLayerC;
    private void Start()
    {
        GeneratePyramid(layer, baseLayerC);
    }

    public void GeneratePyramid(int layers, int baseLayerCubes)
    {
        // Calculate the total number of cubes required
        int totalCubes = CalculateTotalCubes(layers, baseLayerCubes);

        // Generate the pyramid
        float xOffset = -(baseLayerCubes - 1) * 0.5f;
        float zOffset = 0f;
        int currentLayerCubes = baseLayerCubes;

        for (int layer = 0; layer < layers; layer++)
        {
            for (int cubeIndex = 0; cubeIndex < currentLayerCubes; cubeIndex++)
            {
                Vector3 cubePosition = new Vector3(xOffset + cubeIndex, layer, zOffset);
                Instantiate(cubePrefab, cubePosition, Quaternion.identity);
            }

            xOffset += 0.5f;
            zOffset += 0.5f;
            currentLayerCubes--;
        }
    }

    private int CalculateTotalCubes(int layers, int baseLayerCubes)
    {
        int totalCubes = 0;

        for (int layer = 0; layer < layers; layer++)
        {
            totalCubes += baseLayerCubes;
            baseLayerCubes--;
        }

        return totalCubes;
    }
}
