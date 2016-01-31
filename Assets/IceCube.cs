using UnityEngine;
using System.Collections;


public enum IceCubeState
{
    Pristine,
    Cracked,
    Fractured,
    Falling,
}

public class IceCube : MonoBehaviour
{
    public IceCubeState state;
    public Texture2D pristineTexture;
    public Texture2D crackedTexture;
    public Texture2D FracturedTexture;
    public Texture2D fallingTexture;

    Rigidbody rb;
    Material mat;
    IceCubeState lastState;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<Renderer>().material;
        lastState = state = IceCubeState.Pristine;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (state != lastState)
        {
            if (state == IceCubeState.Pristine)
                mat.mainTexture = pristineTexture;
            else if (state == IceCubeState.Cracked)
                mat.mainTexture = crackedTexture;
            else if (state == IceCubeState.Fractured)
                mat.mainTexture = FracturedTexture;

            if (state == IceCubeState.Falling)
            {
                mat.mainTexture = fallingTexture;
                rb.useGravity = true;
            }
            else
            {
                rb.useGravity = false;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
    }
}
