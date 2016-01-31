using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject mouse;
	public GameObject turtle;
	public GameObject selector;

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

		if (Input.GetMouseButtonDown (0)) {
			LeftMouseStuff ();
		}
		if (Input.GetMouseButtonDown (1)) {
			RightMouseStuff ();
		}
	}

	private void Select(GameObject chosen){
		selected = chosen;
		Debug.logger.Log (selected.ToString() + " selected");
		selector.transform.position = selected.transform.position;
		selector.transform.SetParent(selected.transform);
	}

	private void RightMouseStuff(){
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		selected.SendMessage("NewDest", mousePos);
	}

	private void LeftMouseStuff(){
		//Debug.logger.Log ("Mouse 0 pressed at " + Input.mousePosition.ToString());

		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

		// Clicked on object
		if (hit.collider != null) {
			GameObject hitObject = hit.collider.gameObject;
			Debug.Log ("Target Position: " + hitObject.transform.position);
			if (hitObject.CompareTag ("Player"))
				Select (hitObject);
			else if (hitObject.CompareTag ("Floor"))
				selected.SendMessage("AddDest", mousePos);

		// Clicked on Nothing
		} else if (selected != null) {
			selected.SendMessage("AddDest", mousePos);

		}
	}
}
