using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryController3 : MonoBehaviour
{
    GameObject levelMan;

    public Text textBox;
    private string[] story = { "Octopus: \"So, you scrawny miscreants made it all the way here.\"",
            "Turtle: \"Let Star Platypus go you monster! We aren't responsible for the plague, and Star Platypus was just trying to help your kingdom!\"",
            "Octopus: \"Oh, I know that! Who cares if a few die? I've found something far better: a ritual to steal Star Playpus's power so all my creatures can survive in the air. We will take over your precious land! Ha ha ha ha ha!\"",
            "Mouse: \"Oh no! We have to stop them!\""};
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
