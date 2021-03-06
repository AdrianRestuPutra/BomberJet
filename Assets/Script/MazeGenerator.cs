﻿using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

	public GameObject frontPrefab;
	public GameObject backgroundPrefab;
	public GameObject shadowPrefab;
	
	public GameObject box;
	
	public GameObject[] players;
	
	public GameObject puzzleMapObject;
	
	public int r = 20;
	public int c = 20;
	
	private int[,] puzzleMap;
	
	private int[] topX = {0, -1, -1, -1, 0};
	private int[] topY = {-1, -1, 0, 1, 1};
	private int[] rightX = {-1, -1, 0, 1, 1};
	private int[] rightY = {0, 1, 1, 1, 0};
	private int[] downX = {0, 1, 1, 1, 0};
	private int[] downY = {1, 1, 0, -1, -1};
	private int[] leftX = {1, 1, 0, -1, -1};
	private int[] leftY = {0, -1, -1, -1, 0};
	
	private Stack stack = new Stack();

	// Use this for initialization
	void Start () {
		InitializeMap();
		StartGenerating();
		VisualizeMap();
		SpawnPlayer();
	}
	
	void InitializeMap() {
		puzzleMap = new int[r, c];
		
		for(int i=0;i<r;i++) {
			for(int j=0;j<c;j++)
				puzzleMap[i, j] = 1;
		}
	}
	
	void StartGenerating() {
		int x = Random.Range(0, r);
		int y = Random.Range(0, c);
		
		puzzleMap[x, y] = 0;
		
		stack.Push(new Vector2(x, y));
		
		while (stack.Count != 0) {
			Vector2 top = (Vector2)stack.Pop();
			stack.Push(top);
			
			/* SHUFFLE ARRAY
				1 = TOP
				2 = RIGHT
				3 = DOWN
				4 = LEFT
			*/
			int[] arr = {1, 2, 3, 4};
			for(int i=0;i<20;i++) {
				int _x = Random.Range(0, 4);
				int _y = Random.Range(0, 4);
				
				int T = arr[_x];
				arr[_x] = arr[_y];
				arr[_y] = T;
			}
			
			x = (int)top.x;
			y = (int)top.y;
			
			bool hasPotential = false;
			
			for(int i=0;i<4;i++) {
				if (arr[i] == 1) hasPotential |= GoTop(x - 1, y);
				if (arr[i] == 2) hasPotential |= GoRight(x, y + 1);
				if (arr[i] == 3) hasPotential |= GoDown(x + 1, y);
				if (arr[i] == 4) hasPotential |= GoLeft(x, y - 1);
				if (hasPotential) break;
			}
			
			if (hasPotential == false) stack.Pop();
		}
	}
	
	bool GoTop(int x, int y) {
		if (x < 0 || x >= r || y < 0 || y >= c) return false;
		if (puzzleMap[x, y] == 0) return false;
		
		for(int i=0;i<5;i++) {
			int _x = x + topX[i];
			int _y = y + topY[i];
			if (_x < 0 || _x >= r || _y < 0 || _y >= c) continue;
			if (puzzleMap[_x, _y] == 0) return false;
		}
		
		puzzleMap[x, y] = 0;
		stack.Push(new Vector2(x, y));
		return true;
	}
	
	bool GoRight(int x, int y) {
		if (x < 0 || x >= r || y < 0 || y >= c) return false;
		if (puzzleMap[x, y] == 0) return false;
		
		for(int i=0;i<5;i++) {
			int _x = x + rightX[i];
			int _y = y + rightY[i];
			if (_x < 0 || _x >= r || _y < 0 || _y >= c) continue;
			if (puzzleMap[_x, _y] == 0) return false;
		}
		
		puzzleMap[x, y] = 0;
		stack.Push(new Vector2(x, y));
		return true;
	}
	
	bool GoDown(int x, int y) {
		if (x < 0 || x >= r || y < 0 || y >= c) return false;
		if (puzzleMap[x, y] == 0) return false;
		
		for(int i=0;i<5;i++) {
			int _x = x + downX[i];
			int _y = y + downY[i];
			if (_x < 0 || _x >= r || _y < 0 || _y >= c) continue;
			if (puzzleMap[_x, _y] == 0) return false;
		}
		
		puzzleMap[x, y] = 0;
		stack.Push(new Vector2(x, y));
		return true;
	}
	
	bool GoLeft(int x, int y) {
		if (x < 0 || x >= r || y < 0 || y >= c) return false;
		if (puzzleMap[x, y] == 0) return false;
		
		for(int i=0;i<5;i++) {
			int _x = x + leftX[i];
			int _y = y + leftY[i];
			if (_x < 0 || _x >= r || _y < 0 || _y >= c) continue;
			if (puzzleMap[_x, _y] == 0) return false;
		}
		
		puzzleMap[x, y] = 0;
		stack.Push(new Vector2(x, y));
		return true;
	}
	
	bool CheckEmpty(int i, int j) {
		if (i >= r || j >= c || i < 0 || j < 0) return false;
		
		return puzzleMap[i, j] == 0;
	}
	
	void VisualizeMap() {
		for(int i=0;i<r;i++) {
			for(int j=0;j<c;j++) {
				if (puzzleMap[i, j] == 1) {
					GameObject wall = Instantiate(frontPrefab) as GameObject;
					wall.transform.parent = puzzleMapObject.transform;
					wall.transform.localPosition = new Vector3(i * 5, j * 5);
					
					GameObject shadow = Instantiate(shadowPrefab) as GameObject;
					shadow.transform.parent = puzzleMapObject.transform;
					shadow.transform.localPosition = new Vector3(i * 5 + 1f, j * 5 - 1f);
				} else {
					GameObject background = Instantiate(backgroundPrefab) as GameObject;
					background.transform.parent = puzzleMapObject.transform;
					background.transform.localPosition = new Vector3(i * 5, j * 5);
				}
			}
		}
		
		for(int i=-1;i<=r;i++) {
			GameObject wall = Instantiate(frontPrefab) as GameObject;
			wall.transform.parent = puzzleMapObject.transform;
			wall.transform.localPosition = new Vector3(i * 5, -5);
			
			GameObject shadow = Instantiate(shadowPrefab) as GameObject;
			shadow.transform.parent = puzzleMapObject.transform;
			shadow.transform.localPosition = new Vector3(i * 5 + 1f, -5 - 1f);
		}
		for(int i=-1;i<=r;i++) {
			GameObject wall = Instantiate(frontPrefab) as GameObject;
			wall.transform.parent = puzzleMapObject.transform;
			wall.transform.localPosition = new Vector3(i * 5, c * 5);
			
			GameObject shadow = Instantiate(shadowPrefab) as GameObject;
			shadow.transform.parent = puzzleMapObject.transform;
			shadow.transform.localPosition = new Vector3(i * 5 + 1f, c * 5 - 1f);
		}
		for(int i=0;i<c;i++) {
			GameObject wall = Instantiate(frontPrefab) as GameObject;
			wall.transform.parent = puzzleMapObject.transform;
			wall.transform.localPosition = new Vector3(-5, i * 5);
			
			GameObject shadow = Instantiate(shadowPrefab) as GameObject;
			shadow.transform.parent = puzzleMapObject.transform;
			shadow.transform.localPosition = new Vector3(-5 + 1f, i * 5 - 1f);
		}
		for(int i=0;i<c;i++) {
			GameObject wall = Instantiate(frontPrefab) as GameObject;
			wall.transform.parent = puzzleMapObject.transform;
			wall.transform.localPosition = new Vector3(r * 5, i * 5);
			
			GameObject shadow = Instantiate(shadowPrefab) as GameObject;
			shadow.transform.parent = puzzleMapObject.transform;
			shadow.transform.localPosition = new Vector3(r * 5 + 1f, i * 5 - 1f);
		}
	}
	
	void SpawnPlayer() {
		for(int i=0;i<r;i++) {
			if (puzzleMap[i, c-1] == 0) {
				players[0].transform.position = new Vector3(i * 5, (c - 1) * 5);
				break;
			}
		}
		
		if (players.Length == 1) return;
		for(int i=r-1;i>=0;i--) {
			if (puzzleMap[i, c-1] == 0) {
				players[1].transform.position = new Vector3(i * 5, (c - 1) * 5);
				break;
			}
		}
		
		if (players.Length == 2) return;
		for(int i=0;i<r;i++) {
			if (puzzleMap[i, 0] == 0) {
				players[2].transform.position = new Vector3(i * 5, 0);
				break;
			}
		}
		
		if (players.Length == 3) return;
		for(int i=r-1;i>=0;i--) {
			if (puzzleMap[i, 0] == 0) {
				players[3].transform.position = new Vector3(i * 5, 0);
				break;
			}
		}
	}
	
	public void PlaceBoxBonus() {
		for(int i=0;i<100;i++) {
			int R = Random.Range(0, r);
			int C = Random.Range(0, c);
			if (puzzleMap[R, C] == 0) {
				GameObject _box = Instantiate(box, new Vector3(R * 5, C * 5), Quaternion.identity) as GameObject;
				_box.GetComponent<BoxBonus>().SetPosition(R, C);
				puzzleMap[R, C] = 2;
				break;
			}
		}
	}
	
	public void FreeBoxBonus(int R, int C) {
		puzzleMap[R, C] = 0;
	}
	
	public int[,] GetMap() {
		return puzzleMap;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
