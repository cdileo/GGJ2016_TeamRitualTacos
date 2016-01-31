using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryController1 : MonoBehaviour
{
    GameObject levelMan;

    public Text textBox;
    private string[] story = { "Turtle: \"Mouse! Mouse! Star Platypus has been kidnapped by the Octpus King! We have to rescue them!\"", 
            "Mouse: \"I know you're hard to hurt Turtle, but the tunnel to the Octopus King's lair will be well defended. I can throw a knife, but if I get hit I'm done for.\"",
            "Turtle: \"C'mon, just stay behind me and I'll protect you. Just jump out to attack! Together we can defeat them and bring back the amazing Star Platypus!\"",
            "Mouse: \"... Right, amazing.... Well, together sounds good at least.\""};
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
