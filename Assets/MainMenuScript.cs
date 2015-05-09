using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public GameObject localMultiplayerMenu;
	public GameObject mainCamera;
	public GameObject[] listMenu;
	
	private int indexMenu;

	// Use this for initialization
	void Start () {
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
			if (indexMenu == 0) {
				mainCamera.GetComponent<Animator>().SetTrigger("Up");
				localMultiplayerMenu.GetComponent<LocalMultiplayerMainMenuScript>().enabled = true;
				this.enabled = false;
			}
		}
	}
}
