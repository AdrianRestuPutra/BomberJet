using UnityEngine;
using System.Collections;

public class PhotonNetworkManager : MonoBehaviour {

	private const string roomName = "RoomName";
	private RoomInfo[] roomsList;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnGUI() {
		if (!PhotonNetwork.connected) {
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		} else if (PhotonNetwork.room == null) {
			// Create Room
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")) {
				PhotonNetwork.CreateRoom(roomName, true, true, 5);
			}			
		}
		
		if (roomsList != null) {
			for (int i = 0; i < roomsList.Length; i++) {
				if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name))
					PhotonNetwork.JoinRoom(roomsList[i].name);
			}
		}
	}
	
	void OnReceivedRoomListUpdate() {
		roomsList = PhotonNetwork.GetRoomList();
		print ("Ada Room");
	}
	
	void OnJoinedRoom() {
		Application.LoadLevel("Waiting Room Client");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
