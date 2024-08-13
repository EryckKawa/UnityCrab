using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	private string _horizontalAxis = "Horizontal";
	private string _verticalAxis = "Vertical";
	[SerializeField] private Rigidbody2D _rb2d;
	[SerializeField] private float _speed = 3;
	private Vector2 _input;

	public UnityEvent OnPlayerDie;

	private void FixedUpdate()
	{
		MovePlayer();
	}

	// Update is called once per frame
	void Update()
	{
		HandleMovement();
	}

	private void HandleMovement()
	{
		float horizontalInput = Input.GetAxis(_horizontalAxis);
		float verticalInput = Input.GetAxis(_verticalAxis);
		_input = new Vector2(horizontalInput, verticalInput);
		_input.Normalize();
	}

	private void MovePlayer()
	{
		_rb2d.velocity = _input * _speed;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		OnPlayerDie?.Invoke();
		Destroy(gameObject);
	}
}
