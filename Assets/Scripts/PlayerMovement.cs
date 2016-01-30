using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public GameObject destToken;

	private Vector2 newDest;
	private Vector2 noDest = new Vector2(9999, 9999);
	private Queue<Vector2> moveQueue;

	// Use this for initialization
	void Start () {
		moveQueue = new Queue<Vector2> ();
		newDest = noDest;
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
					destToken.transform.position =  new Vector3(noDest.x, noDest.y, 0);
			}
			
		}	
	}

	void AddDest(Vector2 mousePos){
		Debug.Log ("Destination " + mousePos.ToString() + " recieved by " + this.ToString());
		moveQueue.Enqueue (mousePos);
		destToken.transform.position =  new Vector3(mousePos.x, mousePos.y, 0);
	}

	void NewDest(Vector2 mousePos){
		Debug.Log ("Destination " + mousePos.ToString() + " recieved by " + this.ToString());
		moveQueue = new Queue<Vector2> ();
		newDest = mousePos;
		destToken.transform.position =  new Vector3(newDest.x, newDest.y, 0);
	}
}
