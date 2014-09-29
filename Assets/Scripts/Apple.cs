using UnityEngine;
using System.Collections;

public class Apple : Item{
	
	public AppleObject appleObject;
	public float value = 40;
	
	public Apple(Texture2D icon){
		image = icon;
	}

 	public override void performAction(){
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		gameObject.SendMessage("playerOnHeal", value);
		Debug.Log("item performs action");
	}
}




