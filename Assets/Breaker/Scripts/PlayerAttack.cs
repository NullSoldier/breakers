using UnityEngine;
using System.Collections;

enum PlayerState
{
    Alive,
    Dead
}

public class AttackState : MonoBehaviour
{
    private KeyCode smashKey;

    public void Awake()
    {
    }

    public void Update()
    {
        body = GetComponent<Rigidbody>();
        RaycastHit hit;
        var rc = Physics.Raycast(transform.position, Vector3.down, out hit);
        if (rc)
            lastCube = hit.transform.GetComponent<IceCube>();


        if (playerIndex == 0)
        {
            smashKey = KeyCode.RightAlt;
        }
        else
        {
            smashKey = KeyCode.Space;
        }
    }

    Vector3 getDirVector()
    {
        return transform.position - new Vector3(lastDir.x, 0, lastDir.y);
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

    if (state == PlayerState.SlammingUp)
            {
                body.velocity = Vector3.up* 5;
            }
            else if (state == PlayerState.SlammingDown)
            {
                var body = GetComponent<Rigidbody>();
body.velocity = Vector3.down* 10;
            }
            else if (state == PlayerState.Dead)
                gameObject.SetActive(false);

    
            lastState = state;
            lastStateTime = Time.time;
}