using UnityEngine;
using System.Collections;

public class bossScript : MonoBehaviour {

    public GameObject turtle;
    public GameObject rabbit;
    public float attackInterval;
    public float nearThreshold = 3f;

    private float lastAttackTime;
    private bool playerNear;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        playerNear = checkNear();
	}

    private bool checkNear()
    {
        return Vector2.Distance(turtle.transform.position, transform.position) < nearThreshold
            && Vector2.Distance(rabbit.transform.position, transform.position) < nearThreshold;
    }
}
