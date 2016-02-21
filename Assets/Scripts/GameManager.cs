using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject mouseNavigator;
	public GameObject turtleNavigator;
	public GameObject selector;
	public GameObject floor;
	public GameObject heartPrefab;
	public GameObject heartContainer;

	private HealthBar monsterHeartBar;
	private HealthBar mouseHeartBar;
	private HealthBar turtleHeartBar;

	GameObject levelMan;

	public int turtleMaxHealth;
	public int mouseMaxHealth;
    public int monsterMaxHealth; //set per level

    private int turtleDefense = 0;
    private int mouseDefense = 0;
    private int bossDefense = 0;


	private GameObject selected;

	// Use this for initialization
	void Awake () {
		Renderer floorRend = floor.GetComponent<Renderer> ();
		mouseNavigator.SendMessage ("PassFloor", floorRend);
		turtleNavigator.SendMessage ("PassFloor", floorRend);

		SetHearts ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.D)) {
			DamageToBoss (2);
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			HealBoss (2);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			DamageToMouse (2);
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			HealMouse (2);
		}

		if (Input.GetKeyDown ("1") && selected != mouseNavigator) {
			Debug.logger.Log ("1 pressed");
			Select (mouseNavigator);
		} else if (Input.GetKeyDown ("2") && selected != turtleNavigator) {
			Debug.logger.Log ("2 pressed");
			Select (turtleNavigator);
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
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

		// Clicked on object
		if (hit.collider != null) {
			GameObject hitObject = hit.collider.gameObject;
			Debug.Log ("Target Position: " + hitObject.transform.position);
			Debug.Log ("Target hit: " + hitObject.name);
			if (hitObject.CompareTag ("Player"))
				Select (hitObject);
			else if (hitObject.CompareTag ("Floor") & selected != null)
				selected.SendMessage("AddDest", mousePos);

		 //Clicked on Nothing
		} else if (selected != null) {
			Debug.Log("Mouse clicked on nothing at: " + mousePos.x + ", " + mousePos.y);
			selected.SendMessage("AddDest", mousePos);
		}
	}

    
	void HealTurtle(int heal){
		turtleHeartBar.Heal (heal);
	}

	void DamageToTurtle(int damage){
        if (damage - turtleDefense > 0)
			turtleHeartBar.Damage(damage - turtleDefense);
    }

	void HealMouse(int heal){
		mouseHeartBar.Heal (heal);
	}

	void DamageToMouse(int damage){
        if(damage - mouseDefense > 0)
			mouseHeartBar.Damage (damage - mouseDefense);
	}

	void DamageToBoss(int damage) {
		print ("damage = " + damage + " Defense: " + bossDefense);
        if (damage - bossDefense > 0)
			monsterHeartBar.Damage(damage - bossDefense);
    }


    void BossDefense(int i)
    {
        bossDefense = i;
    }

    void TurtleDefense(int i)
    {
        turtleDefense = i;
    }

    void MouseDefense(int i)
    {
        mouseDefense = i;
    }

    void HealBoss (int heal){
		monsterHeartBar.Heal (heal);
	}

	private void SetHearts(){
		float heartX = heartContainer.transform.position.x;
		float heartY = heartContainer.transform.position.y;

		turtleHeartBar = new HealthBar (Color.green, turtleMaxHealth, new Vector2(heartX, heartY), this);
		mouseHeartBar = new HealthBar (Color.blue, mouseMaxHealth, new Vector2(heartX + turtleMaxHealth, heartY), this);
		monsterHeartBar = new HealthBar (Color.red, monsterMaxHealth, new Vector2(heartX, heartY - 1), this);
	}

	public void Win(){
		print ("You win!");
		SpriteRenderer cookedCrab = GameObject.Find ("Boss").GetComponent<SpriteRenderer> (); // Bad form but fun for now
		cookedCrab.color = Color.red;

		levelMan = GameObject.Find("LevelManager");
		(levelMan.GetComponent<LevelManagerScript>()).nextLevel();
	}

	public void Lose(){
		print ("You Lose!");
		levelMan = GameObject.Find("LevelManager");
		(levelMan.GetComponent<LevelManagerScript>()).loadDeath();
	}

	public void Dead(){
		if (monsterHeartBar.getHealth() == 0) {
			Win ();
		} else {
			Lose ();
		}
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
				}
			}
		
		}
	}
	

    void playSound(int soundNumber)
    {
		SendMessage("playSoundAtAudioManager", soundNumber, SendMessageOptions.DontRequireReceiver);
    }
}
