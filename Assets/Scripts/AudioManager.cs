using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    // nicely resizes in editor
    public AudioSource[] sounds;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void playSoundAtAudioManager (int soundIndex)
    {
		print ("Im here");
        sounds[soundIndex].Play();
    }
}
