using UnityEngine;
using System.Collections;

public class BoxBonus : MonoBehaviour {

	private int bonusId;
	private int X, Y;

	// Use this for initialization
	void Start () {
		bonusId = Random.Range(1, 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetPosition(int X, int Y) {
		this.X = X;
		this.Y = Y;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (bonusId == 1)
				other.gameObject.GetComponent<PlayerController>().AddMaxDropBomb(1);
			if (bonusId == 2)
				other.gameObject.GetComponent<PlayerController>().jetFuel = 1000;
			GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>().FreeBoxBonus(X, Y);
			Destroy(gameObject);
		}
	}
}
