using UnityEngine;
using System.Collections;

public class AppleObject : MonoBehaviour {
	public Texture2D icon;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.name == "Player"){
			Debug.Log("Pick up an apple");
			GameObject inventory = GameObject.FindGameObjectWithTag ("Inventory");
			inventory.SendMessage ("addItem", new Apple(icon));
			Destroy (gameObject);
		}
		
	}
}
