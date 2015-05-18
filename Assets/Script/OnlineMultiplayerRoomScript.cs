using UnityEngine;
using System.Collections;

public class OnlineMultiplayerRoomScript : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject onlineMultiplayerLobby;
	
	public GameObject player;
	
	private GameObject _player;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnEnable () {
		_player = PhotonNetwork.Instantiate(player.name, new Vector3(200, 50), Quaternion.identity, 0);
	}
	
	void OnGUI () {
		PhotonPlayer[] players = PhotonNetwork.playerList;
		for(int i=0;i<players.Length;i++) {
			if (players[i].Equals(PhotonNetwork.player))
				GUI.Label(new Rect(10, (i + 1) * 20, 200, 20), players[i].name + " (" + players[i].ID + ")" + " | Ping : " + PhotonNetwork.GetPing());
			else GUI.Label(new Rect(10, (i + 1) * 20, 200, 20), players[i].name + " (" + players[i].ID + ")");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			PhotonNetwork.LeaveRoom();
		}
	}
	
	void OnLeftRoom () {
		PhotonNetwork.Destroy(_player);
		mainCamera.GetComponent<Animator>().SetTrigger("Back Lobby");
		onlineMultiplayerLobby.GetComponent<OnlineMultiplayerLobbyScript>().enabled = true;
		this.enabled = false;
	}
}
