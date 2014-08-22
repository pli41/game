using UnityEngine;
using System.Collections;

public class weapon : MonoBehaviour {

	public Mob enemy;
	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.name == "Player"){
			Debug.Log("123123123");
			if(enemy.inAttackRange()){
				collision.gameObject.SendMessage("playerOnHit", 10);
			}
		}
	}
}
