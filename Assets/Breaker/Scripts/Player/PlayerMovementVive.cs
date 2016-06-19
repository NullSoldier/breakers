using UnityEngine;
using System.Collections;

public class PlayerMovementVive : MonoBehaviour
{
	private PlayerController playerCtrl;
	private SteamVR_Camera headCamera;

	public void Awake()
	{
		playerCtrl = GetComponent<PlayerController>();
		headCamera = GetComponentInChildren<SteamVR_Camera>();

		if (!playerCtrl.IsViveController)
			throw new System.Exception ("Cannot use VivePlayerMovement on non vive player");
	}

	public void Update()
	{
		Vector2 touchVec = getTouchVec();

		if(touchVec != Vector2.zero) {
			playerCtrl.MoveDir = headCamera.transform.forward * touchVec.y + headCamera.transform.right * touchVec.x;
            //new Vector3(playerCtrl.MoveDir.x, 0, playerCtrl.MoveDir.y) * playerCtrl.Speed;
            //transform.position += (headCamera.transform.forward * touchVec.y + headCamera.transform.right * touchVec.x).normalized * playerCtrl.Speed;
            var move = (headCamera.transform.forward * touchVec.y + headCamera.transform.right * touchVec.x);
            transform.position += move * playerCtrl.Speed;
            playerCtrl.MoveDir = new Vector3(move.x, 0.0f, move.y);
        }

		playerCtrl.LookDir = headCamera.transform.forward;
	}

	private Vector2 getTouchVec()
	{
		int leftIndex = (int)SteamVR_TrackedObject.EIndex.Device3;
		int rightIndex = (int)SteamVR_TrackedObject.EIndex.Device4;

		SteamVR_Controller.Device leftDevice = SteamVR_Controller.Input (leftIndex);
		SteamVR_Controller.Device rightDevice = SteamVR_Controller.Input (rightIndex);

		Vector2 leftTouch = leftDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
		Vector2 rightTouch = rightDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

		bool leftTouchDown = leftDevice.GetPress (SteamVR_Controller.ButtonMask.Touchpad);
		bool rightTouchDown = rightDevice.GetPress (SteamVR_Controller.ButtonMask.Touchpad);

		if (leftTouchDown && leftTouch != Vector2.zero)
			return leftTouch;

		if (rightTouchDown && rightTouch != Vector2.zero)
			return rightTouch;

		return Vector2.zero;
	}
}