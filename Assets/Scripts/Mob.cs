using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {
	public enum state{idle, move, attack, die};
	public state mobState = state.idle;
	public float speed;
	public float attackRange;
	public float alarmRange;
	public float hp;
	public GameObject opponent;


	private Color startcolor;
	private bool mouseOver;

	public Material body;
	public Material head;

	public Transform playerTransform;
	public CharacterController controller;

	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip attack;
	public AnimationClip die;

	// Use this for initialization
	void Start () {
		startcolor = body.color;
		mouseOver = false;
		hp = 100f;
		opponent = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(hp <= 0){
			mobState = state.die;
		}
		else{
			if(!mouseOver){
				OnMouseNotOver();
			}
			else{
				mouseOver = false;
			}

			if (InAttackRange()){
				mobState = state.attack;
			}
			else if (InAlarmRange ()){
				mobState = state.move;
			}
			else{
				mobState = state.idle;
			}
		}

		if(mobState == state.die){
			animation.CrossFade (die.name);
			Invoke("Die", 2.0f);
		}
		else if (mobState == state.attack){
			Attack();
		}
		else if (mobState == state.move){
			Run();
		}
		else{
			Idle();
		}
	


	}

	void MobOnHit(float damage){
		hp -= damage;
		Debug.Log ("Mob health =" + hp);
	}


	public bool InAttackRange(){
		return Vector3.Distance (transform.position, playerTransform.position) < attackRange;
	}

	bool InAlarmRange(){
		return Vector3.Distance (transform.position, playerTransform.position) < alarmRange;
	}

	void Die(){
		Debug.Log("Mob dies");
		Destroy (gameObject);
	}

	void Attack(){
		animation.CrossFade(attack.name);
	}

	void Idle(){
		animation.CrossFade (idle.name);
	}

	void Run(){
		transform.LookAt (playerTransform.position);
		controller.SimpleMove (transform.forward * speed);
		animation.CrossFade(run.name);
	}

	void OnMouseOver(){
		mouseOver = true;
		body.color = Color.red;
		head.color = Color.red;
	}

	void OnMouseNotOver(){
		body.color = startcolor;
		head.color = startcolor;
	}

}


