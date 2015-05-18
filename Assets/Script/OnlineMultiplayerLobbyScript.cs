using UnityEngine;
using System.Collections;

public class OnlineMultiplayerLobbyScript : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject mainMenu;
	public GameObject onlineRoom;
	
	private RoomInfo[] roomsList;

	// Use this for initialization
	void Start () {
		PhotonNetwork.autoJoinLobby = true;
		PhotonNetwork.player.name = "Madya121";
		print (PhotonNetwork.sendRateOnSerialize);
		if (!PhotonNetwork.connected)
			PhotonNetwork.ConnectToMaster("192.168.1.102", 5055, "BomberJet", "0.1");
			//PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnEnable() {
		if (!PhotonNetwork.connected)
			PhotonNetwork.ConnectToMaster("192.168.1.102", 5055, "BomberJet", "0.1");
			//PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnGUI () {
		if (!PhotonNetwork.connected) return;
		
		if (GUI.Button(new Rect(100, 100, 250, 100), "Create Room")) {
			PhotonNetwork.CreateRoom("Testing Room Name", true, true, 4);
		}
		
		if (roomsList != null) {
			for (int i = 0; i < roomsList.Length; i++) {
				if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name + " (" 
					+ roomsList[i].playerCount + ") Player"))
					PhotonNetwork.JoinRoom(roomsList[i].name);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			mainCamera.GetComponent<Animator>().SetTrigger("Left");
			mainMenu.GetComponent<MainMenuScript>().enabled = true;
			this.enabled = false;
		}
		
		
	}
	
	void OnJoinedRoom () {
		mainCamera.GetComponent<Animator>().SetTrigger("Online Room");
		onlineRoom.GetComponent<OnlineMultiplayerRoomScript>().enabled = true;
		this.enabled = false;
	}
	
	void OnReceivedRoomListUpdate() {
		roomsList = PhotonNetwork.GetRoomList();
		print ("Ada Room");
	}
}
