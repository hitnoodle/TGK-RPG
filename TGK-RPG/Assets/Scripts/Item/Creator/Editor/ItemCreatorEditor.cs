using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ItemCreator), true)]
public class ItemCreatorEditor : Editor 
{	
	private SpriteManager sm;

	public void OnEnable()
	{
		if(serializedObject == null)
			return;
		if(sm==null)
			sm = ((GameObject)Resources.Load("Item/Sprite_Manager")).GetComponent<SpriteManager>();
	}
	
	void DrawSaveLayout(ItemCreator creator)
	{
		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("Save"))
			creator.Save();

		if (GUILayout.Button("Close"))
			creator.Done();

		if (GUILayout.Button("Delete"))
			creator.Delete(sm);

		EditorGUILayout.EndHorizontal();
	}

	void DrawCreateLayout(ItemCreator creator)
	{
		EditorGUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Create"))
			CreateNewItem(creator);
		
		if (GUILayout.Button("Cancel"))
			creator.Done();
		
		EditorGUILayout.EndHorizontal();
	}

	int index1, index2;
	string[] itemType = new string[]{ "Weapon", "Armor", "Consumable" };
	string[] weaponType = new string[]{ "One Handed Melee", "Two Handed Melee", "Bow", "Staff", "Shield" };
	string[] armorType = new string[]{ "Head", "Body", "Hand", "Leg"};
	string[] consumableType = new string[]{ "HP Potion", "Mana Potion"};

	void DrawCreateItemLayout(Item item){
		index1 = EditorGUILayout.Popup("Item Type :", index1, itemType, EditorStyles.popup);
		if(index1==0){
			index2 = EditorGUILayout.Popup("Weapon Type :", index2, weaponType, EditorStyles.popup);
		}
		else if(index1==1){
			index2 = EditorGUILayout.Popup("Armor Type :", index2, armorType, EditorStyles.popup);
		}
		else if(index1==2){
			index2 = EditorGUILayout.Popup("Consumable Type :", index2, consumableType, EditorStyles.popup);
		}
	}

	void CreateNewItem(ItemCreator creator){
		Item newItem = null;

		if(index1==0){
			if(index2==0){ //One Handed Melee
				OneHandedMelee ohm = new OneHandedMelee();
				newItem = ohm as Item;
			}
			else if(index2==1){ //Two Handed Melee
				TwoHandedMelee thm = new TwoHandedMelee();
				newItem = thm as Item;
			}
			else if(index2==2){ //Bow
				Bow b = new Bow();
				newItem = b as Item;
			}
			else if(index2==3){ //Staff
				Staff s = new Staff();
				newItem = s as Item;
			}
			else if(index2==4){ //Shield
				Shield s = new Shield();
				newItem = s as Item;
			}
		}
		else if(index1==1){
			if(index2==0){ //Head
				HeadArmor ha = new HeadArmor();
				newItem = ha as Item;
			}
			else if(index2==1){ //Body
				BodyArmor ba = new BodyArmor();
				newItem = ba as Item;
			}
			else if(index2==2){ //Hand
				HandArmor ha = new HandArmor();
				newItem = ha as Item;
			}
			else if(index2==3){ //Leg
				LegArmor la = new LegArmor();
				newItem = la as Item;
			}
		}
		else if(index1==2){
		
		}

		newItem.ID = creator.itemID;

		creator._Item = newItem;
		creator.Save();
		
		creator.AddSpriteManager(sm);

		creator.isEditing = true;
		creator.isCreating = false;
	}

	void DrawEditItemLayout(Item item)
	{
		item.ID = EditorGUILayout.TextField ("Item ID", item.ID);

		item.description = EditorGUILayout.TextField ("Description", item.description);

		if(item is Weapon){
			Weapon weapon = item as Weapon;

			if(weapon != null){
				
				weapon.damage = EditorGUILayout.FloatField("Damage", weapon.damage);
			}
		}
		else if(item is Armor){
			Armor armor = item as Armor;

			if(armor != null){
				
				armor.armor = EditorGUILayout.FloatField("Armor Value", armor.armor);
			}
		}
		else if(item is Consumable){
			
		}

		EditorGUILayout.Space();
	}
	
	public override void OnInspectorGUI ()
	{	
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedObject.Update ();
		
		ItemCreator script = (ItemCreator) target;

		SerializedProperty itemID = serializedObject.FindProperty("itemID");
		itemID.stringValue = EditorGUILayout.TextField ("Item ID", itemID.stringValue);
		
		if (GUILayout.Button("Load / Create"))
		{
			script.Load(script.itemID);

			return;
		}

		if(script.isCreating)
		{
			EditorGUILayout.LabelField("=========================================");

			DrawCreateItemLayout(script._Item);

			DrawCreateLayout(script);
			
			EditorGUILayout.LabelField("=========================================");
		}

		if (script.isEditing)
		{
			EditorGUILayout.LabelField("=========================================");
			
			DrawEditItemLayout(script._Item);

			DrawSaveLayout(script);
			
			EditorGUILayout.LabelField("=========================================");
		}
		
		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
		serializedObject.ApplyModifiedProperties ();
	}
	
}
