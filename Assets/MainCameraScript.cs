using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

	public GameObject[] player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate() {
		float xMin = player[0].transform.position.x;
		float xMax = player[0].transform.position.x;
		float yMin = player[0].transform.position.y;
		float yMax = player[0].transform.position.y;
		
		for(int i=0;i<player.Length;i++) {
			xMin = Mathf.Min(xMin, player[i].transform.position.x);
			xMax = Mathf.Max(xMax, player[i].transform.position.x);
			
			yMin = Mathf.Min(yMin, player[i].transform.position.y);
			yMax = Mathf.Max(yMax, player[i].transform.position.y);
		}
		
		float differentY = yMax - yMin;
		float differentX = xMax - xMin;
		
		float cameraY = Mathf.Max(differentY * 0.7f, 10);
		float cameraX = Mathf.Max(differentX * 0.5f, 10);
		
		camera.orthographicSize = Mathf.Max(cameraY, cameraX);
		
		gameObject.transform.position = new Vector3((xMin + xMax) / 2f, (yMin + yMax) / 2f, gameObject.transform.position.z);
	}
}
