using UnityEngine;
using System.Collections;

public class GameDataLocal : MonoBehaviour {

	public int jumlahPemain;
	public int[] controller = new int[4];

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		
		if (FindObjectsOfType(GetType()).Length > 1) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
