using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageManager : MonoBehaviour {

	public GameObject player;
	public GameObject winnerTag;
	public GameObject mazeGenerator;
	
	public GameObject[] jetPackFuel;
	
	public GameObject[] HUD;
	
	private bool isGameFinished = false;
	
	private float second = 0f;
	private float dropedBox = 0f;
	
	private float timePlay = 0f;

	void Awake () {
		GameObject gameData = GameObject.Find("Game Data");
		
		int jumlahPemain = gameData.GetComponent<GameDataLocal>().jumlahPemain;
		int[] controller = gameData.GetComponent<GameDataLocal>().controller;
		
		GameObject[] listPemain = new GameObject[jumlahPemain];
		
		for(int i=0;i<listPemain.Length;i++) {
			listPemain[i] = Instantiate(player);
			listPemain[i].GetComponent<PlayerController>().playerNumber = controller[i];
			listPemain[i].GetComponent<PlayerController>().jetPackFuelSlider = jetPackFuel[i];
		}
		
		for(int i=listPemain.Length;i<HUD.Length;i++)
			Destroy(HUD[i]);
		
		mazeGenerator.GetComponent<MazeGenerator>().players = listPemain;
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameFinished) {
			second += Time.deltaTime;
			if (second >= 3) {
				Application.LoadLevel(Application.loadedLevel);
				second = 0;
			}
		}
		
		timePlay += Time.deltaTime;
		if (timePlay >= 30) {
			timePlay = 0;
			//Time.timeScale = Mathf.Min(Time.timeScale + 0.25f, 2.5f);
		}
		
		dropedBox += Time.deltaTime;
		if (dropedBox >= 5f) {
			dropedBox = 0f;
			
			mazeGenerator.GetComponent<MazeGenerator>().PlaceBoxBonus();
		}
	}
	
	void FixedUpdate() {
		if (GameObject.FindGameObjectsWithTag("Player").Length == 1) {
			isGameFinished = true;
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameFinished();
			winnerTag.GetComponent<Text>().text = "WINNER";
			winnerTag.GetComponent<Text>().color = new Color(1, 1, 1, 1);
		} else if (GameObject.FindGameObjectsWithTag("Player").Length == 0) {
			isGameFinished = true;
			winnerTag.GetComponent<Text>().text = "DRAW";
			winnerTag.GetComponent<Text>().color = new Color(1, 1, 1, 1);
		}
	}
}
