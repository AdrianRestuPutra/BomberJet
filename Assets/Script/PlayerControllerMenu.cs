using UnityEngine;
using System.Collections;

public class PlayerControllerMenu : MonoBehaviour {

	public int controllerType;
	public int playerNumber;
	public GameObject bomb;
	
	private float moveForce = 10;
	private float jetForce = 10;
	private int maxDropBomb = 1;
	
	private float axis;
	private float jetPack;
	private bool dropBomb;
	private bool nextPlayer, prevPlayer;
	
	private GameObject pointCharacterSelection;

	void Awake () {
		pointCharacterSelection = GameObject.Find("Point Character Selection Script");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		InputManager();
	}
	
	void InputManager() {
		if (controllerType == 0) {
			bool left = Input.GetKey(KeyCode.LeftArrow);
			bool right = Input.GetKey(KeyCode.RightArrow);
			bool jet = Input.GetKey(KeyCode.UpArrow);
			
			nextPlayer = Input.GetKeyDown(KeyCode.E);
			prevPlayer = Input.GetKeyDown(KeyCode.Q);
			
			if (left) axis = -0.5f;
			else if (right) axis = 0.5f;
			else axis = 0;
			
			if (jet) jetPack = 0.5f;
			else jetPack = 0;
			
			dropBomb = Input.GetKeyDown(KeyCode.X);
		}
		if (controllerType == 1) {
			axis = Input.GetAxis("Player1_Axis");
			jetPack = Input.GetAxis("Player1_JetPack");
			dropBomb = Input.GetKeyDown(KeyCode.Joystick1Button2);
			prevPlayer = Input.GetKeyDown(KeyCode.Joystick1Button4);
			nextPlayer = Input.GetKeyDown(KeyCode.Joystick1Button5);
		}
		if (controllerType == 2) {
			axis = Input.GetAxis("Player2_Axis");
			jetPack = Input.GetAxis("Player2_JetPack");
			dropBomb = Input.GetKeyDown(KeyCode.Joystick2Button2);
			prevPlayer = Input.GetKeyDown(KeyCode.Joystick2Button4);
			nextPlayer = Input.GetKeyDown(KeyCode.Joystick2Button5);
		}
		
		if (dropBomb) {
			GameObject _bomb = Instantiate(bomb) as GameObject;
			_bomb.transform.position = gameObject.transform.position;
		}
		
		if (nextPlayer) pointCharacterSelection.GetComponent<PointCharacterSelectionScript>().CharacterChoose(playerNumber, 1);
		if (prevPlayer) pointCharacterSelection.GetComponent<PointCharacterSelectionScript>().CharacterChoose(playerNumber, -1);
	}
	
	void FixedUpdate() {
		if (axis <= -0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(-moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else if (axis >= 0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		
		if (jetPack >= 0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jetForce);
	}
}
