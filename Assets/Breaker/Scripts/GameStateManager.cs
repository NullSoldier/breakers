using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public enum GameState
{
	Waiting,
	Running,
	RoundOver
}
	
public class GameStateManager : MonoBehaviour
{
	public Camera OverheadCamera;
	public Camera SpectateCamera;
	public GameObject PcPrefab;
	public GameObject VivePrefab;

	private List<GameObject> players;
	private GameState state;
	private GameState lastState;
	private float lastStateChangeTime;
	private IceCubeSpawner iceSpawner;

	void Awake()
	{
		players = new List<GameObject> ();
		state = GameState.Waiting;
		iceSpawner = (IceCubeSpawner) FindObjectOfType(typeof(IceCubeSpawner));

		var player1 = Instantiate(PcPrefab);
		var ctrl = player1.GetComponent<PlayerController> ();
		ctrl.PlayerIndex = 0;
		var mv = player1.GetComponent<PlayerMovementAxis> ();
		mv.HorizontalAxis = "Horizontal1";
		mv.VerticalAxis = "Vertical1";
		player1.transform.parent = transform.parent;
		players.Add (player1);

		var player2 = Instantiate(PcPrefab);
		ctrl = player2.GetComponent<PlayerController> ();
		ctrl.PlayerIndex = 1;
		mv = player2.GetComponent<PlayerMovementAxis> ();
		mv.HorizontalAxis = "Horizontal2";
		mv.VerticalAxis = "Vertical2";
		player2.transform.parent = transform.parent;
		players.Add (player2);

//		var player3 = Instantiate(VivePrefab);
//		var ctrl = player3.GetComponent<PlayerController> ();
//		ctrl.PlayerIndex = 2;
//		player3.transform.parent = transform.parent;
//		players.Add (player3);
	}

    void Update()
    {
		if (state != lastState) {
			switch (state) {
				case GameState.Waiting: awake_Waiting(); break;
				case GameState.Running: awake_Running(); break;
				case GameState.RoundOver: awake_RoundOver(); break;
			}
			lastState = state;
			lastStateChangeTime = Time.time;
		}
		else
		{
			switch (state) {
				case GameState.Waiting: update_Waiting(); break;
				case GameState.Running: update_Running(); break;
				case GameState.RoundOver: update_RoundOver(); break;
			}
		}
    }

	private void awake_Waiting()
	{
		SpectateCamera.enabled = true;
		OverheadCamera.enabled = false;
	}

	private void awake_Running()
	{
		SpectateCamera.enabled = false;
		OverheadCamera.enabled = true;

		var toSpawn = players.OfPlayerState (PlayerState.Waiting, PlayerState.Dead).AsComponent<PlayerController> ();

		foreach (var p in toSpawn)
			p.Spawn ();
	}

	private void awake_RoundOver()
	{
		SpectateCamera.enabled = true;
		OverheadCamera.enabled = false;
	}

	private void update_Waiting()
	{
		int readyCount = players.OfPlayerState (PlayerState.Waiting).Count ();

		if (readyCount >= 2) {
			Debug.Log ("Starting game with players " + readyCount);
			state = GameState.Running;
		}
	}

	private void update_Running()
	{
		int aliveCount = players.OfPlayerState (PlayerState.Alive).Count();
		if (aliveCount <= 1) {
			Debug.Log ("Game ended... starting over");
			state = GameState.RoundOver;
		}
	}

	private void update_RoundOver()
	{
		if (Time.time - lastStateChangeTime < 3.0f)
			return;

		Debug.Log ("Resetting all players for next round...");
		foreach (var p in players.OfPlayerState (PlayerState.Dead, PlayerState.Alive).AsComponent<PlayerController> ()) {
			p.StartWaiting ();
		}
		iceSpawner.resetIceCubes ();

		state = GameState.Waiting;
	}
}