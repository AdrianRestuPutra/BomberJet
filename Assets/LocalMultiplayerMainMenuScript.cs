using UnityEngine;
using System.Collections;

public class LocalMultiplayerMainMenuScript : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject mainMenu;
	public GameObject playerMenu;
	public GameObject countdown;

	private GameObject[] listPlayer = new GameObject[4];
	private int[] gamepadPlayer = new int[4];
	private bool[] inputList = new bool[5];
	private bool[] cancelList = new bool[5];
	private bool[] sudah = new bool[5];

	// Use this for initialization
	void Start () {
		for(int i=0;i<sudah.Length;i++)
			sudah[i] = false;
	}
	
	// Update is called once per frame
	void Update () {
		InputManager();
		PlayerCount();
	}
	
	void PlayerCount() {
		int sum = 0;
		for(int i=0;i<4;i++) {
			if (listPlayer[i])
				sum++;
		}
		
		if (sum >= 2) countdown.GetComponent<StartGameCountdown>().StartCountdown();
		else countdown.GetComponent<StartGameCountdown>().DisableCountdown();
	}
	
	void InputManager() {
		inputList[0] = Input.GetKeyDown(KeyCode.Space);				// 0
		inputList[1] = Input.GetKeyDown(KeyCode.Joystick1Button0);	// 1
		inputList[2] = Input.GetKeyDown(KeyCode.Joystick2Button0);	// 2
		inputList[3] = Input.GetKeyDown(KeyCode.Joystick3Button0);	// 3
		inputList[4] = Input.GetKeyDown(KeyCode.Joystick4Button0);	// 4
		
		bool adaInput = false;
		for(int i=0;i<5;i++) adaInput |= inputList[i];
		
		if (adaInput) {
			for(int i=0;i<5;i++) {
				if (inputList[i] && sudah[i] == false) {
					for(int j=0;j<4;j++) {
						if (listPlayer[j] == null) {
							listPlayer[j] = Instantiate(playerMenu);
							gamepadPlayer[j] = i;
							listPlayer[j].GetComponent<PlayerControllerMenu>().playerNumber = i;
							sudah[i] = true;
							break;
						}
					}
				}
			}
		}
		
		cancelList[0] = Input.GetKeyDown(KeyCode.Escape);			// 0
		cancelList[1] = Input.GetKeyDown(KeyCode.Joystick1Button1);	// 1
		cancelList[2] = Input.GetKeyDown(KeyCode.Joystick2Button1);	// 2
		cancelList[3] = Input.GetKeyDown(KeyCode.Joystick3Button1);	// 3
		cancelList[4] = Input.GetKeyDown(KeyCode.Joystick4Button1);	// 4
		
		bool adaCancel = false;
		for(int i=0;i<5;i++) adaCancel |= cancelList[i];
		
		if (adaCancel) {
			for(int i=0;i<5;i++) {
				if (cancelList[i] && sudah[i] == true) {
					for(int j=0;j<4;j++) {
						if (gamepadPlayer[j] == i) {
							sudah[i] = false;
							Destroy(listPlayer[j]);
							break;
						}
					}
				}
			}
		}
		
		if (adaCancel) {
			int sumPlayer = 0;
			for(int i=0;i<4;i++) {
				if (listPlayer[i])
					sumPlayer++;
			}
			
			if (sumPlayer == 0) {
				mainMenu.GetComponent<MainMenuScript>().enabled = true;
				this.enabled = false;
				mainCamera.GetComponent<Animator>().SetTrigger("Down");
			}
		}
	}
	
	public GameObject[] GetListPlayer() {
		return listPlayer;
	}
	
	public int[] GetGamePadPlayer() {
		return gamepadPlayer;
	}
}
