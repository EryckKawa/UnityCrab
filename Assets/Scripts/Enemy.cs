using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

// This class represents an enemy in the game
public class Enemy : MonoBehaviour
{
    // Speed at which the enemy moves
    [SerializeField] private float _speed = 1;

    // Reference to the Rigidbody2D component of the enemy
    private Rigidbody2D _rb2d;

    // Reference to the player's transform
    private Transform _playerTransform;

    // Flag to indicate if the enemy is stopped
    public bool Stopped = false;

    // Prefab to instantiate when the enemy dies
    [SerializeField] private GameObject _crabDead;

    // Event that is triggered when the enemy dies
    public event Action OnDie = null; // Stores a method

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        _rb2d = GetComponent<Rigidbody2D>();

        // Find the player object in the scene
        Player player = FindAnyObjectByType<Player>();

        // Check if the player object was found
        if (player != null)
        {
            // Set the player's transform
            _playerTransform = player.transform;
        }
        else
        {
            // If no player is found, stop the enemy
            Stopped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy towards the player
        Move();
    }

    // Method to move the enemy
    private void Move()
    {
        // Check if the enemy is stopped or the player's transform is not set
        if (Stopped || _playerTransform == null)
        {
            // Set the velocity to zero to stop the enemy
            _rb2d.velocity = Vector2.zero;
            return;
        }

        // Calculate the direction to the player
        Vector3 directionToPlayer = _playerTransform.position - transform.position;

        // Set the velocity of the enemy towards the player
        _rb2d.velocity = directionToPlayer.normalized * _speed;
    }

    // Method called when the enemy collides with another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to a weapon
        if (other.CompareTag("Weapon"))
        {
            // Instantiate the dead crab prefab at the enemy's position
            Instantiate(_crabDead, transform.position, Quaternion.identity);

            // Destroy the enemy game object
            Destroy(gameObject);

            // Check if there are any subscribers to the OnDie event
            if (OnDie != null)
            {
                // Trigger the OnDie event
                OnDie();
            }
        }
    }
}