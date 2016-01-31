using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    GameObject levelMan;

    public Text textBox;
    private string[] story = { "Credits:\n   Art: James Choi\n   Programming: Chris Dileo, Roxanne Taylor, Michelle Findlay-Olynyk\n   Narrative & Mechanics Design: Michelle Findlay-Olynyk\n   Sound: Evan Witt"};
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
