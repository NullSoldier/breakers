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
		Vector3 move = new Vector3(
			Input.GetAxis(HorizontalAxis),
            0.0f,
			Input.GetAxis(VerticalAxis));

		if (move != Vector3.zero) {
			playerCtrl.MoveDir = move;
			playerCtrl.LookDir = move;
			transform.position += move * playerCtrl.Speed;
		}
    }
}