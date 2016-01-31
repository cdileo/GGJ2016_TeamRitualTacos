using UnityEngine;
using System.Collections;

public class dontDestroyMyParentScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
}
