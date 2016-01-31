using UnityEngine;
using System.Collections;

public class bossScript : MonoBehaviour {

    public GameObject turtle;
    public GameObject rabbit;
    public Snail snail1;
    public float attackInterval;
    public float nearThreshold = 3f;
    public int level;
    

    private float lastAttackTime;
    private bool playerNear;
    private int stage;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        playerNear = checkNear();

        switch (level)
        {
            case 1:
                if (playerNear)
                    snail1.NearMechanics(stage, turtle.transform.position, rabbit.transform.position);
                else
                    snail1.FarMechanics(stage, turtle.transform.position, rabbit.transform.position);
                break;



            default:
                break;
        }

       
        stage = (stage + 1) % 6;
	}

    private bool checkNear()
    {
        return Vector2.Distance(turtle.transform.position, transform.position) < nearThreshold
            && Vector2.Distance(rabbit.transform.position, transform.position) < nearThreshold;
    }
}
