using UnityEngine;
using System.Collections;


[System.Serializable]
public class Slot{
	public bool showInventory;
	public Item item;
	public bool occupied;
	public bool mouseOver;
	//itemPosition is the position of the item in the bag
	public Rect itemPosition;
	//slotPosition is the position of the slot on the screen
	public Rect slotPosition;
	public Texture2D slotFrame;
	public Texture2D slotFrame1;

	public Slot(Rect position, Texture2D frame, Texture2D frame1){
		slotFrame = frame;
		slotFrame1 = frame1;
		itemPosition = position;
		occupied = false;
		mouseOver = false;
	}



	public void addItem(Item item){
		this.item = item;
		occupied = true;
		Debug.Log("Item added");
	}


	//FrameX and FrameY is the position of the top-left corner of the first slot
	public void drawSlot(float frameX, float frameY){
		slotPosition = new Rect (frameX + itemPosition.x, frameY + itemPosition.y
		                        , itemPosition.width, itemPosition.height); 
		if(mouseOver){
			GUI.DrawTexture (slotPosition, slotFrame1);
		}
		else{
			GUI.DrawTexture (slotPosition, slotFrame);
		}

		if(occupied){
			GUI.DrawTexture (slotPosition, item.image);
		}



	}

	public void useItem(){
		item.performAction();
		item = null;
		occupied = false;
		Debug.Log ("Item used");
	}

	public void checkMouse(){
		if(slotPosition.Contains(Event.current.mousePosition)){
			mouseOver = true;
		}
		else{
			mouseOver = false;
		}




		if(Input.GetMouseButtonDown(0) && mouseOver && occupied){
			useItem();
		}
	}


}
