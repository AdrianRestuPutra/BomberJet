using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public GameObject localMultiplayerMenu;
	public GameObject onlineMultiplayerLobby;
	public GameObject mainCamera;
	public GameObject[] listMenu;
	
	public int indexMenu;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
		indexMenu = 0;
	}
	
	// Update is called once per frame
	void Update () {
		bool up   = Input.GetKeyDown(KeyCode.UpArrow);
		bool down = Input.GetKeyDown(KeyCode.DownArrow);
		
		bool select = Input.GetKeyDown(KeyCode.Return);
		
		if (down) indexMenu = (indexMenu + 1) % listMenu.Length;
		if (up)   indexMenu = ((indexMenu - 1) + listMenu.Length) % listMenu.Length;
		
		for(int i=0;i<listMenu.Length;i++) {
			listMenu[i].GetComponent<MenuSelectorScript>().isSelected = false;
		}
		
		listMenu[indexMenu].GetComponent<MenuSelectorScript>().isSelected = true;
		
		if (select) {
			if (indexMenu == 0) GoToLocalMultiplayer();
			if (indexMenu == 1) GoToOnlineMultiplayer();
		}
	}
	
	public void GoToLocalMultiplayer() {
		mainCamera.GetComponent<Animator>().SetTrigger("Up");
		localMultiplayerMenu.GetComponent<LocalMultiplayerMainMenuScript>().enabled = true;
		this.enabled = false;
	}
	
	public void GoToOnlineMultiplayer() {
		mainCamera.GetComponent<Animator>().SetTrigger("Right");
		onlineMultiplayerLobby.GetComponent<OnlineMultiplayerLobbyScript>().enabled = true;
		this.enabled = false;
	}
	
	public void GoToExit() {
		Application.Quit();
	}
}
