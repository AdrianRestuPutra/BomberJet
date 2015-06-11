using UnityEngine;
using UnityEngine.Cloud.Analytics;
using System.Collections;

public class UnityAnalyticsIntegration : MonoBehaviour {

	// Use this for initialization
	void Start () {
		const string projectId = "e203e536-999c-4cfb-a838-9af11fbd107e";
		UnityAnalytics.StartSDK (projectId);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
