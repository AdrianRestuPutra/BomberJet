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
		GameObject left = Instantiate(horizontalBullet, transform.position, Quaternion.identity) as GameObject;
		GameObject right = Instantiate(horizontalBullet, transform.position, Quaternion.identity) as GameObject;
		GameObject up = Instantiate(verticalBullet, transform.position, Quaternion.identity) as GameObject;
		GameObject down = Instantiate(verticalBullet, transform.position, Quaternion.identity) as GameObject;
		
		left.GetComponent<BulletScript>().bulletForceHorizontal = -bulletForce;
		right.GetComponent<BulletScript>().bulletForceHorizontal = bulletForce;
		down.GetComponent<BulletScript>().bulletForceVertical = -bulletForce;
		up.GetComponent<BulletScript>().bulletForceVertical = bulletForce;
		
		/*left.GetComponent<ParticleSystem>().enableEmission = true;
		right.GetComponent<ParticleSystem>().enableEmission = true;
		up.GetComponent<ParticleSystem>().enableEmission = true;
		down.GetComponent<ParticleSystem>().enableEmission = true;*/
		
		left.GetComponent<BulletScript>().GoingLeft();
		down.GetComponent<BulletScript>().GoindDown();
		
		if (player)
			player.GetComponent<PlayerController>().AddMaxDropBomb(1);
		
		Destroy(gameObject);
	}
	
	public void AddBlastVorce(Vector3 expPosition, float expRadius, float expForce) {
		var dir = (gameObject.transform.position - expPosition);
		float calc = 1 - (dir.magnitude / expRadius);
		if (calc <= 0) {
			calc = 0;		
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		GetComponent<Rigidbody2D>().AddForce (dir.normalized * expForce * calc);
	}
}
