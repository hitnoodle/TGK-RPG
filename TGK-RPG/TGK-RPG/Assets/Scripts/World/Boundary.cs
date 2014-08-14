using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public Vector2 BottomLeft;
	public Vector2 TopRight;
	
	public float Top
	{
		get { return TopRight.y; }
	}

	public float Bottom
	{
		get { return BottomLeft.y; }
	}
	
	public float Left
	{
		get { return BottomLeft.x; }
	}
	
	public float Right
	{
		get { return TopRight.x; }
	}
	
	public bool IsZero
	{
		get { return BottomLeft == Vector2.zero && TopRight == Vector2.zero; }
	}
	
	protected Rect _Rectangle;
	public Rect Rectangle
	{
		get { return _Rectangle; }
	}
	
	public Boundary(Vector2 bottomLeft, Vector2 topRight)
	{
		BottomLeft = bottomLeft;
		TopRight = topRight;
		
		_Rectangle = new Rect(bottomLeft.x, topRight.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
	}
}