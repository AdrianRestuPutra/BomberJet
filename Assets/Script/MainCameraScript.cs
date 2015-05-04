using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

	public GameObject[] player;
	
	// SHAKE CAMERA
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;
	private bool shaking;
	private Transform _transform;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnEnable() {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!shaking)
			return;
		if (shake_intensity > 0f){
			//_transform.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
			_transform.localRotation = new Quaternion(
				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		} else {
			shaking = false;
			//_transform.localPosition = originPosition;
			_transform.localRotation = originRotation;	
		}
	}
	
	public void Shake(){
		if(!shaking) {
			//originPosition = _transform.localPosition;
			originRotation = _transform.localRotation;
		}
		shaking = true;
		shake_intensity = .1f;
		shake_decay = 0.005f;
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
		
		GetComponent<Camera>().orthographicSize = Mathf.Max(cameraY, cameraX);
		
		gameObject.transform.position = new Vector3((xMin + xMax) / 2f, (yMin + yMax) / 2f, gameObject.transform.position.z);
	}
}
