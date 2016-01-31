using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryController : MonoBehaviour {


	public Text textBox;
	private string[] story = { "line 1", "line 2" };
	private int position;

	// Use this for initialization
	void Start () {
		position = 0;
		textBox.text = story [0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewMessage(){
		if (position < story.Length - 1) {
			textBox.text = story [++position];
		} else {
			// TODO
		}
	}
}
