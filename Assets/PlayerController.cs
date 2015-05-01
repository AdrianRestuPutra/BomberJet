using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveForce = 5;
	public float jetForce = 5;
	public int playerNumber;
	public GameObject bomb;
	
	private float axis, jetPack;
	private bool dropBomb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		InputManager();
	}
	
	void InputManager() {
		if (playerNumber == 1) {
			axis = Input.GetAxis("Player1_Axis");
			jetPack = Input.GetAxis("Player1_JetPack");
			dropBomb = Input.GetKeyDown(KeyCode.Joystick1Button1);
		}
		if (playerNumber == 2) {
			axis = Input.GetAxis("Player2_Axis");
			jetPack = Input.GetAxis("Player2_JetPack");
		}
		
		if (dropBomb) DropBomb();
	}
	
	void DropBomb() {	
		GameObject _bomb = Instantiate(bomb) as GameObject;
		_bomb.transform.position = gameObject.transform.position;
	}
	
	void FixedUpdate() {
		if (axis <= -0.5f) rigidbody2D.velocity = new Vector2(-moveForce, rigidbody2D.velocity.y);
		else if (axis >= 0.5f) rigidbody2D.velocity = new Vector2(moveForce, rigidbody2D.velocity.y);
		else rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		
		if (jetPack >= 0.5f) rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jetForce);
	}
}
