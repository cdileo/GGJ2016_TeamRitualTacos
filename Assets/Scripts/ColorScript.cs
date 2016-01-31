using UnityEngine;
using System.Collections;

public class ColorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer thisRenderer = GetComponent<SpriteRenderer> ();
		foreach (SpriteRenderer renderer in this.GetComponentsInChildren<SpriteRenderer>()) {
			renderer.color = thisRenderer.color;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
