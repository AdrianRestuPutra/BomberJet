using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageManager : MonoBehaviour {

	public GameObject winnerTag;
	public GameObject mazeGenerator;
	
	private bool isGameFinished = false;
	
	private float second = 0f;
	private float dropedBox = 0f;

	// Use this for initialization
	void Start () {
		
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
			winnerTag.GetComponent<Text>().color = new Color(1, 1, 1, 1);
		}
	}
}
