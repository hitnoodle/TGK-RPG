using UnityEngine;
using UnityEditor;
using System.Collections;

using RPG.Stories;

[CustomEditor(typeof(ShopDesigner), true)]
public class ShopDesignerEditor : Editor {
	void DrawSaveLayout(ShopDesigner designer)
	{
		EditorGUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Clear"))
			designer.Clear();
		
		if (GUILayout.Button("Save"))
			designer.Save();
		
		EditorGUILayout.EndHorizontal();
		
		if (GUILayout.Button("Done"))
			designer.Done();
	}

	void DrawStoryLayout(Shop shop)
	{
		SerializedProperty storyProperty = serializedObject.FindProperty("OShop");
		
		SerializedProperty shopId = storyProperty.FindPropertyRelative("shopId");
		shopId.stringValue = EditorGUILayout.TextField ("Shop Id", shopId.stringValue);


		SerializedProperty sellValue = storyProperty.FindPropertyRelative("sellValue");
		sellValue.intValue = int.Parse (EditorGUILayout.TextField ("sellValue", sellValue.intValue.ToString()));

		SerializedProperty listSellItemId = storyProperty.FindPropertyRelative("listSellItemId").FindPropertyRelative("Array");

		int length = listSellItemId.arraySize;
		EditorGUILayout.LabelField("List Item");
		for(int i = 0;i < length;i++){
			SerializedProperty sellItemId = listSellItemId.FindPropertyRelative ("data[" + i + "]");
			sellItemId.stringValue = EditorGUILayout.TextField ("Id Item " + i, sellItemId.stringValue);
		}

		if (EditorGUILayout.Toggle("Add List Sell Item", false))
		{
			shop.listSellItemId.Add("");
		}

		
		EditorGUILayout.Space();
	}

	public override void OnInspectorGUI ()
	{	
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedObject.Update ();
		
		ShopDesigner script = (ShopDesigner) target;
		
		SerializedProperty shopId = serializedObject.FindProperty("shopId");
		shopId.stringValue = EditorGUILayout.TextField ("Shop Id", shopId.stringValue);



		if (GUILayout.Button("Load / Create")){
			script.Load(script.shopId);
			
			return;
		}

		if (script.IsEditing)
		{
			EditorGUILayout.LabelField("=========================================");
			
			DrawStoryLayout(script.OShop);
			
			DrawSaveLayout(script);
			
			EditorGUILayout.LabelField("=========================================");
		}
		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
		serializedObject.ApplyModifiedProperties ();
	}


}
