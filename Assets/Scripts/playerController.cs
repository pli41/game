using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public enum state{onHit, idle, move, attack, die};
	public float maxHealth = 100;
	public float speed;	
	public float rotationSpeed;
	public CharacterController controller;
	public float hp;
	private Vector3 facePosition;
	private Vector3 mousePosition;
	private Quaternion rotation;
	private Vector3 direction;
	private bool forward;
	private bool back;
	private bool left;
	private bool right;
	private bool idleS;
	public state playerState;
	private float onHitTimer;
	private float onHitTime = 1;

	public AnimationClip getHit;
	public AnimationClip die;
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip attack;

	public float combatEscapeTime = 7;

	public float countDown;

	// Use this for initialization
	void Start () {
		onHitTimer = onHitTime;
		facePosition = transform.position;
		idleS = true;
	}

	


	// Update is called once per frame
	void Update (){

		if(hp <= 0){
			playerState = state.die;
			idleS = false;
		}

		if(playerState == state.die){
			animation.CrossFade (die.name);
			Invoke("Die", 2.0f);
		}
		else{
			if(Input.GetKey(KeyCode.W)){
				forward = true;
				playerState = state.move;
				idleS = false;
			}
			if(Input.GetKey(KeyCode.A)){
				left = true;
				playerState = state.move;
				idleS = false;
			}
			if(Input.GetKey(KeyCode.S)){
				back = true;
				playerState = state.move;
				idleS = false;
			}
			if(Input.GetKey(KeyCode.D)){
				right = true;
				playerState = state.move;
				idleS = false;
			}
			if(Input.GetMouseButton(0)){
				playerState = state.attack;
				idleS = false;
			}
			if (idleS){
				playerState = state.idle;
			}

			AdjustFacingDirection();


			if(playerState == state.attack){
				Attack();
			}
			else if(playerState == state.onHit){
				animation.CrossFade(getHit.name);
			}
			else if(playerState == state.move){
				Move ();
			}

			else if (playerState == state.idle){
				Idle();
			}



			//Reset
			forward = false;
			back = false;
			left = false;
			right = false;
			direction = new Vector3(0, 0, 0);
			if(onHitTimer > 0 && playerState == state.onHit){
				onHitTimer -= Time.deltaTime;
			}
			else{
				idleS = true;
				onHitTimer = onHitTime;
			}

			//Debug.Log (transform.position);
		}
	}

	void Idle(){
		animation.CrossFade (idle.name);

	}

	void Die(){
		Debug.Log("You are dead");
		Destroy (gameObject);
	}


	void Move(){
		rotateToMovePosition();
		transform.position += direction * speed * Time.deltaTime;
		playerState = state.move;
		animation.CrossFade(run.name);
	}

	void Attack(){
		locateMousePosition();
		rotateToMousePosition();
		animation.CrossFade (attack.name);
		countDown = combatEscapeTime;
		InvokeRepeating ("combatEscapeCountDown", 0, 1);
		Debug.Log("attack");
	}

	void playerOnHit(float damage){
		playerState = state.onHit;
		hp -= damage;
		countDown = combatEscapeTime;
		InvokeRepeating ("combatEscapeCountDown", 0, 1);
		Debug.Log ("Player health =" + hp);
	}

	void combatEscapeCountDown(){
		countDown -= 1;
		if(countDown == 0){
			CancelInvoke("combatEscapeCountDown");
			Debug.Log("Escaped fighting");
		}
	}

	void AdjustFacingDirection(){
		//direction = new Vector3(0, 0, 0);
		if(forward){
			direction += Vector3.forward;
		}
		if(back){
			direction += Vector3.back;
		}
		if(left){
			direction += Vector3.left;
		}
		if(right){
			direction += Vector3.right;
		}
		direction = direction.normalized;
		facePosition = transform.position + direction;
	}

	//Locate where the mouse points
	void locateMousePosition(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, 1000)) {
			mousePosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			Debug.Log(mousePosition);
		}
		else{
			Debug.Log("Mouse position is not available");
		}

	}

	void rotateToMovePosition(){
		//should directly set direction by 
		Vector3 targetDir = facePosition - transform.position;
		float angle = Vector3.Angle(transform.forward,targetDir);
		transform.Rotate (new Vector3 (0f, angle, 0f));
	}

	void rotateToMousePosition(){
		//should directly set direction by 
//		Vector3 mouseDir = mousePosition - transform.position;
//		float angle = Vector3.Angle(transform.forward, mouseDir);
//		if (angle > 7.0f) {
//			transform.Rotate (new Vector3 (0f, angle, 0f));
//		}
		transform.LookAt(mousePosition);
		Debug.Log("look at");
	}
	
}

