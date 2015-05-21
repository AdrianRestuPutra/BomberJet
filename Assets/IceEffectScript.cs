using UnityEngine;
using System.Collections;

public class IceEffectScript : MonoBehaviour {

	public GameObject owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void DestroyIceEffect () {
		Destroy(gameObject);
	}
	
	public void MoveIce () {
		gameObject.transform.position = new Vector3(1000, 1000);
	}
}
