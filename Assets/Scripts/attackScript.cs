using UnityEngine;
using System.Collections;
using System;

public class attackScript : MonoBehaviour {

    public int positionState;
    public positionTracker pt;
    public float attackCoolDown = 1f;
    public float moveCoolDown = 7f;
    public float specialChargeTime = 1f;
    public bool canAttack = false;
    public bool canMove = false;
    public bool canSpecial = false;
    public bool isSpecialing = false;
    public bool isAttacking = false;
    public bool isMoving = false;
    public GameObject indicatorPrefab;
    public GameObject indicatorHolder;
    public float sheildChargeTime = .5f;
    public float shieldLifetime = 2;

    private GameObject activeIndicator;
    private float lastAttack;
    private float lastMove;
    private float lastSpecial;

    // Use this for initialization
    void Start () {
        lastAttack = Time.time;
        lastMove = Time.time;
        lastSpecial = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            triggerSpecial(pt.getPartnerDirection());
        }
        canSpecial = Time.time - lastMove > moveCoolDown;
        lastMove = pt.lastMove;
    }

    private void triggerSpecial(int state)
    {
        if (state == -1)
            return;
        else if (!canSpecial)
        {
            return;
        }
        // init this special 
        if (state == 4)
        {
            startShield();
        }
        // set special cooldown
    }

    private void startShield()
    {
        activeIndicator = Instantiate(  indicatorPrefab, 
                                        indicatorHolder.transform.position,
                                        indicatorHolder.transform.rotation) as GameObject;
        activeIndicator.transform.parent = this.transform;
        Destroy(activeIndicator, shieldLifetime);
        canSpecial = false;
    }
}
