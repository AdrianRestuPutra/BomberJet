using UnityEngine;
using System.Collections;

public class StartGameCountdown : MonoBehaviour {

	public GameObject localMultiplayerMenu;
	
	private GameObject gameData;
	private int countdown = 100;
	private bool stopCountdown = true;
	private float second = 0;

	void Awake () {
		gameData = GameObject.Find("Game Data");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		second += Time.deltaTime;
		if (second >= 1 && stopCountdown == false) {
			GetComponent<TextMesh>().text = countdown + "";
			countdown--;
			second = 0;
		}
		
		if (countdown <= -1) {
			GameObject[] listPlayer = localMultiplayerMenu.GetComponent<LocalMultiplayerMainMenuScript>().GetListPlayer();
			int[] listGamepad = localMultiplayerMenu.GetComponent<LocalMultiplayerMainMenuScript>().GetGamePadPlayer();
			
			int sum = 0;
			
			for(int i=0;i<4;i++) {
				if (listPlayer[i])
					sum++;
			}
			gameData.GetComponent<GameDataLocal>().jumlahPemain  = sum;
			
			int index = 0;
			for(int i=0;i<4;i++) {
				if (listPlayer[i]) {
					gameData.GetComponent<GameDataLocal>().controller[index] = listGamepad[i];
					index++;
				}
			}
			
			Application.LoadLevel("Game Play Scene");
		}
	}
	
	public void StartCountdown() {
		if (stopCountdown == true) {
			stopCountdown = false;
			countdown = 100;
			GetComponent<TextMesh>().text = countdown + "";
			GetComponent<MeshRenderer>().enabled = true;
		}
	}
	
	public void DisableCountdown() {
		if (stopCountdown == false) {
			stopCountdown = true;
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
