using UnityEngine;
using System.Collections;

public enum CubeState
{
    Pristine = 0,
    Cracked = 1,
    Fractured = 2,
    Falling = 3
}

public class IceCube : MonoBehaviour
{
    public Material[] materials; //one for each state
    public float stateDelay = 0.25f;
    public float respawnDelay = 5f;
    float crackDelay = 0;
    float originalY = 0.0f;

    public CubeState state;
    CubeState lastState;
    float lastStateChangeTime;

    private Rigidbody rb;
    private Renderer ren;
    private ParticleSystem ptz;
	private new AudioSource audio;

    int stateIndex { get { return (int)state; } }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ren = GetComponent<Renderer>();
        ptz = GetComponentInChildren<ParticleSystem>();
        audio = GetComponent<AudioSource>();

        lastState = state = CubeState.Pristine;
        lastStateChangeTime = 0f;

        ren.material = materials[stateIndex];
        originalY = transform.position.y;
    }

    void Update()
    {
        if (state != lastState)
        {
            if (state == CubeState.Pristine)
            {
                ren.material = materials[stateIndex];
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
                ren.material = materials[stateIndex];
                ptz.Stop();
            }
            else if (state == CubeState.Falling)
            {
                rb.useGravity = true;
                ptz.Play();
            }
            else
                ren.material = materials[stateIndex];

            lastState = state;
            lastStateChangeTime = Time.time;
        }
        else
        {
            float stateChangeDelta = Time.time - lastStateChangeTime;

            if (state == CubeState.Cracked && stateChangeDelta > stateDelay + crackDelay)
            {
                state = CubeState.Fractured;
                audio.Play();
            }
            else if (state == CubeState.Fractured && stateChangeDelta > stateDelay)
                state = CubeState.Falling;
            else if (state == CubeState.Falling && stateChangeDelta > respawnDelay)
                state = CubeState.Pristine;
        }
    }

    public void Crack(float Delay)
    {
        crackDelay = Delay;
        if (state != CubeState.Falling)
            state++;
    }
}
