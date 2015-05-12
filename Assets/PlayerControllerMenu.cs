using UnityEngine;
using System.Collections;

public class PlayerControllerMenu : MonoBehaviour {

	public int playerNumber;
	public GameObject bomb;
	
	private float moveForce = 10;
	private float jetForce = 10;
	private int maxDropBomb = 1;
	
	private float axis;
	private float jetPack;
	private bool dropBomb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		InputManager();
	}
	
	void InputManager() {
		if (playerNumber == 0) {
			bool left = Input.GetKey(KeyCode.LeftArrow);
			bool right = Input.GetKey(KeyCode.RightArrow);
			bool jet = Input.GetKey(KeyCode.UpArrow);
			
			if (left) axis = -0.5f;
			else if (right) axis = 0.5f;
			else axis = 0;
			
			if (jet) jetPack = 0.5f;
			else jetPack = 0;
			
			dropBomb = Input.GetKeyDown(KeyCode.X);
		}
		if (playerNumber == 1) {
			axis = Input.GetAxis("Player1_Axis");
			jetPack = Input.GetAxis("Player1_JetPack");
			dropBomb = Input.GetKeyDown(KeyCode.Joystick1Button2);
		}
		if (playerNumber == 2) {
			axis = Input.GetAxis("Player2_Axis");
			jetPack = Input.GetAxis("Player2_JetPack");
			dropBomb = Input.GetKeyDown(KeyCode.Joystick2Button2);
		}
		
		if (dropBomb) {
			GameObject _bomb = Instantiate(bomb) as GameObject;
			_bomb.transform.position = gameObject.transform.position;
		}
	}
	
	void FixedUpdate() {
		if (axis <= -0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(-moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else if (axis >= 0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		
		if (jetPack >= 0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jetForce);
	}
}
