using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float Speed = -0.5f;
	private PlayerState state;
	private PlayerState lastState;
	private float lastStateTime;

	enum PlayerState {
		Alive,
		SlammingUp,
		SlammingDown,
		Dead,
		Inactive
	}

	void Start ()
	{
		state = lastState = PlayerState.Alive;

		foreach (var renderer in this.GetComponentsInChildren<MeshRenderer>()) {
			renderer.material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), 1f);
		}
	}

	void Update ()
	{
		if (state != lastState) {
			if (state == PlayerState.SlammingUp) {
				var body = GetComponent<Rigidbody> ();
				body.velocity = Vector3.up * 5;
			} else if (state == PlayerState.SlammingDown) {
				var body = GetComponent<Rigidbody> ();
				body.velocity = Vector3.down * 10;
			}

			lastState = state;
			lastStateTime = Time.time;
		} else {
			float timeSinceLast = Time.time - lastStateTime;

			if (state == PlayerState.Alive) {
				var h = Input.GetAxis ("Horizontal");
				var v = Input.GetAxis ("Vertical");
				transform.position += new Vector3 (h, 0, v) * Speed;

				if (Input.GetKey (KeyCode.Space)) {
					state = PlayerState.SlammingUp;
				}
			} else if (state == PlayerState.SlammingUp && timeSinceLast > 0.25) {
				state = PlayerState.SlammingDown;
			} else if (state == PlayerState.SlammingDown && timeSinceLast > 0.25) {
				state = PlayerState.Alive;
			}

		}
	}
}
