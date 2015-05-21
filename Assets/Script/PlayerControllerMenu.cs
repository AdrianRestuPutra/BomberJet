using UnityEngine;
using System.Collections;

public class PlayerControllerMenu : MonoBehaviour {

	public int controllerType;
	public int playerNumber;
	
	public GameObject selector;
	public Sprite[] playerSelector;
	
	public GameObject bomb;
	public GameObject freezeBox;
	public GameObject blastEffect;
	public GameObject portalIn;
	public GameObject portalOut;
	
	private float moveForce = 10;
	private float jetForce = 10;
	private int maxDropBomb = 1;
	
	private float axis;
	private float jetPack;
	private bool dropBomb;
	private bool nextPlayer, prevPlayer;
	private bool special = false;
	private bool freezing = false;
	
	private float cantMoveSecond = 0;
	
	private GameObject pointCharacterSelection;

	void Awake () {
		pointCharacterSelection = GameObject.Find("Point Character Selection Script");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (cantMoveSecond <= 0) {
			cantMoveSecond = 0;
			InputManager();
		}
		cantMoveSecond -= Time.deltaTime;
	}
	
	void InputManager() {
		if (controllerType == 0) {
			bool left = Input.GetKey(KeyCode.LeftArrow);
			bool right = Input.GetKey(KeyCode.RightArrow);
			bool jet = Input.GetKey(KeyCode.UpArrow);
			special = Input.GetKeyDown(KeyCode.Z);
			
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
		
		if (special) DoSpecial();
	}
	
	void DoSpecial() {
//		SpecialFreeze();
		SpecialBlast();
//		SpecialBlink();
	}
	
	void SpecialBlast() {
		GameObject[] bomb = GameObject.FindGameObjectsWithTag("Bomb");
		for(int i=0;i<bomb.Length;i++)
			bomb[i].GetComponent<BombScript>().AddBlastForce(transform.position, 10, 1000);
			
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject player in players) {
			if (player.Equals(gameObject) == false) {
				player.GetComponent<PlayerControllerMenu>().AddBlastForce(transform.position, 10, 1000);
			}
		}
		
		Instantiate(blastEffect, gameObject.transform.position, Quaternion.identity);
	}
	
	void SpecialFreeze() {
		GameObject ice = Instantiate(freezeBox, gameObject.transform.position, Quaternion.identity) as GameObject;
		ice.GetComponent<IceEffectScript>().owner = gameObject;
	}
	
	void SpecialBlink() {
		Vector3 blinkTo = new Vector3(0, 50, 0);
		Instantiate(portalOut, blinkTo, Quaternion.identity);
		Instantiate(portalIn, transform.position, Quaternion.identity);
		transform.position = blinkTo;
	}
	
	public void AddBlastForce(Vector3 expPosition, float expRadius, float expForce) {
		var dir = (gameObject.transform.position - expPosition);
		float calc = 1 - (dir.magnitude / expRadius);
		if (calc <= 0) {
			calc = 0;
		}
		if (calc == 0) return;
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		GetComponent<Rigidbody2D>().AddForce (dir.normalized * expForce * calc);
		cantMoveSecond = 0.5f;
	}
	
	void FixedUpdate() {
		FixedUpdateMove();
		VisualizeSelector();
	}
	
	void VisualizeSelector() {
		selector.GetComponent<SpriteRenderer>().sprite = playerSelector[playerNumber];
	}
	
	void FixedUpdateMove() {
		if (cantMoveSecond > 0) return;
		if (axis <= -0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(-moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else if (axis >= 0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		
		if (jetPack >= 0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jetForce);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Ice") && freezing == false) {
			if (other.gameObject.GetComponent<IceEffectScript>().owner.Equals(gameObject) == true)
				return;
			freezing = true;
			moveForce /= 4f;
			jetForce /= 4f;
			GetComponent<Rigidbody2D>().gravityScale = 1;
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (freezing == false)	return;
		if (other.gameObject.tag.Equals("Ice")) {
			moveForce *= 4f;
			jetForce *= 4f;
			GetComponent<Rigidbody2D>().gravityScale = 3;
			freezing = false;
		}
	}
}
