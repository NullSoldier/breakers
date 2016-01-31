using UnityEngine;
using System.Collections;

public class IceCube : MonoBehaviour
{
    public Material[] materials; //one for each state

    public uint state;
    uint lastState;
    float lastFallTime;

    Rigidbody rb;
    Renderer ren;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ren = GetComponent<Renderer>();
        lastState = state = 0;
        lastFallTime = 0;

        ren.material = materials[state];
    }

    // Update is called once per frame
    void Update()
    {
        if (state != lastState)
        {
            if (state == materials.Length)
            {
                rb.useGravity = true;
                lastFallTime = Time.time;
            }
            else
            {
                ren.material = materials[state];
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }

            lastState = state;
        }
        else if (state == materials.Length && Time.time - lastFallTime > 3)
            state = 0;
    }

    void OnMouseDown()
    {
        if (state != materials.Length)
            state++;
    }
}
