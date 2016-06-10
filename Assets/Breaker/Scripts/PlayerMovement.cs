using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerCtrl;
    private string hAxis;
    private string vAxis;
	private float Speed = playerCtrl.Speed;

    public void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();

        if (playerCtrl.PlayerIndex == 0)
        {
            hAxis = "Horizontal1";
            vAxis = "Vertical1";
        }
        else if (playerCtrl.PlayerIndex == 1)
        {
            hAxis = "Horizontal2";
            vAxis = "Vertical2";
        }
    }

    public void Update()
    {
		Vector2 movementVec;

		// read inputs
		if (playerCtrl.isViveController) {
			var touch = getTouchPadPosition ();
			Vector2 flatForward = Vector2 (playerCtrl.LookDir.x, playerCtrl.LookDir.z);

			playerCtrl.MoveDir = flatForward * touch;

		} else {
			playerCtrl.MoveDir = Vector2(Input.GetAxis(hAxis), Input.GetAxis(vAxis));
		}

		// apply the movement tranform
        transform.position += new Vector3(playerCtrl.MoveDir.x, 0, playerCtrl.MoveDir.z) * Speed;
        if (h != 0 || v != 0)
            lastDir = new Vector2(h, v);
    }

    private Vector2 getTouchPadPosition()
    {
        //Get the right device
        var rightDevice = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.FarthestRight);
        //Check if the device is valid
        if (rightDevice == -1) { return Vector2.zero; }
        //Get the x,y position on the pad
        Vector2 touch = SteamVR_Controller.Input(rightDevice).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        //Check if the player press on the pad button
        if (!SteamVR_Controller.Input(rightDevice).GetPress(SteamVR_Controller.ButtonMask.Touchpad)) { return Vector2.zero; }
        return touch;
    }
}