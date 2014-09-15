using UnityEngine;
using System.Collections;

public class Apple : Item {
	
	public float heal = 40;
	
	public Apple(){

	}


	public override void performAction(){
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		gameObject.SendMessage("playerOnHeal", heal);
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.name == "Player"){
			Debug.Log("Pick up an apple");
			GameObject inventory = GameObject.FindGameObjectWithTag ("Inventory");
			inventory.SendMessage ("addItem", gameObject);
			Destroy (gameObject);
		}

	}
}
