using UnityEngine;
using System.Collections;

public class bossScript : MonoBehaviour {

    public GameObject turtle;
    public GameObject mouse;
    public Snail snail1;
    public Crab crab1;
    public Octopus octo1;
    public float nearThreshold = 3f;
    public Animator myAnimator;

    //need to be set in scene
    public int level;
    public float attackInterval;


    private float lastAttackTime;
    private bool playerNear;
    private int stage;

    // Use this for initialization
    void Start () {
        stage = 0;
        InvokeRepeating("logic", 2f, attackInterval);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private bool checkNear()
    {
        return Vector2.Distance(turtle.transform.position, transform.position) < nearThreshold
            && Vector2.Distance(mouse.transform.position, transform.position) < nearThreshold;
    }

    private void logic()
    {
        playerNear = checkNear();
        print(playerNear);
        level = 2; //TODO: come back to me
        switch (level)
        {
            case 1: //snail
                if (playerNear)
                    snail1.NearMechanics(stage, turtle.transform.position, mouse.transform.position);
                else
                    snail1.FarMechanics(stage, turtle.transform.position, mouse.transform.position);
                break;

            case 2: //crab
                if (playerNear)
                    crab1.NearMechanics(stage, turtle.transform.position, mouse.transform.position);
                
                else
                    crab1.FarMechanics(stage, turtle.transform.position, mouse.transform.position);
                myAnimator.SetTrigger("Attack");
                break;

            case 3: //octopus
                octo1.Mechanics(stage, turtle.transform.position, mouse.transform.position);
                break;

            default:
                break;
        }

        stage = (stage + 1) % 6;
        print("Leaving logic");
    }
}


