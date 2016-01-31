using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public GameObject destToken;
	public int speed;



	private Vector3 newDest;
	private Vector3 noDest = new Vector3(9999, 9999, 0);
	private Queue<Vector3> moveQueue;
	private Renderer floorRend;

	private float minX;
	private float maxX;
	private float minY;
	private float maxY;


	// Use this for initialization
	void Awake () {
		moveQueue = new Queue<Vector3> ();
		newDest = noDest;
	}
	
	// Update is called once per frame
	void Update () {

		// Get new dest if needed
		if (newDest == noDest) {
			if (moveQueue != null && moveQueue.Count > 0) {
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
		destToken.transform.position =  new Vector3(mousePos.x, mousePos.y, 1);
	}

	void NewDest(Vector2 mousePos){
		mousePos = ClampMousePos (mousePos);
		moveQueue = new Queue<Vector3> ();
		newDest = mousePos;
		destToken.transform.position =  newDest;
	}

	private Vector3 ClampMousePos(Vector2 mousePos){
		//Debug.Log ("Destination " + mousePos.ToString() + " recieved by " + this.ToString());
		mousePos.x = Mathf.Clamp (mousePos.x, minX, maxX);
		mousePos.y = Mathf.Clamp (mousePos.y, minY, maxY);
		return mousePos;
	}

	void PassFloor(Renderer floor){
		minX = floor.bounds.min.x;
		maxX = floor.bounds.max.x;
		minY = floor.bounds.min.y;
		maxY = floor.bounds.max.y;
	}

	void OnCollisionEnter (Collision other){
		print (other.gameObject.tag);
		if (other.gameObject.CompareTag ("Potion")) {
			print (" I'm here");
			SendMessageUpwards ("HealTurtle", 1);
		}
	}
}
