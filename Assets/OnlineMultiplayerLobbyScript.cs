using UnityEngine;
using System.Collections;

public class OnlineMultiplayerLobbyScript : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject mainMenu;

	// Use this for initialization
	void Start () {
		PhotonNetwork.autoJoinLobby = true;
		if (!PhotonNetwork.connected)
			PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnEnable() {
		if (!PhotonNetwork.connected)
			PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			mainCamera.GetComponent<Animator>().SetTrigger("Left");
			mainMenu.GetComponent<MainMenuScript>().enabled = true;
			this.enabled = false;
		}
	
		if (PhotonNetwork.insideLobby)
			print ("We Are In Lobby");
	}
}
