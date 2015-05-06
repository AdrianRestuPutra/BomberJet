using UnityEngine;
using System.Collections;

public class OnlineMazeStringGenerator {

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
	
	private bool mapFinish = false;
	
	public void GeneratedMap(int _r, int _c) {
		r = _r;
		c = _c;
		
		InitializeMap();
		StartGenerating();
		mapFinish = true;
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
}
