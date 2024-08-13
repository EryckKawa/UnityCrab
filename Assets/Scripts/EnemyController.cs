using UnityEngine;
using Random = UnityEngine.Random;

// This class controls the behavior of enemies in the game
public class EnemyController : MonoBehaviour
{
	// Prefab for the enemy object
	[SerializeField] private GameObject _enemyPrefab;

	// Number of enemies to spawn
	[SerializeField] private int _enemyCount = 5;

	// Transform positions for spawning enemies at different corners of the screen
	[SerializeField] private Transform _spawnTopLeft, _spawnTopRight, _spawnBottomLeft, _spawnBottomRight;

	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < _enemyCount; i++)
		{
			SpawnEnemy();
		}
	}

	// Method to spawn an enemy
	private void SpawnEnemy()
	{
		// Select a random position for spawning the enemy
		Vector3 spawnPosition = SelectRandomPosition();
		GameObject enemyObject = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
		Enemy enemy = enemyObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.OnDie += SpawnEnemy;
		}
	}

	// Method to select a random position for spawning an enemy
	private Vector3 SelectRandomPosition()
	{
		Transform selectedTransform = null;

		// Generate a random value between 0 and 3
		int randomValue = Random.Range(0, 4);

		// Convert the random value to a SpawnPointType enum
		SpawnPointType spawnType = (SpawnPointType)randomValue;

		// Select a transform based on the random value
		switch (spawnType)
		{
			case SpawnPointType.TopLeft:
				{
					selectedTransform = _spawnTopLeft;
					break;
				}

			case SpawnPointType.TopRight:
				{
					selectedTransform = _spawnTopRight;
					break;

				}
			case SpawnPointType.BottomLeft:
				{
					selectedTransform = _spawnBottomLeft;
					break;
				}

			case SpawnPointType.BottomRight:
				{
					selectedTransform = _spawnBottomRight;
					break;
				}

			default:
				{
					selectedTransform = _spawnTopLeft;
					break;
				}
		}

		// Return the position of the selected transform
		return selectedTransform.position + (Vector3)Random.insideUnitCircle;
	}

	void Update()
	{
	}
}

// Enum defining the different spawn point types
public enum SpawnPointType
{
	TopLeft,    // 0
	TopRight,   // 1
	BottomLeft, // 2
	BottomRight // 3
}