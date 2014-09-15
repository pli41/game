using UnityEngine;
using System.Collections;


[System.Serializable]
public class Slot{

	public Item item;
	public bool occupied;
	public Rect itemPosition;

	public static Texture2D test;

	public Slot(Rect position){
		itemPosition = position;
		occupied = false;
	}



	public void addItem(Item item){
		this.item = item;
		occupied = true;
		Debug.Log("Item added");
	}

	public void drawSlot(float frameX, float frameY){
		GUI.DrawTexture (new Rect(frameX + itemPosition.x, frameY + itemPosition.y
		                          , itemPosition.width, itemPosition.height), item.image);
	}
}
