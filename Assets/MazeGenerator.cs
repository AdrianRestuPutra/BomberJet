using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

	public GameObject frontPrefab;
	public GameObject backgroundPrefab;
	public GameObject shadowPrefab;
	
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
			//stack.Push(top);
			
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
				//if (hasPotential) break;
			}
			
			//if (hasPotential == false) stack.Pop();
		}
	}
	
	bool GoTop(int x, int y) {
		if (x < 0 || x >= r || y < 0 || y >= c) return false;
		
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
	
	void VisualizeMap() {
		for(int i=0;i<r;i++) {
			for(int j=0;j<c;j++) {
				if (puzzleMap[i, j] == 1) {
					GameObject wall = Instantiate(frontPrefab) as GameObject;
					wall.transform.position = new Vector3(i * 5, j * 5);
					
					GameObject shadow = Instantiate(shadowPrefab) as GameObject;
					shadow.transform.position = new Vector3(i * 5 + 2.5f, j * 5 - 2.5f);
				} else {
					GameObject background = Instantiate(backgroundPrefab) as GameObject;
					background.transform.position = new Vector3(i * 5, j * 5);
				}
			}
		}
		
		for(int i=-1;i<=r;i++) {
			GameObject wall = Instantiate(frontPrefab) as GameObject;
			wall.transform.position = new Vector3(i * 5, -5);
			
			GameObject shadow = Instantiate(shadowPrefab) as GameObject;
			shadow.transform.position = new Vector3(i * 5 + 2.5f, -5 - 2.5f);
		}
		for(int i=-1;i<=c;i++) {
			GameObject wall = Instantiate(frontPrefab) as GameObject;
			wall.transform.position = new Vector3(i * 5, r * 5);
			
			GameObject shadow = Instantiate(shadowPrefab) as GameObject;
			shadow.transform.position = new Vector3(i * 5 + 2.5f, r * 5 - 2.5f);
		}
		for(int i=0;i<r;i++) {
			GameObject wall = Instantiate(frontPrefab) as GameObject;
			wall.transform.position = new Vector3(-5, i * 5);
			
			GameObject shadow = Instantiate(shadowPrefab) as GameObject;
			shadow.transform.position = new Vector3(-5 + 2.5f, i * 5 - 2.5f);
		}
		for(int i=0;i<r;i++) {
			GameObject wall = Instantiate(frontPrefab) as GameObject;
			wall.transform.position = new Vector3(c * 5, i * 5);
			
			GameObject shadow = Instantiate(shadowPrefab) as GameObject;
			shadow.transform.position = new Vector3(c * 5 + 2.5f, i * 5 - 2.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
