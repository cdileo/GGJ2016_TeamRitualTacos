using UnityEngine;
using System.Collections;

public class LevelManagerScript : MonoBehaviour {

    public int thisLevel = 0;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void nextLevel()
    {
        if (Application.levelCount > thisLevel) { 
            //I know it's depricated
            Application.LoadLevel(thisLevel + 1);
            thisLevel++;
        } else
        {
            loadSplashScreen();
        }
    }

    public void loadSplashScreen()
    {
        Application.LoadLevel(0);
    }

    public void loadDeath()
    {
        Application.LoadLevel(7);

    }
}
