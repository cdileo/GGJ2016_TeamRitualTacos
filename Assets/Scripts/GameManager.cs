using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject mouse;
	public GameObject turtle;
	public GameObject selector;
	public GameObject floor;
	public GameObject heartPrefab;

	public GameObject turtleHeartOrigin;
	public GameObject mouseHeartOrigin;
	public GameObject monsterHeartOrigin;

	private HealthBar monsterHeartBar;

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
			DamageToBoss (1);
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			HealBoss (1);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			DamageToMouse (1);
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			HealMouse (1);
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

	void HealMouse(int heal){
		if (mouseHealth < mouseMaxHealth) {
			SpriteRenderer renderer = mouseHearts [mouseHealth].GetComponent<SpriteRenderer> (); 
			renderer.color = Color.blue;
			mouseHealth++;
			print (mouseHealth);
		}
	}

	void DamageToMouse(int damage){
		SpriteRenderer renderer = mouseHearts[mouseHealth-1].GetComponent<SpriteRenderer> (); 
		renderer.color = Color.black;
		mouseHealth--;
		print (mouseHealth);
	}

	void DamageToBoss(int damage) {
		monsterHeartBar.Damage (damage);
	}

	void HealBoss (int heal){
		monsterHeartBar.Heal (heal);
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
		mouseHealth = monsterMaxHealth;	

		monsterHeartBar = new HealthBar (Color.red, monsterMaxHealth, monsterHeartOrigin.transform.position, this);
		monsterHeartOrigin.SetActive (false);
	}

	public void Win(){
		// TODO
		print ("You win!");
	}

	public class HealthBar {

		private Color colour;
		private int maxHealth;
		private Vector3 originPos;

		private GameObject[] heartContainer;
		private int health;
		private GameObject heart;

		private GameManager gameManager;

		public Color getColour() { return colour; }
		public Vector3 getOriginPos() { return originPos; }
		public int getMaxHealth() { return maxHealth; }
		public int getHealth() { return health; }

		public HealthBar(Color colourSet, int maxHealthSet, Vector3 originPosSet, GameManager gameManagerSet){
			colour = colourSet;
			maxHealth = maxHealthSet;
			originPos = originPosSet;
			gameManager = gameManagerSet;

			health = maxHealth;
			heartContainer = new GameObject[maxHealth];
			heart = gameManager.heartPrefab;
			SpriteRenderer heartRenderer = heart.GetComponent<SpriteRenderer>();
			heartRenderer.color = colour;


			for (int i = 0; i < maxHealth; i++) {
				Vector3 heartPos = originPos;
				heartPos.x += i;
				heartContainer [i] = Instantiate (heart, heartPos, Quaternion.identity) as GameObject;
			}

		}

		public void Damage(int damage){
			for (int i = 0; i < damage; i++ ){
				if (health > 0) {
					SpriteRenderer renderer = heartContainer [health - 1].GetComponent<SpriteRenderer> (); 
					renderer.color = Color.black;
					health--;
					if (health <= 0)
						gameManager.SendMessage ("Dead");
				} else 
					gameManager.SendMessage ("Dead");

			}		
		}

		public void Heal(int heal){
			for (int i = 0; i < heal; i++) {
				if (health < maxHealth) {
					SpriteRenderer heartRenderer = heartContainer [health].GetComponent<SpriteRenderer> (); 
					heartRenderer.color = colour;
					health++;
					print (health);
				}
			}
		
		}
	
	}
}
