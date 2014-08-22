using UnityEngine;
using System.Collections;

public class highLight : MonoBehaviour {

	private bool mouseOver;
	private Color startColor;
	public Material body;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseEnter(){

	}

	void OnMouseOver(){
		Debug.Log("Mouse is over the character");
		startColor = body.color;
		body.color = Color.green;
	}

	void OnMouseExit(){
		Debug.Log("Mouse is not over the character");
		renderer.material.color = startColor;
	}
}
