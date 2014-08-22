using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

	public float weaponDamage;	
	public playerController player;

	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.tag == "Enemy"){
			if(player.playerState == playerController.state.attack){
				collision.gameObject.SendMessage("MobOnHit", weaponDamage);
			}
		}
	}
}