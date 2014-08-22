using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public AnimationClip Attack;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			Debug.Log("attack");
			animation.Play(Attack.name);
		}
	}
}
