using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {


	public float heal = 40;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Player"){
			collision.gameObject.SendMessage("playerOnHeal", heal);
			Destroy(gameObject);
		}
	}
}
