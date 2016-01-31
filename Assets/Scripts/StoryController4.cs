using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryController4 : MonoBehaviour
{


    public Text textBox;
    private string[] story = { "Turtle: \"Star Platypus! Star Platypus! Where are you?!\"",
            "Mouse: \"They're over here! Are you okay?\"",
            "Star Platypus: \"The ritual... was interrupted? .. I still feel strange...\"",
            "Turtle: \"Yes yes, we did it! I'm so glad we found you in time!\"",
            "Mouse: (Sigh) \"We should get back.\"",
            "Star Platypus: \"Thank you for the resuce, both of you. The ocean creatures still need our help, but we will return home for now.\"",
            "Turtle: \"Yay!\""};
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
            // TODO: move to next scene
        }
    }
}
