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
    private bool nearBoss;

    void Start () {
        lastAttack = Time.time;
        lastMove = Time.time;
        lastSpecial = Time.time;
    }

    void Update()
    {
        isMoving = pt.getIsMoving();
        nearBoss = pt.isNearBoss();
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
        // what's a switch statement?
        if(state == 0)
        {
            startBomb();
        }
        else if (state == 1)
        {
            doNothingAtAll();
        }
        else if (state == 2)
        {
            startNearAttack1();
        }
        else if (state == 3)
        {
            startFarAttack1();
        }
        else if (state == 4)
        {
            startDefence();
        }
        else if (state == 5)
        {
            startFarAttack2();
        }
        else if (state == 6)
        {
            startNearAttack2();
        }
        else if (state == 7)
        {
            doNothingAtAll();
        }
        // set special cooldown
    }






    private void startFarAttack1()
    {
        if (!nearBoss)
            SendMessageUpwards("DamageToBoss", 1);
        print("State 3: far attack 1");
        resetAbilities();
    }
    private void startFarAttack2()
    {
        if (!nearBoss)
            SendMessageUpwards("DamageToBoss", 1);
        print("State 5: near attack 1");
        resetAbilities();
    }

    private void startNearAttack1()
    {
        if (nearBoss)
            SendMessageUpwards("DamageToBoss", 2);
        print("State 2: near attack 1");
        resetAbilities();
    }
    private void startNearAttack2()
    {
        SendMessageUpwards("DamageToBoss", 2);
        print("State 6: near attack 2");
        resetAbilities();
    }

    private void doNothingAtAll()
    {
        SendMessageUpwards("IPutOnMyRobeAndWizardHat", 1,SendMessageOptions.DontRequireReceiver);
        print("State 1 or 7: not wearing pants...");
        resetAbilities();
    }

    private void startBomb()
    {
        if (nearBoss)
            SendMessageUpwards("DamageToBoss", 1);
        print("State 0: bomb");
        resetAbilities();
    }

    private void startDefence()
    {
        activeIndicator = Instantiate(  indicatorPrefab, 
                                        indicatorHolder.transform.position,
                                        indicatorHolder.transform.rotation) as GameObject;
        activeIndicator.transform.parent = this.transform;
        Destroy(activeIndicator, shieldLifetime);
        resetAbilities();
		print ("Shield starting");
    }

    private void resetAbilities()
    {
        canSpecial = false;
        canAttack = false;
        hasMovedSinceLast = false;
    }
}
