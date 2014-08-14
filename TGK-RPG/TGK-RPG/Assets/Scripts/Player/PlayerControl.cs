using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	public float Speed = 5f;
	public bool ClampSpeed = true;
	public bool	MoveDiagonally = false;

	public WorldArea Area;

	protected Transform _Transform;
	protected Boundary _MoveBoundary;

	protected float _SpriteHalfWidth;
	protected float _SpriteHalfHeight;

	// Use this for initialization
	void Start() 
	{
		_Transform = transform;
		_MoveBoundary = Area.WalkingBoundary;

		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		_SpriteHalfWidth = sprite.bounds.size.x / 2;
		_SpriteHalfHeight = sprite.bounds.size.y / 2;
		//Debug.Log ("Player Size: " + new Vector2 (_SpriteHalfWidth, _SpriteHalfHeight));
	}
	
	// Update is called once per frame
	void Update() 
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		Move(horizontal, vertical);

	}

	void Move(float horizontal, float vertical)
	{
		Vector3 position = _Transform.localPosition;

		if (ClampSpeed) 
		{
			if (horizontal > 0) horizontal = 1;
			else if (horizontal < 0) horizontal = -1;

			if (vertical > 0) vertical = 1;
			else if (vertical < 0) vertical = -1;
		}

		//position.x += horizontal * Speed * Time.deltaTime;
		//if (MoveDiagonally || horizontal == 0) position.y += vertical * Speed * Time.deltaTime;

		if (_MoveBoundary.IsZero)
		{
			position.x += horizontal * Speed * Time.deltaTime;
			position.y += vertical * Speed * Time.deltaTime;
		}
		else
		{
			if ((_MoveBoundary.Left + _SpriteHalfWidth < position.x && position.x < _MoveBoundary.Right - _SpriteHalfWidth) //Between left and right
			    || (position.x < _MoveBoundary.Left + _SpriteHalfWidth && horizontal > 0) //On left boundary, can only move right
			    || (_MoveBoundary.Right - _SpriteHalfWidth < position.x && horizontal < 0)) //On right boundary, can only move left
				position.x += horizontal * Speed * Time.deltaTime;

			if (MoveDiagonally || horizontal == 0)
			{
				if ((_MoveBoundary.Bottom + _SpriteHalfHeight < position.y && position.y < _MoveBoundary.Top - _SpriteHalfHeight) //Between bottom and top
				    || (position.y < _MoveBoundary.Bottom + _SpriteHalfHeight  && vertical > 0) //On bottom boundary, can only move up
				    || (_MoveBoundary.Top - _SpriteHalfHeight < position.y && vertical < 0)) //On top boundary, can only move down
					position.y += vertical * Speed * Time.deltaTime;
			}
		}

		_Transform.position = position;
	}
}
