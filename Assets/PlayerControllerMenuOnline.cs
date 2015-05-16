using UnityEngine;
using System.Collections;

public class PlayerControllerMenuOnline : Photon.MonoBehaviour {

	public int playerNumber;
	public GameObject bomb;
	
	private float moveForce = 10;
	private float jetForce = 10;
	
	private float axis;
	private float jetPack;
	
	private Rigidbody2D rigidbody2D;
	
	void Awake () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		endPosition = transform.position;
		if (!photonView.isMine)
			Destroy(rigidbody2D);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine) SyncedMovement();
		else InputManager();
	}
	
	private Vector3 endPosition;
	
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext(transform.position);
		} else {
			syncEndPosition = (Vector3)stream.ReceiveNext();
			syncStartPosition = transform.position;
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
		}
	}
	
	private void SyncedMovement() {
		syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void InputManager() {
		bool left = Input.GetKey(KeyCode.LeftArrow);
		bool right = Input.GetKey(KeyCode.RightArrow);
		bool jet = Input.GetKey(KeyCode.UpArrow);
		bool dropBomb = Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton2);
		
		if (left) axis = -0.5f;
		else if (right) axis = 0.5f;
		else axis = 0;
		
		axis = Input.GetAxis("Player_Axis");
		
		if (jet) jetPack = 0.5f;
		else jetPack = 0;
		
		jetPack = Input.GetAxis("Player_Jetpack");
		
		if (dropBomb)
			DropBomb(gameObject.transform.position);
	}
	
	[RPC]
	void DropBomb(Vector3 position) {
		GameObject _bomb = Instantiate(bomb) as GameObject;
		_bomb.transform.position = position;
		
		if (photonView.isMine)
			PhotonNetwork.RPC(photonView, "DropBomb", PhotonTargets.Others, true, position);
	}
	
	void FixedUpdate() {
		if (!photonView.isMine) return;
		if (axis <= -0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(-moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else if (axis >= 0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(moveForce, GetComponent<Rigidbody2D>().velocity.y);
		else GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		
		if (jetPack >= 0.5f) GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jetForce);
	}
}
