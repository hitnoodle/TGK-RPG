using UnityEngine;
using System.Collections;

public class InventoryViewer : MonoBehaviour {
	public KeyCode showButton;
	public Inventory inventory;
	public Vector2 scrollPosition = Vector2.zero;
	public GUISkin inventorySkin;
	public Vector2 viewSize;
	public Vector2 offset;
	public bool showGUI;
	public int numberOfColumn;
	public int padding;
	public int itemCount;
	public Vector2 scale {
		get {return new Vector2((float)Screen.width/640f,(float)Screen.height/360f);}
	}
	public Rect viewRect {
		get {return new Rect(offset.x*scale.x,offset.y*scale.y,viewSize.x*scale.x,viewSize.y*scale.y);}
	}
	public Vector2 buttonSize {
		get {return new Vector2(scrollViewWidth/numberOfColumn,scrollViewWidth/numberOfColumn);}
	}

	public int scrollViewWidth {
		get {return (int)(viewRect.width-20);}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(showButton))
			showGUI = !showGUI;
	}

	void OnGUI () {
		if(showGUI) {
			GUI.skin = inventorySkin;
			GUI.Box(viewRect,"");
			scrollPosition = GUI.BeginScrollView(viewRect,scrollPosition,new Rect(0,0,scrollViewWidth,((inventory.items.Count-1)/numberOfColumn+1)*buttonSize.y));
			for(int i=0;i<inventory.items.Count;i++){
				int column = i % numberOfColumn;
				int row = i / numberOfColumn;
				Rect buttonRect = new Rect(column*buttonSize.x
				                           ,row*buttonSize.y
				                           ,buttonSize.x
				                           ,buttonSize.y);
				if(GUI.Button(buttonRect,inventory.items[i].icon)){
					if(Event.current.button == 1)
						inventory.RemoveFromInventory(inventory.items[i],1);
					else if(Event.current.button == 0){
						if(inventory is FighterInventory && inventory.items[i] is Equipment)
							((FighterInventory)inventory).AddEquipment(inventory.items[i] as Equipment);
					}
				}
			}
			GUI.EndScrollView();
		}
	}
}
