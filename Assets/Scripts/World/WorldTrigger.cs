using UnityEngine;
using System.Collections;

public class WorldTrigger : MonoBehaviour 
{
	public bool NeedActivate = false;

	public delegate void onTrigger(GameObject go);
	public onTrigger OnTrigger;
	
	protected GameObject objectOnTrigger;

	public void Activate()
	{
		if (NeedActivate && OnTrigger != null && objectOnTrigger != null)
			OnTrigger(objectOnTrigger);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		objectOnTrigger = coll.gameObject;

		if (!NeedActivate && OnTrigger != null)
			OnTrigger(objectOnTrigger);
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		objectOnTrigger = null;
	}
}
