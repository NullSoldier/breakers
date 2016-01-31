using UnityEngine;
using System.Collections;

public enum CubeState {
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

    public CubeState state;
	CubeState lastState;
	float lastStateChangeTime;

    Rigidbody rb;
    Renderer ren;

	int stateIndex { get { return (int)state; } }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ren = GetComponent<Renderer>();
		lastState = state = CubeState.Pristine;
		lastStateChangeTime = 0f;

		ren.material = materials[stateIndex];
    }
		
    void Update()
    {
		if (state != lastState) {
			if (state == CubeState.Pristine){
				ren.material = materials [stateIndex];
				rb.useGravity = false;
				rb.velocity = Vector3.zero;
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
				ren.material = materials [stateIndex];
			} else if (state == CubeState.Cracked) {
				ren.material = materials[stateIndex];
			} else if (state == CubeState.Fractured) {
				ren.material = materials[stateIndex];
			} else if (state == CubeState.Falling) {
				rb.useGravity = true;
			}

			lastState = state;
			lastStateChangeTime = Time.time;
		} else {
			float stateChangeDelta = Time.time - lastStateChangeTime;
			
			if (state == CubeState.Cracked && stateChangeDelta > stateDelay) {
				state = CubeState.Falling;
			}
			else if (state == CubeState.Fractured && stateChangeDelta > stateDelay) {
				state = CubeState.Falling;
			}
			else if (state == CubeState.Falling && stateChangeDelta > respawnDelay) {
				state = CubeState.Pristine;
			}
		}
    }

    void OnMouseDown()
    {
		if (state == CubeState.Pristine) {
			state = CubeState.Cracked;
		}
    }
}
