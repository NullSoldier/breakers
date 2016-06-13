using UnityEngine;
using System.Collections;

public class PlayerMovementAxis : MonoBehaviour
{
	public string HorizontalAxis;
	public string VerticalAxis;

	private PlayerController playerCtrl;

    public void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
    }

    public void Update()
    {
		Vector3 move = new Vector2(
			Input.GetAxis(HorizontalAxis),
			Input.GetAxis(VerticalAxis));

		if (move != Vector3.zero) {
			playerCtrl.MoveDir = move;
			playerCtrl.LookDir = move;
			transform.position += new Vector3(move.x, 0, move.y) * playerCtrl.Speed;
		}
    }
}