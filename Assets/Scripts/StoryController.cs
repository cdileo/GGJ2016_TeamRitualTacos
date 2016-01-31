using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryController : MonoBehaviour {


	public Text textBox;
	private string[] story = { "When a plague flowed in to his ocean kingdom, " +  
            "the Octopus King turned his many eyes toward the land." +
            "Blaming the creatures of the forest and river for the suffering of his subjects,"
            + " he began plotting for war....",
            "The denizens of the land were worried, " + 
            "but felt the pain of the ocean creatures. " +
            "They called on the revered Star Platypus to calm the Octopus King " +
            "and offer what help the land could provide.",
            "The Octopus King had no interest in help and threw Star Platypus in prison."
            + " Rumours have come back to the land that the Octopus King is planning " +
            "to sacrifce Star Platypus for some dark purpose....." };
	private int position;

	// Use this for initialization
	void Start () {
		position = 0;
		textBox.text = story [0];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            NewMessage();
        }
    }

	public void NewMessage(){
		if (position < story.Length - 1) {
			textBox.text = story [++position];
		} else {
            //GameObject.Find("LevelManager");
		}
	}
}
