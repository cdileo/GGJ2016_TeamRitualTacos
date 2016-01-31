using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryController2 : MonoBehaviour
{
    GameObject levelMan;

    public Text textBox;
    private string[] story = { "Turtle: \"We did it Mouse! I knew we could do it if we worked together! Next time we get close, you should go first, and we'll do a combined attack.\"",
        "Mouse: \"I'll have to watch out more for attacks. Good thing we have a minute to recover. ... I'm glad you asked for my help. Maybe we can talk before we get to Star Platypus?\"",
        "Turtle: \"...\"",
        "Mouse: \"Uh, you know, about how to best rescue Star Platypus. Ha ha... ha...\"",
        "Turtle: \"Huh, what was that? Sorry, do you hear snapping?\""};
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
            (levelMan.GetComponent<LevelManagerScript>()).nextLevel();
        }
    }
}
