﻿using UnityEngine;
using System.Collections.Generic;

public class Inventory: MonoBehaviour {
	//321*434
	public Texture2D image;
	public bool showInventory;
	public Rect position;

	public List<Item> items = new List<Item>();
	private int slotWidthNum = 10;
	private int slotHeightNum = 4;
	public Slot[,] slots;
	public int slotX;
	public int slotY;
	public int slotWidth = 26;
	public int slotHeight = 26;
	

	// Use this for initialization
	void Start () {
		showInventory = false;
		setSlots ();
	}

	void setSlots(){
		slots = new Slot[slotWidthNum, slotHeightNum];
		for(int x = 0; x < slotWidthNum; x++){
			for(int y = 0; y < slotHeightNum; y++){
				slots[x,y] = new Slot(new Rect(slotX + slotWidth * x, slotY + slotHeight * y, slotWidth, slotHeight));
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.B)) {
			showInventory = !showInventory;
		}
	}

	void OnGUI(){
		if(showInventory){
			drawInventory ();
			drawSlots();
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
		for(int x = 0; x < slotWidthNum; x++){
			for(int y = 0; y < slotHeightNum; y++){
				if (!slots[x,y].occupied){
					return slots[x,y];
					Debug.Log("Empty slot found");
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
				if(slots[x,y].occupied){
					slots[x,y].drawSlot(position.x + 16, position.y + 255);
				}

			}
		}
	}

}
