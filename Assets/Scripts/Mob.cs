using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

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

	public AnimationClip Run;
	public AnimationClip Idle;
	public AnimationClip Attack;

	// Use this for initialization
	void Start () {
		startcolor = body.color;
		mouseOver = false;
		hp = 100f;
		opponent = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(!mouseOver){
			OnMouseNotOver();
		}
		else{
			mouseOver = false;
		}

		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (inAttackRange()){
			Debug.Log("In attack range");
			attack ();
		}
		else if (inAlarmRange ()){
			chase ();
		}
		else{
			animation.CrossFade(Idle.name);
		}
	}

	public bool inAttackRange(){
		return Vector3.Distance (transform.position, playerTransform.position) < attackRange;
	}

	bool inAlarmRange(){
		return Vector3.Distance (transform.position, playerTransform.position) < alarmRange;
	}

	void attack(){
		animation.CrossFade(Attack.name);
		Debug.Log("Enemy attacking");
	}


	void chase(){
		Debug.Log("Enemy chasing");
		transform.LookAt (playerTransform.position);
		controller.SimpleMove (transform.forward * speed);
		animation.CrossFade(Run.name);
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


