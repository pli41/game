using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public enum state{idle, move, attack, die};
	public int maxHealth = 100;
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

	private float healthBarlenght;


	public AnimationClip die;
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip attack;

	// Use this for initialization
	void Start () {
		facePosition = transform.position;
		healthBarlenght = Screen.width / 2;
		idleS = true;
	}

	void OnGUI(){

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

			getFacingDirection();


			if(playerState == state.attack){
				Attack();
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
			idleS = true;
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
		playerState = state.attack;
		animation.CrossFade (attack.name);
		Debug.Log("attack");
	}

	void playerOnHit(float damage){
		hp -= damage;
		Debug.Log ("Player health =" + hp);
	}

	void getFacingDirection(){
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
		Vector3 mouseDir = mousePosition - transform.position;
		float angle = Vector3.Angle(transform.forward, mouseDir);
		if (angle > 7.0f) {
			transform.Rotate (new Vector3 (0f, angle, 0f));
		}
	}
	
}

