using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathStoryController : MonoBehaviour
{

    GameObject levelMan;

    public Text textBox;
    private string[] story = { "You lose....", "Please try again" };
    private int position;

    // Use this for initialization
    void Start()
    {
        position = 0;
        textBox.text = story[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NewMessage();
        }
    }

    public void NewMessage()
    {
        if (position < story.Length - 1)
        {
            textBox.text = story[++position];
        }
        else {
            levelMan = GameObject.Find("LevelManager");
            (levelMan.GetComponent<LevelManagerScript>()).loadSplashScreen();
        }
    }
}
