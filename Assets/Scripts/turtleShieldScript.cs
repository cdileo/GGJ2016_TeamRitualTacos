using UnityEngine;
using System.Collections;

public class turtleShieldScript : MonoBehaviour {

    public float shieldLifetime = 3f;

    private float bornTime;

    // we only exist to track damage? and cease existing on timeout
    void Start()
    {
        DestroyObject(this, shieldLifetime);
    }
}
