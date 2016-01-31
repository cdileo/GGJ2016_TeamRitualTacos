using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public GameObject destToken;
	public GameObject floor;
	public int speed;

	private Vector3 newDest;
	private Vector3 noDest = new Vector3(9999, 9999, 0);
	private Queue<Vector3> moveQueue;
	private Renderer floorRend;

	float minX;
	float maxX;
	float minY;
	float maxY;

	// Use this for initialization
	void Start () {
		moveQueue = new Queue<Vector3> ();
		newDest = noDest;
		floorRend = floor.GetComponent<Renderer> ();
		minX = floorRend.bounds.min.x;
		maxX = floorRend.bounds.max.x;
		minY = floorRend.bounds.min.y;
		maxY = floorRend.bounds.max.y;
	}
	
	// Update is called once per frame
	void Update () {

		// Get new dest if needed
		if (newDest == noDest) {
			if (moveQueue.Count > 0) {
				newDest = moveQueue.Dequeue ();
			}
		}
			
		if (newDest != noDest) {
			transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), newDest, 3 * Time.deltaTime);	
			if (transform.position.x == newDest.x & transform.position.y == newDest.y) {
				newDest = noDest;
				if (moveQueue.Count == 0)
					destToken.transform.position = noDest;
			}
			
		}	
	}

	private void MovePlayer(Vector3 newDest) {

	}

	void AddDest(Vector2 mousePos){
		mousePos = ClampMousePos (mousePos);
		moveQueue.Enqueue (mousePos);
		destToken.transform.position =  new Vector3(mousePos.x, mousePos.y, 0);
	}

	void NewDest(Vector2 mousePos){
		mousePos = ClampMousePos (mousePos);
		moveQueue = new Queue<Vector3> ();
		newDest = mousePos;
		destToken.transform.position =  newDest;
	}

	private Vector3 ClampMousePos(Vector2 mousePos){
		Debug.Log ("Destination " + mousePos.ToString() + " recieved by " + this.ToString());
		mousePos.x = Mathf.Clamp (mousePos.x, minX, maxX);
		mousePos.y = Mathf.Clamp (mousePos.y, minY, maxY);
		return mousePos;
	}
}
