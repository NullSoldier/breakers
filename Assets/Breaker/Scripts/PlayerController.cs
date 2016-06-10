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
    public int PlayerIndex = 0;

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
        state = lastState = PlayerState.Alive;
    }

    void Update()
    {
        if (transform.position.y < -5)
            state = PlayerState.Dead;
    }
}