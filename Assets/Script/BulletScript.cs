using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	public float bulletForceHorizontal = 0;
	public float bulletForceVertical = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate () {
		rigidbody2D.velocity = new Vector2(bulletForceHorizontal, bulletForceVertical);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Wall") {
			GameObject.Find("Main Camera").GetComponent<MainCameraScript>().Shake();
			Destroy(gameObject);
		}
	}
}
