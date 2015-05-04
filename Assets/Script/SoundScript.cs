using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public float destroyAfter = 1;
	
	private float second = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		second += Time.deltaTime;
		if (second >= destroyAfter) {
			Destroy(gameObject);
		}
	}
}
