using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public int secondBlow = 3;
	public GameObject timer;
	
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
		Destroy(gameObject);
	}
}
