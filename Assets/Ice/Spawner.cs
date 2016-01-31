using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject IceCube;
    public float Distance = 1.05f;
    public uint Count = 8;

    // Use this for initialization
    void Start()
    {
        var start = transform.position + new Vector3(Distance, 0, Distance) * (Count / -2);

        for (uint i = 0; i < Count; i++)
            for (uint j = 0; j < Count; j++)
                Instantiate(IceCube, start + new Vector3(i, 0, j) * Distance, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene("fuck", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
