using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Item: MonoBehaviour{



	public Texture2D image;
	public int xInBag;
	public int yInBag;

	public abstract void performAction ();



}
