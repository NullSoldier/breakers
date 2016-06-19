using UnityEngine;
using System.Collections;

public enum PlayerState
{
	Inactive,
	Waiting,
    Alive,
    Dead
}

public class PlayerController : MonoBehaviour
{
	public float Speed = -0.5f;
	public bool IsViveController = false;
	public int PlayerIndex = -1;
	public PlayerState State = PlayerState.Inactive;

	private PlayerAttack playerAttack;

	[HideInInspector]
	public Vector3 MoveDir = Vector3.forward;
	[HideInInspector]
	public Vector3 LookDir = Vector3.forward;

	private Rigidbody rigidBody = null;

    void Awake()
    {
		rigidBody = GetComponent<Rigidbody> ();
		playerAttack = GetComponent<PlayerAttack> ();
		State = PlayerState.Waiting;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, LookDir, Color.green, 0.5f, false);
        Debug.DrawRay(transform.position, MoveDir, Color.blue, 0.5f, false);

        if (State == PlayerState.Alive)
		{
            if (transform.position.y < -5)
				State = PlayerState.Dead;
		}
    }

	public void StartWaiting()
	{
		State = PlayerState.Waiting;
		playerAttack.resetAttack();
	}

	public void Spawn()
	{
		if(PlayerIndex == 0)
			transform.position = new Vector3(4, 2, 4);
		else if(PlayerIndex == 1)
			transform.position = new Vector3(-4, 2, -4);
		else if(PlayerIndex == 2)
			transform.position = new Vector3(0, 0, 0);

        rigidBody.velocity = Vector3.zero;
		State = PlayerState.Alive;
	}


}