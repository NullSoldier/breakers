using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

enum PlayerAttackState
{
    None,
    SlammingUp,
    SlammingDown,
}

public class PlayerAttack : MonoBehaviour
{
    public float BreakStaggerTime = 0.1f;

    private PlayerController playerCtrl;
    private Rigidbody rigidBody;
    private IceCube lastCube;
    private PlayerAttackState state;
    private PlayerAttackState lastState;
    private KeyCode attackKey = KeyCode.Space;
    private float lastStateTime; // the time the last state change happened

    public void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody>();
        lastCube = getCubeUnder();
        attackKey = KeyCode.Space;
    }

    public void Update()
    {
        float timeSinceLast = Time.time - lastStateTime;

        IceCube cube = getCubeUnder();
        if(cube)
        {
            cube.GetComponent<Renderer>().material.color = Color.red;
            lastCube = cube;
        }

        if (state != lastState)
        {
            if (state == PlayerAttackState.SlammingUp)
            {
                rigidBody.velocity = Vector3.up * 5;
            }
            else if (state == PlayerAttackState.SlammingDown)
            {
                rigidBody.velocity = Vector3.down * 10;
            }

            lastState = state;
            lastStateTime = Time.time;
        }
        else
        {
            if (state == PlayerAttackState.None)
            {
                if(Input.GetKey(attackKey))
                    state = PlayerAttackState.SlammingUp;
            }
            else if (state == PlayerAttackState.SlammingUp)
            {
                if (timeSinceLast > 0.25)
                    state = PlayerAttackState.SlammingDown;
            }
        }
    }

    private Vector3 getAttackDir()
    {
        return new Vector3(-playerCtrl.LookDir.x, 0.0f, -playerCtrl.LookDir.y);
    }

    private IceCube getCubeUnder()
    {
        RaycastHit hit;
        var rc = Physics.Raycast(transform.position, Vector3.down, out hit);
        if (rc)
            return hit.transform.GetComponent<IceCube>();
        return null;
    }

    private List<IceCube> getCubesAttacked()
    {
        return Physics
            .RaycastAll(lastCube.transform.position, getAttackDir())
            .Select((h) => h.transform.GetComponent<IceCube>())
            .ToList();
    }

    void OnCollisionEnter(Collision c)
    {
        if (state != PlayerAttackState.SlammingDown)
            return;

        if (lastCube == null)
        {
            Debug.LogError("Collided with no lastCube");
            return;
        }

        float staggerTime = 0.0f;
        var cubes = getCubesAttacked();

        foreach (IceCube cube in cubes) {
            cube.Crack(staggerTime);
            staggerTime += BreakStaggerTime;
        }

        lastCube.GetComponentInChildren<ParticleSystem>().Play();
        lastCube.Crack(0);
        state = PlayerAttackState.None;
    }
}