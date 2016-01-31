using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject mouse;
	public GameObject turtle;
	public GameObject selector;
	public GameObject floor;


	public GameObject turtleHeartOrigin;
	public GameObject mouseHeartOrigin;
	public GameObject monsterHeartOrigin;

	public int turtleMaxHealth;
	public int mouseMaxHealth;
	public int monsterMaxHealth;

	private int turtleHealth;
	private int mouseHealth;
	private int monsterHealth;
	private GameObject[] turtleHearts;
	private GameObject[] mouseHearts;
	private GameObject[] monsterHearts;

	private GameObject selected;

	// Use this for initialization
	void Awake () {
		Renderer floorRend = floor.GetComponent<Renderer> ();
		mouse.SendMessage ("PassFloor", floorRend);
		turtle.SendMessage ("PassFloor", floorRend);

		SetHearts ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.D)) {
			DamageToTurtle (2);
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			HealTurtle (1);
		}

		if (Input.GetKeyDown ("1") && selected != mouse) {
			Debug.logger.Log ("1 pressed");
			Select (mouse);
		} else if (Input.GetKeyDown ("2") && selected != turtle) {
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
		if (selected != null) {
			selected.SendMessage ("NewDest", mousePos);
		}
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
			else if (hitObject.CompareTag ("Floor") & selected != null)
				selected.SendMessage("AddDest", mousePos);

		// Clicked on Nothing
		} else if (selected != null) {
			selected.SendMessage("AddDest", mousePos);

		}
	}


	private void SetHearts(){
		turtleHearts = new GameObject[turtleMaxHealth];
		for (int i = 0; i < turtleMaxHealth; i++) {
			Vector3 heartPos = turtleHeartOrigin.transform.position;
			heartPos.x += i;
			turtleHearts [i] = Instantiate (turtleHeartOrigin, heartPos, Quaternion.identity) as GameObject;
		}
		turtleHealth = turtleMaxHealth;	

		mouseHearts = new GameObject[mouseMaxHealth];
		for (int i = 0; i < mouseMaxHealth; i++) {
			Vector3 heartPos = mouseHeartOrigin.transform.position;
			heartPos.x += i;
			mouseHearts [i] = Instantiate (mouseHeartOrigin, heartPos, Quaternion.identity) as GameObject;
		}
		monsterHealth = monsterMaxHealth;	

		monsterHearts = new GameObject[monsterMaxHealth];
		for (int i = 0; i < monsterMaxHealth; i++) {
			Vector3 heartPos = monsterHeartOrigin.transform.position;
			heartPos.x += i;
			monsterHearts [i] = Instantiate (monsterHeartOrigin, heartPos, Quaternion.identity) as GameObject;
		}
		monsterHealth = monsterMaxHealth;	
	}

	void HealTurtle(int heal){
		if (turtleHealth < turtleMaxHealth) {
			SpriteRenderer renderer = turtleHearts [turtleHealth].GetComponent<SpriteRenderer> (); 
			renderer.color = Color.green;
			turtleHealth++;
			print (turtleHealth);
		}
	}

	void DamageToTurtle(int damage){
		SpriteRenderer renderer = turtleHearts[turtleHealth-1].GetComponent<SpriteRenderer> (); 
		renderer.color = Color.black;
		turtleHealth--;
		print (turtleHealth);
	}

	void DamageToBoss(int damage) {
		SpriteRenderer renderer = monsterHearts[monsterHealth-1].GetComponent<SpriteRenderer> (); 
		renderer.color = Color.black;
		monsterHealth--;
	}

    void playSound(int soundNumber)
    {
        SendMessage("playSoundAtAudioManager", soundNumber);
    }
}
