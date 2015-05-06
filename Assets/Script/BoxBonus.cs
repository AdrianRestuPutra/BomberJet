using UnityEngine;
using System.Collections;

public class BoxBonus : MonoBehaviour {

	private int bonusId;

	// Use this for initialization
	void Start () {
		bonusId = Random.Range(1, 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (bonusId == 1)
				other.gameObject.GetComponent<PlayerController>().AddMaxDropBomb(1);
			if (bonusId == 2)
				other.gameObject.GetComponent<PlayerController>().jetFuel = 1000;
			Destroy(gameObject);
		}
	}
}
