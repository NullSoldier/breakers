using UnityEngine;
using System.Collections;

public enum PlayerState
{
	Inactive,
	Waiting,
    Alive,
    SlammingUp,
    SlammingDown,
    Dead
}

public class PlayerController : MonoBehaviour
{
	public float Speed = -0.5f;
	public bool IsViveController = false;
	public uint PlayerIndex;
	public PlayerState State;

	[HideInInspector]
	public Vector2 MoveDir = Vector2.down;
	[HideInInspector]
	public Vector3 LookDir = Vector3.forward;

    void Awake()
    {
		State = PlayerState.Inactive;
		State = PlayerState.Waiting;
    }

    void Update()
    {
		if (State == PlayerState.Alive)
		{
			if (transform.position.y < -5)
				State = PlayerState.Dead;
		}
    }

	public void StartWaiting()
	{
		State = PlayerState.Waiting;
	}

	public void Spawn()
	{
		State = PlayerState.Alive;

		if(PlayerIndex == 0)
			transform.position = new Vector3(4, 2, 4);
		else if(PlayerIndex == 1)
			transform.position = new Vector3(-4, 2, -4);
		else if(PlayerIndex == 2)
			transform.position = new Vector3(-4, 2, 4);
	}


}