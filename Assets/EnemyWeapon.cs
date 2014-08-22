using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {
	
	public GameObject opponent;
	private Mob enemy;

	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.name == "Enemy"){
			enemy = GetComponent<Mob>();
			if(enemy.mobState == Mob.state.attack){
				collision.gameObject.SendMessage("playerOnHit", 10);
			}
		}
	}
}
