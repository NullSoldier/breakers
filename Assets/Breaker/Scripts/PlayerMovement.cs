using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerCtrl;
    private string hAxis;
    private string vAxis;

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
        if (playerCtrl.isViveController)
        {
            Transform camTransform = GetComponentInChildren<SteamVR_Camera>().head;
            Vector3 lookDir = camTransform.forward;
            Vector3 rightDir = camTransform.right;
            Vector2 analogDir = getTouchPadPosition(SteamVR_TrackedObject.EIndex.Device4).normalized;
            playerCtrl.MoveDir = ((lookDir * analogDir.y) + (rightDir * analogDir.x));
        }
        else
        {
			playerCtrl.MoveDir = new Vector2(Input.GetAxis(hAxis), Input.GetAxis(vAxis));
		}

        transform.position += playerCtrl.MoveDir * 0.02f;
    }

    private Vector2 getTouchPadPosition(SteamVR_TrackedObject.EIndex index)
    {
        var device = SteamVR_Controller.Input((int)index);
        bool isPressed = device.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
        return isPressed ? device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0) : Vector2.zero;
    }
}