using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public int secondBlow = 3;
	public GameObject timer;
	public GameObject horizontalBullet;
	public GameObject verticalBullet;
	public float bulletForce = 5;
	public GameObject player;
	
	private float secondPass = 0;

	// Use this for initialization
	void Start () {
		timer.GetComponent<MeshRenderer>().sortingOrder = 4;
	}
	
	// Update is called once per frame
	void Update () {
		secondPass += Time.deltaTime;
		if (secondPass >= 0.5f) {
			secondBlow--;
			secondPass = 0;
		}
		
		if (secondBlow == 0)
			BombExplode();
	}
	
	void FixedUpdate() {
		timer.GetComponent<TextMesh>().text = secondBlow + "";
	}
	
	void BombExplode() {
		GameObject left = Instantiate(horizontalBullet) as GameObject;
		GameObject right = Instantiate(horizontalBullet) as GameObject;
		GameObject up = Instantiate(verticalBullet) as GameObject;
		GameObject down = Instantiate(verticalBullet) as GameObject;
		
		left.GetComponent<BulletScript>().bulletForceHorizontal = -bulletForce;
		right.GetComponent<BulletScript>().bulletForceHorizontal = bulletForce;
		down.GetComponent<BulletScript>().bulletForceVertical = -bulletForce;
		up.GetComponent<BulletScript>().bulletForceVertical = bulletForce;
		
		left.transform.position = transform.position;
		right.transform.position = transform.position;
		down.transform.position = transform.position;
		up.transform.position = transform.position;
		
		if (player)
			player.GetComponent<PlayerController>().AddMaxDropBomb(1);
		
		Destroy(gameObject);
	}
}
