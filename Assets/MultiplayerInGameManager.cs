using UnityEngine;
using System.Collections;

public class MultiplayerInGameManager : MonoBehaviour {

	public GameObject player;
	public GameObject mazeGeneratorOnline;
	
	public GameObject SliderPlayer1;
	public GameObject SliderPlayer2;
	public GameObject SliderPlayer3;
	public GameObject SliderPlayer4;

	// Use this for initialization
	void Start () { 
		int myNumber = GameObject.Find("Room Data").GetComponent<RoomData>().myNumber;
	
		GameObject _player = PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity, 0);
		
		if (myNumber == 1) {
			_player.GetComponent<PlayerController>().jetPackFuelSlider = SliderPlayer1;
			mazeGeneratorOnline.GetComponent<MazeGeneratorOnlinePlay>().SpawnPlayer(_player, 1);
		} else if (myNumber == 2) {
			_player.GetComponent<PlayerController>().jetPackFuelSlider = SliderPlayer2;
			mazeGeneratorOnline.GetComponent<MazeGeneratorOnlinePlay>().SpawnPlayer(_player, 2);
		} else if (myNumber == 3) {
			_player.GetComponent<PlayerController>().jetPackFuelSlider = SliderPlayer3;
			mazeGeneratorOnline.GetComponent<MazeGeneratorOnlinePlay>().SpawnPlayer(_player, 3);
		} else if (myNumber == 4) {
			_player.GetComponent<PlayerController>().jetPackFuelSlider = SliderPlayer4;
			mazeGeneratorOnline.GetComponent<MazeGeneratorOnlinePlay>().SpawnPlayer(_player, 4);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
