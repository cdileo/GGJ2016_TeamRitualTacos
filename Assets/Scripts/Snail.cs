using UnityEngine;
using System.Collections;

public class Snail : MonoBehaviour {

    public int health;
    public int defense = 0;

    private int maxHealth = 10;
    private float nearThreshold = 3f;

    
	public void NearMechanics(int stage, Vector3 tPos, Vector3 mPos)
	{
        switch (stage)
        {
            case 0:
                EyeButt(tPos, mPos);
                break;

            case 1:
                Hide();
                break;

            case 2:
                EyeButt(tPos, mPos);
                break;

            default:
                EyeButt(tPos, mPos);
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


    public void EyeButt(Vector3 tPos, Vector3 mPos)
    {
        SendMessageUpwards("SnailDefense", 0);
        //query characters to see if either get hit
        //if so, get 1 damage
        if (Vector2.Distance(tPos, transform.position) < nearThreshold)
            SendMessageUpwards("DamageToTurtle", 1);
        if (Vector2.Distance(mPos, transform.position) < nearThreshold)
            SendMessageUpwards("DamageToMouse", 1);
        
    }


    public void Hide()
    {
        SendMessageUpwards("SnailDefense", 2);
        //animate?
        //sound effect?


    }


}
