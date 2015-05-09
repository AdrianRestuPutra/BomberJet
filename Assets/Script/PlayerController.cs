using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : Photon.MonoBehaviour {

	public float moveForce = 5;
	public float jetForce = 5;
	public int playerNumber;
	public GameObject bomb;
	
	// JET PACK THING
	public GameObject jetPackFuelSlider;
	public int jetFuel = 1000;
	public int reduceFuelFactor = 2;
	public int addFuelFactor = 1;
	public float fuelSecondConstant = 0.01f;
	public bool canDoJet = true;
	
	// DROP BOMB THING
	public int maxDropBomb = 1;
	
	private float axis, jetPack;
	private bool dropBomb;
	private float second = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView) {
			if (photonView.isMine)
				InputManager();
		} else InputManager();
		CalculateJetFuel();
	}
	
	void CalculateJetFuel() {
		second += Time.deltaTime;
		if (second >= fuelSecondConstant) {
			second = 0.0f;
			if (jetPack >= 0.5f && canDoJet) {
				if (jetFuel > 0)
					jetFuel -= reduceFuelFactor;
			} else {
				if (jetFuel < 1000) 
					jetFuel += addFuelFactor;
			}
			if (jetFuel <= 0) {
				jetFuel = 0;
				canDoJet = false;
			}
			if (jetFuel >= 1000) {
				jetFuel = 1000;
				canDoJet = true;
			}
			jetPackFuelSlider.GetComponent<Slider>().value = jetFuel;
		}
	}
	
	void InputManager() {
		if (Input.GetKeyDown(KeyCode.M))
			Application.LoadLevel("Main Menu");
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel(Application.loadedLevel);
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
		
		if (dropBomb && maxDropBomb > 0) DropBomb();
	}
	
	void DropBomb() {	
		GameObject _bomb = Instantiate(bomb) as GameObject;
		_bomb.GetComponent<BombScript>().player = gameObject;
		_bomb.transform.position = gameObject.transform.position;
		
		maxDropBomb--;
	}
	
	public void AddMaxDropBomb(int bomb) {
		maxDropBomb += bomb;
	}
	
	public void GoingUp() {
		jetPack = 1f;
	}
	
	public void GoingDown() {
		jetPack = 0f;
	}
	
	public void GoLeft() {
		axis = -1f;
	}
	
	public void GoRight() {
		axis = 1f;
	}
	
	public void Stop() {
		axis = 0f;
	}
	
	void FixedUpdate() {
		if (axis <= -0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(-moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else if (axis >= 0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		
		if (jetPack >= 0.5f && canDoJet) GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jetForce);
	}
	
	public void Dead() {
		Destroy(gameObject);
	}
	
	public void GameFinished() {
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<Rigidbody2D>().isKinematic = true;
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		enabled = false;
	}
}
