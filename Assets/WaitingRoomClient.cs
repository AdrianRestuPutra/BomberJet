using UnityEngine;
using System.Collections;

public class WaitingRoomClient : MonoBehaviour {
	
	public GameObject roomData;
	
	void OnGUI() {
		GUILayout.Label(PhotonNetwork.room.playerCount + "");
	}

	// Use this for initialization
	void Start () {
		PhotonNetwork.automaticallySyncScene = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
			StartGame();
	}
	
	void StartGame () {
		PhotonPlayer[] players = PhotonNetwork.playerList;
		int index = 1;
		foreach(PhotonPlayer player in players) {
			if (player.GetHashCode() == PhotonNetwork.player.GetHashCode())
				roomData.GetComponent<RoomData>().myNumber = index;
			index++;
		}
		
		roomData.GetComponent<RoomData>().countPlayer = players.Length;
		
		PhotonNetwork.LoadLevel("Multiplayer Scene");
	}
}
