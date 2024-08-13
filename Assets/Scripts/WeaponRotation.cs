using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rb2d;
	[SerializeField] private float _speed = 1.0f;

	private void FixedUpdate()
	{
		RotateWeapon();
	}
	
	private void RotateWeapon()
	{
		_rb2d.MoveRotation(_rb2d.rotation + _speed * Time.fixedDeltaTime);
	}
}
