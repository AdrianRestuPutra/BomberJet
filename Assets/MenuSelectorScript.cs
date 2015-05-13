using UnityEngine;
using System.Collections;

public class MenuSelectorScript : MonoBehaviour {

	public bool isSelected = false;
	
	public Sprite selected;
	public Sprite notSelected;
	
	public float scaleSelected = 0.8f;
	public float scaleNotSelected = 0.7f;
	
	public GameObject mainMenu;
	public int menuNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate() {
		if (isSelected == true) {
			transform.localScale = new Vector3(scaleSelected, scaleSelected, 1);
			GetComponent<SpriteRenderer>().sprite = selected;
		} else {
			transform.localScale = new Vector3(scaleNotSelected, scaleNotSelected, 1);
			GetComponent<SpriteRenderer>().sprite = notSelected;
		}
	}
	
	void OnMouseOver() {
		mainMenu.GetComponent<MainMenuScript>().indexMenu = menuNumber;
	}
	
	void OnMouseDown() {
		if (menuNumber == 0) mainMenu.GetComponent<MainMenuScript>().GoToLocalMultiplayer();
		if (menuNumber == 1) mainMenu.GetComponent<MainMenuScript>().GoToOnlineMultiplayer();
		if (menuNumber == 3) mainMenu.GetComponent<MainMenuScript>().GoToExit();
	}
}
