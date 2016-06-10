using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject IceCube;
    public float Distance = 0.5f;
    public uint Count = 8;

    void Start()
    {
        for (uint i = 0; i < Count; i++) {
            for (uint j = 0; j < Count; j++)
                spawnCubeAt(i, j);
        }
    }

    private Vector3 getBlockPosition(uint cellX, uint cellY)
    {
        Vector3 offset = new Vector3(0, -(Distance / 2), 0);
        Vector3 half = new Vector3(Distance, 0, Distance) * (Count / 2.0f);
        Vector3 position = new Vector3(cellX, 0, cellY) * Distance;

        return transform.position + position - half + offset;
    }

    private void spawnCubeAt(uint cellX, uint cellY)
    {
        Vector3 position = getBlockPosition(cellX, cellY);
        GameObject cube = (GameObject)Instantiate(IceCube, position, Quaternion.identity);
        cube.transform.parent = transform;
    }
}