using UnityEngine;
using System.Collections;

public class Crab : MonoBehaviour
{

    public int defense = 0;

    private int maxHealth = 8;
    private float nearThreshold = 3f;
    private float midThreshold = 2f; //check this value


    public void NearMechanics(int stage, Vector3 tPos, Vector3 mPos)
    {
        switch (stage)
        {
            case 0:
                ClawSnap (tPos, mPos);
                break;

            case 1:
                ClawSnap(tPos, mPos);
                break;

            case 2:
                Hide();
                break;

            case 3:
                ClawSnap(tPos, mPos);
                break;

            case 4:
                ClawSnap(tPos, mPos);
                break;

            case 5:
                Hide();
                break;

            default:
                Hide();
                break;
        }


    }


    public void FarMechanics(int stage, Vector3 tPos, Vector3 mPos)
    {
        switch (stage)
        {
            case 0:
                //do nothing for now
                //ranged if time

                break;

            case 1:
                Hide();

                break;

            default:
                //do nothing for now
                break;
        }


    }


    public void ClawSnap(Vector3 tPos, Vector3 mPos)
    {
        SendMessageUpwards("BossDefense", 0);
        //query characters to see if either get hit
        //if so, get 2 damage
        if (Vector2.Distance(tPos, transform.position) < nearThreshold)
            SendMessageUpwards("DamageToTurtle", 2);
        if (Vector2.Distance(mPos, transform.position) < nearThreshold)
            SendMessageUpwards("DamageToMouse", 2);

        //animate and sounds?

    }



    public void Hide()
    {
        SendMessageUpwards("BossDefense", 2);
        //animate?
        //sound effect?


    }


}