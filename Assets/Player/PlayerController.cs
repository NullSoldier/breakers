using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float Speed = -0.5f;

	void Start ()
	{
	}

	void Update ()
	{
		var h = Input.GetAxis("Horizontal");
		var v = Input.GetAxis("Vertical");
		transform.position += new Vector3(h, 0, v) * Speed;
	
	}
}
