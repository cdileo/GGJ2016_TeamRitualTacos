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
    public bool isMoving = true;
    public bool hasMovedSinceLast = true;
    public GameObject indicatorPrefab;
    public GameObject indicatorHolder;
    public float sheildChargeTime = .5f;
    public float shieldLifetime = 2;

    private GameObject activeIndicator;
    private float lastAttack;
    private float lastMove;
    private float lastSpecial;

    void Start () {
        lastAttack = Time.time;
        lastMove = Time.time;
        lastSpecial = Time.time;
    }

    void Update()
    {
        isMoving = pt.getIsMoving();
        if (!isMoving)
        {
            if (canSpecial)
            {
                triggerSpecial(pt.getPartnerDirection());
            } else if (canAttack)
            {
                attack();
            }
        } else
        {
            hasMovedSinceLast = true;
        }

        canSpecial = pt.canAttack() && pt.partnerTracker.canAttack() &&
                    pt.getPartnerDirection() != -1 && 
                    hasMovedSinceLast;
        canAttack = pt.canAttack() && hasMovedSinceLast;
    }

    private void attack()
    {
        print("Attacking....");
        hasMovedSinceLast = false;
        canSpecial = false;
    }

    private void triggerSpecial(int state)
    {
        if (state == -1)
        {
            print("WTF did you do? We shouldn't be hitting this condition....");
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
        canAttack = false;
        hasMovedSinceLast = false;
    }
}
