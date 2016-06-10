using UnityEngine;
using System.Collections;

enum PlayerState
{
    Alive,
    SlammingUp,
    SlammingDown,
    Dead,
    Inactive
}

public class PlayerController : MonoBehaviour
{
    public float Speed = -0.5f;
	public int PlayerIndex = 0;
	public Vector2 MoveDir = Vector2.down;
	public Vector3 LookDir = Vector3.forward;
	public bool isViveController = false;

    private PlayerState state;
    private PlayerState lastState;
    private float lastStateTime;

    private KeyCode smashKey;
    private string hAxis;
    private string vAxis;

    Rigidbody body;
    IceCube lastCube;
    Renderer renderer;

    void Start()
    {
        state = lastState = PlayerState.Alive;
    }

    void Update()
    {
        // Check death
        if (transform.position.y < -5)
        {
            state = PlayerState.Dead;
            gameObject.SetActive(false);
        }

        if(this.MoveDir != Vector2.zero) {
            // read look direction
            if (this.isViveController) {
			    this.LookDir = GetComponent<SteamVR_Camera> ().head.forward;
		    } else {
			    this.LookDir = new Vector3 (MoveDir.x, 0.0f, MoveDir.y);
		    }
        }
    }
}