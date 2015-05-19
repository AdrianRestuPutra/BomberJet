using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	public float bulletForceHorizontal = 0;
	public float bulletForceVertical = 0;
	
	public GameObject soundExplode;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void GoingLeft() {
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}
	
	public void GoindDown() {
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
	}
	
	void FixedUpdate () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(bulletForceHorizontal, bulletForceVertical);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Wall") {
			if (GameObject.Find("Main Camera").GetComponent<MainCameraScript>())
				GameObject.Find("Main Camera").GetComponent<MainCameraScript>().Shake();
			
			Instantiate(soundExplode, transform.position, transform.rotation);
			
			Destroy(gameObject);
		} else {
			if (other.tag == "Player") {
				if (other.gameObject.GetComponent<PlayerController>())
					other.gameObject.GetComponent<PlayerController>().Dead();
			}
		}
	}
}
