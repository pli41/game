using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

	public float weaponDamage;	
	public playerController player;
	public GameObject opponent;

	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.tag == "Enemy"){
			opponent = collision.gameObject;
			if(player.playerState == playerController.state.attack){
				collision.gameObject.SendMessage("MobOnHit", weaponDamage);
			}
		}
	}
}