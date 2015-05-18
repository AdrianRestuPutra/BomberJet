using UnityEngine;
using System.Collections;

public class Sensor : MonoBehaviour {

	public bool isActive = false;
	public string sensorName;
	
	private int collide = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (collide >= 1)
			isActive = true;
		else isActive = false;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == sensorName) {
			collide++;
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == sensorName) {
			collide--;
		}
	}
}
