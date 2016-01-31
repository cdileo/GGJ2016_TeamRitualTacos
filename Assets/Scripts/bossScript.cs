using UnityEngine;
using System.Collections;

public class bossScript : MonoBehaviour {

    public GameObject turtle;
    public GameObject rabbit;
    public Snail snail1;
    public Crab crab1;
    public Octopus octo1;
    public float nearThreshold = 3f;

    //need to be set in scene
    public int level;
    public float attackInterval;


    private float lastAttackTime;
    private bool playerNear;
    private int stage;

    // Use this for initialization
    void Start () {
        stage = 0;

        InvokeRepeating("logic", 0, attackInterval);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private bool checkNear()
    {
        return Vector2.Distance(turtle.transform.position, transform.position) < nearThreshold
            && Vector2.Distance(rabbit.transform.position, transform.position) < nearThreshold;
    }

    private void logic()
    {
        playerNear = checkNear();

        switch (level)
        {
            case 1: //snail
                if (playerNear)
                    snail1.NearMechanics(stage, turtle.transform.position, rabbit.transform.position);
                else
                    snail1.FarMechanics(stage, turtle.transform.position, rabbit.transform.position);
                break;

            case 2: //crab
                if (playerNear)
                    crab1.NearMechanics(stage, turtle.transform.position, rabbit.transform.position);
                else
                    crab1.FarMechanics(stage, turtle.transform.position, rabbit.transform.position);
                break;

            case 3: //octopus
                octopus1.Mechanics(stage, turtle.transform.position, rabbit.transform.position);
                break;

            default:
                break;
        }

        stage = (stage + 1) % 6;

    }
}


