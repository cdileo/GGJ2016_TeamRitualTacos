using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerMovement : MonoBehaviour {

	public GameObject destToken;
	public int speed;

	// Movement
	private Vector2 newDest;
    private Vector2 noDest = new Vector2(999, 999);
	private Queue<Vector2> moveQueue;
	private Renderer floorRend;

	// Values sent from Floor. Will clamp players movement.
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;


	// Use this for initialization
	void Awake () {
		moveQueue = new Queue<Vector2> ();
		speed = speed == 0 ? 5 : speed;
        newDest = noDest;
	}
	
	// Update is called once per frame
	void Update () {

		// Get new dest if needed
		if (VectorsEqual(newDest, noDest)) {
			if (moveQueue != null && moveQueue.Count > 0) {
				newDest = moveQueue.Dequeue ();
			}

		} else  {
			transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), newDest, 3 * Time.deltaTime);
            
			if (VectorsEqual(transform.position, newDest)) {
                newDest = noDest;
                if (moveQueue.Count == 0)
                    destToken.transform.position = noDest;
			}
			
		}	
	}

    private bool VectorsEqual(Vector2 target, Vector2 comparison)
    {
        // Bug fix: Don't use '=' operator for float. Too finiky, use Mathf.Approx instead.
        return Mathf.Approximately(target.x, comparison.x) && Mathf.Approximately(target.y, comparison.y);
    }

    void AddDest(Vector2 mousePos){
		mousePos = ClampMousePos (mousePos);
		moveQueue.Enqueue (mousePos);
        destToken.transform.position = mousePos;

	}

	void NewDest(Vector2 mousePos){
		mousePos = ClampMousePos (mousePos);
		moveQueue = new Queue<Vector2> ();
		newDest = mousePos;
		destToken.transform.position =  newDest;
	}

	private Vector3 ClampMousePos(Vector2 mousePos){
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
			print ("Potion: I'm here");
			SendMessageUpwards ("HealTurtle", 1);
		}
	}
}
