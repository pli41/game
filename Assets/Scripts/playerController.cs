using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	enum state{idle, move, attack, die};
	public int maxHealth = 100;
	public float speed;	
	public float rotationSpeed;
	public CharacterController controller;
	public int hp;
	private Vector3 facePosition;
	private Vector3 mousePosition;
	private Quaternion rotation;
	private Vector3 direction;
	private bool forward;
	private bool back;
	private bool left;
	private bool right;
	private state playerState;

	private float healthBarlenght;


	public AnimationClip die;
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip attack;

	// Use this for initialization
	void Start () {
		facePosition = transform.position;
		healthBarlenght = Screen.width / 2;
	}

	void OnGUI(){

	}

	// Update is called once per frame
	void Update (){

		if(hp < 0){
			playerState = state.die;
		}

		if(playerState == state.die){
			Die ();
		}
		else{
			if(Input.GetKey(KeyCode.W)){
				forward = true;
				playerState = state.move;
			}
			if(Input.GetKey(KeyCode.A)){
				left = true;
				playerState = state.move;
			}
			if(Input.GetKey(KeyCode.S)){
				back = true;
				playerState = state.move;
			}
			if(Input.GetKey(KeyCode.D)){
				right = true;
				playerState = state.move;
			}
			if(Input.GetMouseButton(0)){
				playerState = state.attack;
			}

			getFacingDirection();


			if(playerState == state.attack){
				Attack();
			}
			else if(direction == new Vector3(0, 0, 0)){
				animation.CrossFade(idle.name);
				playerState = state.idle;
			}
			else{
				if(playerState == state.move){
					Move ();
				}
			}

			forward = false;
			back = false;
			left = false;
			right = false;
			direction = new Vector3(0, 0, 0);
			playerState = state.idle;
			//Debug.Log (transform.position);
		}
	}
	
	void Die(){
		Debug.Log("You are dead");
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

	void playerOnHit(int damage){
		hp -= damage;
		Debug.Log (hp);
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
		Debug.Log (angle);
		if (angle > 7.0f) {
			transform.Rotate (new Vector3 (0f, angle, 0f));
		}
	}
	
}

