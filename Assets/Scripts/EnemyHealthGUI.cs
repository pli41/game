using UnityEngine;
using System.Collections;

public class EnemyHealthGUI : MonoBehaviour {

	public PlayerWeapon playerW;
	public playerController player;

	public Texture2D healthBarFrame;
	public Rect framePosition;

	private float horizontalDistance = 0.108f;
	private float verticalDistance = 0.27f;
	private float widthPercent = 0.785f;
	private float heightPercent = 0.44f;
	public Texture2D healthBar;
	public Rect healthBarPosition;

	public Mob target;
	public float healthPercentage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerW.opponent != null){
			target = playerW.opponent.GetComponent<Mob> ();
			healthPercentage = target.hp / target.maxHealth;
		}
		else{
			target = null;
			healthPercentage = 0;
		}

	}

	void OnGUI (){
		if(playerW.opponent != null){
			drawFrame ();
			drawBar ();
		}

	}

	void drawBar(){
		healthBarPosition.x = framePosition.x + framePosition.width * horizontalDistance;
		healthBarPosition.y = framePosition.y + framePosition.height * verticalDistance;
		if(healthPercentage <0){
			healthPercentage = 0;
		}
		healthBarPosition.width = framePosition.width * widthPercent * healthPercentage;
		healthBarPosition.height = framePosition.height * heightPercent;
		GUI.DrawTexture (healthBarPosition, healthBar);
	}

	void drawFrame(){
		framePosition.x = (Screen.width - framePosition.width) / 2;
		framePosition.y = 15;
		framePosition.width = Screen.width * (900f / 2560f);
		framePosition.height = Screen.height * (100f / 1600f);
		GUI.DrawTexture (framePosition, healthBarFrame);

	}
}
