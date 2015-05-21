using UnityEngine;
using System.Collections;

public class PointCharacterSelectionScript : MonoBehaviour {

	public GameObject[] pointCharacterSelection;
	public Sprite[] characterSelector;
	
	public int[] selectionArray = new int[16];

	void Awake () {
		for(int i=0;i<selectionArray.Length;i++)
			selectionArray[i] = -1;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate () {
		VisualizePointCharacterSelection();
	}
	
	void VisualizePointCharacterSelection() {
		for(int i=0;i<selectionArray.Length;i++) {
			if (selectionArray[i] == -1)
				pointCharacterSelection[i].GetComponent<SpriteRenderer>().enabled = false;
			else {
				pointCharacterSelection[i].GetComponent<SpriteRenderer>().sprite = characterSelector[selectionArray[i]];
				pointCharacterSelection[i].GetComponent<SpriteRenderer>().enabled = true;
			}
		}
	}
	
	public void CharacterChoose(GameObject player, int PlayerNumber, int Indicator) {
		int index = 0;
		int before = 0;
		for(int i=0;i<selectionArray.Length;i++) {	
			if (selectionArray[i] == PlayerNumber) {
				index = i;
				before = i;
				break;
			}
		}
	
		while (selectionArray[index] != -1) {
			index = ((index + Indicator) + selectionArray.Length) % selectionArray.Length;
		}
		
		player.GetComponent<PlayerControllerMenu>().specialNumber = (int)(index / 4);
		selectionArray[index] = PlayerNumber;
		selectionArray[before] = -1;
	}
	
	public void NewPlayerJoin(GameObject player, int PlayerNumber) {
		int index = 0;
		while (selectionArray[index] != -1) {
			index++;
		}
		
		player.GetComponent<PlayerControllerMenu>().specialNumber = (int)(index / 4);
		selectionArray[index] = PlayerNumber;
	}
	
	public void RemovePlayer(int PlayerNumber) {
		for(int i=0;i<selectionArray.Length;i++) {
			if (selectionArray[i] == PlayerNumber) {
				selectionArray[i] = -1;
				break;
			}
		}
	}
}
