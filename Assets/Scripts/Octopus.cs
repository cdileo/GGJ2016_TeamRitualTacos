using UnityEngine;
using System.Collections;

public class Octopus : MonoBehaviour
{

    public int defense = 0;

    private int maxHealth = 8;
    private float nearThreshold = 3f;
    private float quarterSize = 1f; //check this value


    public void Mechanics(int stage, Vector3 tPos, Vector3 mPos)
    {
        switch (stage)
        {
            case 0:
                Tentacle(0, tPos, mPos);
                break;

            case 1:
                Tentacle(1, tPos, mPos);
                break;

            case 2:
                Tentacle(2, tPos, mPos);
                break;

            case 3:
                Tentacle(3, tPos, mPos);
                break;

            case 4:
                Splash();
                break;

            case 5:
                Hide();
                break;

            default:
                Hide();
                break;
        }


    }



    public void Tentacle(int i, Vector3 tPos, Vector3 mPos)
    {
        SendMessageUpwards("BossDefense", 0);
        //query characters to see if either get hit
        //if so, get 2 damage
        if (tPos.y > quarterSize*i && tPos.y < quarterSize*(i+1))
            SendMessageUpwards("DamageToTurtle", 2);
        if (mPos.y > quarterSize * i && mPos.y < quarterSize * (i + 1))
            SendMessageUpwards("DamageToMouse", 2);

        //animate and sounds?

    }

    public void Splash()
    {
        SendMessageUpwards("BossDefense", 0);
        //1 damage to everyone
        SendMessageUpwards("DamageToTurtle", 1);
        SendMessageUpwards("DamageToMouse", 1);

        //animate and sounds?

    }



    public void Hide()
    {
        SendMessageUpwards("BossDefense", 2);
        //animate?
        //sound effect?


    }


}