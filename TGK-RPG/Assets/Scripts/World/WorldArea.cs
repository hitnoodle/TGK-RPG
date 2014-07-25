using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldArea : MonoBehaviour 
{
	public string Name;

	public Boundary WalkingBoundary;

	// Use this for initialization
	void Start () 
	{
	}

	#if UNITY_EDITOR

	private static float BOUNDARY_DEPTH = -9.9f;

	public bool ShowGizmo = false;

	void OnDrawGizmos ()
	{
		if(!ShowGizmo)
			return;

		//Walking boundary
		Gizmos.color = Color.magenta;

		Vector3 bottomLeft = new Vector3(WalkingBoundary.BottomLeft.x, WalkingBoundary.BottomLeft.y, BOUNDARY_DEPTH);
		Vector3 topRight = new Vector3(WalkingBoundary.TopRight.x, WalkingBoundary.TopRight.y, BOUNDARY_DEPTH);
		Vector3 bottomRight = new Vector3(topRight.x, bottomLeft.y, BOUNDARY_DEPTH);
		Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y, BOUNDARY_DEPTH);

		Gizmos.DrawLine(bottomLeft, topLeft);
		Gizmos.DrawLine(topLeft, topRight);
		Gizmos.DrawLine(topRight, bottomRight);
		Gizmos.DrawLine(bottomRight, bottomLeft);
	}

	#endif
}
