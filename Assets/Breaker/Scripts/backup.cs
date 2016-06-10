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
    public int playerIndex = 0;

    private PlayerState state;
    private PlayerState lastState;
    private float lastStateTime;
    private Vector2 lastDir = Vector2.down;

    private KeyCode smashKey;
    private string hAxis;
    private string vAxis;

    Rigidbody body;
    IceCube lastCube;
    Renderer renderer;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        RaycastHit hit;
        var rc = Physics.Raycast(transform.position, Vector3.down, out hit);
        if (rc)
            lastCube = hit.transform.GetComponent<IceCube>();

        if (playerIndex == 0)
        {
			smashKey = KeyCode.RightAlt;
            hAxis = "Horizontal1";
            vAxis = "Vertical1";
        }
        else
        {
			smashKey = KeyCode.Space;
            hAxis = "Horizontal2";
            vAxis = "Vertical2";
        }


        state = lastState = PlayerState.Alive;

        renderer = GetComponentInChildren<Renderer>();
        renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    }

    void Update()
    {
        RaycastHit hit;
        var rc = Physics.Raycast(transform.position - new Vector3(lastDir.x, 0, lastDir.y), Vector3.down, out hit);
        if (rc)
        {
            var cube = hit.transform.GetComponent<IceCube>();
            
            if (cube != lastCube && lastCube != null)
            {
                
                cube.GetComponent<Renderer>().material.color = renderer.material.color;
                lastCube.GetComponent<Renderer>().material.color = Color.white;
            }

            lastCube = cube;
        }

        if (state != lastState)
        {
            if (state == PlayerState.SlammingUp)
            {
                body.velocity = Vector3.up * 5;
            }
            else if (state == PlayerState.SlammingDown)
            {
                var body = GetComponent<Rigidbody>();
                body.velocity = Vector3.down * 10;
            }
            else if (state == PlayerState.Dead)
                gameObject.SetActive(false);

    
            lastState = state;
            lastStateTime = Time.time;
        }
        else
        {
            float timeSinceLast = Time.time - lastStateTime;

            if (state == PlayerState.Alive)
            {
                var touch = getTouchPadPosition();
                var h = Input.GetAxis(hAxis);
                var v = Input.GetAxis(vAxis);

                if (playerIndex == 1)
                {
                    h = touch.x;
                    v = touch.y;
                }

                transform.position += new Vector3(h, 0, v) * Speed;
                if (h != 0 || v != 0)
                    lastDir = new Vector2(h, v);

                if (Input.GetKey(smashKey))
                {
                    state = PlayerState.SlammingUp;
                }
            }
            else if (state == PlayerState.SlammingUp && timeSinceLast > 0.25)
            {
                state = PlayerState.SlammingDown;
            }
            else if (state == PlayerState.SlammingDown && timeSinceLast > 0.20)
            {
            }

        }
        if (transform.position.y < -5)
            state = PlayerState.Dead;
    }

    Vector2 getTouchPadPosition()
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

    void OnCollisionEnter(Collision c)
    {
        if (state == PlayerState.SlammingDown)
        {
            state = PlayerState.Alive;

            if (lastCube != null)
            {
                lastCube.GetComponentInChildren<ParticleSystem>().Play();
                lastCube.Crack(0);
            }

            var blocks = Physics.RaycastAll(lastCube.transform.position, -new Vector3(lastDir.x, 0, lastDir.y));
            
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].transform.GetComponent<IceCube>().Crack(0.1f * i);
            }
        }
    }
}
