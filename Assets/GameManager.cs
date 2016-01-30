using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject mouse;
	public GameObject turtle;
	public GameObject selector;
	public GameObject destination;

	private GameObject selected;

	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("1") & selected != mouse) {
			Debug.logger.Log ("1 pressed");
			Select (mouse);
		} else if (Input.GetKeyDown ("2") & selected != turtle) {
			Debug.logger.Log ("2 pressed");
			Select (turtle);
		}

		// Just to test selector folowing selected player
		if (Input.GetKey ("right") & selected != null)
			selected.transform.position = new Vector3 ((selected.transform.position.x + (float) .1), selected.transform.position.y, 0);
		if (Input.GetKey ("left") & selected != null)
			selected.transform.position = new Vector3 ((selected.transform.position.x - (float) .1), selected.transform.position.y, 0);

		if (Input.GetMouseButtonDown (0)) {
			Debug.logger.Log ("Mouse 0 pressed at " + Input.mousePosition.ToString());
			/*
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (selected != null) {
				Physics.Raycast (ray.origin, selected.transform.position, 100);
				Debug.DrawLine (ray.origin, selected.transform.position);
			}
			if (Physics.Raycast(ray, out hit, 100)) {
				Debug.DrawLine(ray.origin, hit.point);
			}
			Debug.logger.Log (ray.ToString());
			*/
			destination.transform.position = Input.mousePosition;
		}

	}

	private void Select(GameObject chosen){
		selected = chosen;
		Debug.logger.Log (selected.ToString() + " selected");
		selector.transform.position = selected.transform.position;
		selector.transform.SetParent(selected.transform);
	}
}
