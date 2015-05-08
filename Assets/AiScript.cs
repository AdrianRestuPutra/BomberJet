using UnityEngine;
using System.Collections;

public class AiScript : MonoBehaviour {

	public GameObject player;

	// BOMB SENSOR
	public GameObject horizontal;
	public GameObject vertical;
	
	// WALK SENSOR
	public GameObject right;
	public GameObject bottom;
	public GameObject left;
	public GameObject up;
	
	private PlayerController playerController;
	
	private Sensor rightSensor;
	private Sensor bottomSensor;
	private Sensor leftSensor;
	private Sensor upSensor;
	
	/*
		0 = Right
		1 = Down
		2 = Left
		3 = Up
	*/
	private int goTo = 0;
	
	private bool canUseJet = false;

	// Use this for initialization
	void Start () {
		playerController = player.GetComponent<PlayerController>();
	
		rightSensor = right.GetComponent<Sensor>();
		bottomSensor = bottom.GetComponent<Sensor>();
		leftSensor = left.GetComponent<Sensor>();
		upSensor = up.GetComponent<Sensor>();
		
		goTo = Random.Range(0, 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate () {
		if (upSensor.isActive)
			canUseJet = false;
		if (bottomSensor.isActive)
			canUseJet = true;
	
		if (goTo == 0) {
			playerController.GoRight();
			if (rightSensor.isActive == true)
				playerController.GoingUp();
			else playerController.GoingDown();
		} else if (goTo == 2) {
			playerController.GoLeft();
			if (leftSensor.isActive == true)
				playerController.GoingUp();
			else playerController.GoingDown();
		}
	}
}
