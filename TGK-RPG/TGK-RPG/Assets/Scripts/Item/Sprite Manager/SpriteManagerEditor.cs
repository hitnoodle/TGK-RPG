using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpriteManager), true)]
public class SpriteManagerEditor : Editor {

	void DrawLayout(SpriteManager sm){

		if(sm.itemName != null){
			for(int i=0;i<sm.itemName.Count;i++){
				EditorGUILayout.LabelField("=========================================");
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField ("Name");
				EditorGUILayout.LabelField (sm.itemName[i]);
				EditorGUILayout.EndHorizontal();
				sm.itemIcon[i].sprite = (Sprite)EditorGUILayout.ObjectField ("Icon", sm.itemIcon[i].sprite , typeof(Sprite), true);
				sm.itemDropTexture[i].sprite = (Sprite)EditorGUILayout.ObjectField ("Drop", sm.itemDropTexture[i].sprite, typeof(Sprite), true);
				sm.itemEquipTexture[i].sprite = (Sprite)EditorGUILayout.ObjectField ("Equip", sm.itemEquipTexture[i].sprite, typeof(Sprite), true);
				EditorGUILayout.LabelField("=========================================");
			}
		}
	}

	public override void OnInspectorGUI ()
	{	
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedObject.Update ();
		
		SpriteManager script = (SpriteManager) target;
		
		DrawLayout(script);
		
		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
		serializedObject.ApplyModifiedProperties ();

		EditorUtility.SetDirty(script);
	}
}
