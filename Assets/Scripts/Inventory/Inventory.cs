using UnityEngine;
using System.Collections.Generic;

public class Inventory: MonoBehaviour {
	//321*434 is the size of the inventory GUI
	public Texture2D image;
	public bool showInventory;
	public Rect position;

	public List<Item> items = new List<Item>();
	private int slotWidthNum = 10;
	private int slotHeightNum = 4;
	public Slot[,] slots;
	public int slotWidth = 29;
	public int slotHeight = 29;
	public Texture2D slotFrame;
	public Texture2D slotFrame2;
	

	// Use this for initialization
	void Start () {
		showInventory = false;
		setSlots ();
	}

	void setSlots(){
		slots = new Slot[slotWidthNum, slotHeightNum];
		for(int x = 0; x < slotWidthNum; x++){
			for(int y = 0; y < slotHeightNum; y++){
				slots[x,y] = new Slot(new Rect(slotWidth * x, slotHeight * y, slotWidth, slotHeight), slotFrame, slotFrame2);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.B)) {
			showInventory = !showInventory;
		}

		if(showInventory){
			updateSlotsState(true);
		}
		else{
			updateSlotsState(false);
		}

	}

	void OnGUI(){
		if(showInventory){
			drawInventory ();
			drawSlots();
			checkMouseForSlots();
		}
	}


	void addItem(Item item){
		Slot EmptySlot = findEmptySlot ();
		if(EmptySlot != null){
			findEmptySlot().addItem (item);
		}
		else{
			Debug.Log("Your bag is full");
		}
	}

	Slot findEmptySlot(){
		// x is horizontal 
		for(int y = 0; y < slotHeightNum; y++){
			for(int x = 0; x < slotWidthNum; x++){
				if (!slots[x,y].occupied){
					Debug.Log("Empty slot found");
					return slots[x,y];
				}
			}
		}
		return null;
	}

	void drawInventory(){
		position.x = (Screen.width - position.width) * 0.92f;
		position.y = (Screen.height - position.height) * 0.15f;
		GUI.DrawTexture (position, image);
	}

	void drawSlots(){
		for(int x = 0; x < slotWidthNum; x++){
			for(int y = 0; y < slotHeightNum; y++){
				slots[x,y].drawSlot(position.x + 16, position.y + 255);
			}
		}
	}

	void checkMouseForSlots(){
		for(int x = 0; x < slotWidthNum; x++){
			for(int y = 0; y < slotHeightNum; y++){
				slots[x,y].checkMouse();
			}
		}
	}



	void updateSlotsState(bool state){
		for(int x = 0; x < slotWidthNum; x++){
			for(int y = 0; y < slotHeightNum; y++){
				slots[x,y].showInventory = state;
			}
		}
	}



}
