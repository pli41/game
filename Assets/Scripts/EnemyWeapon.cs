using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {

	public Mob enemy;
	public int weaponDamage;

	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.name == "Player"){
			if(enemy.mobState == Mob.state.attack){
				collision.gameObject.SendMessage("playerOnHit", weaponDamage);
			}
		}
	}
}
