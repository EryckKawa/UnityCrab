using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private int rotationSpeed;
	[SerializeField] private Vector3 rotationPoint = Vector3.zero;
	private void Update()
	{
		float rotateAmount = CalculateRotateAmount();
		transform.RotateAround(rotationPoint, Vector3.forward, rotateAmount);
	}

	private float CalculateRotateAmount()
	{
		return rotationSpeed * Time.deltaTime;
	}
}
