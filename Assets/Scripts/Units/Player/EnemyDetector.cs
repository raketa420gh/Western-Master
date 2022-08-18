using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<Enemy> OnEnemyDetected;
    public event Action<Enemy> OnEnemyUnobserved;

    private Enemy _closestEnemy;

    private readonly List<Enemy> _detectedEnemies = new List<Enemy>();

    public Transform GetClosestEnemyTransform() => GetClosestEnemy().CenterTransform;

    private void Start()
    {
        _detectedEnemies.AddRange(FindObjectsOfType<Enemy>());

        foreach (var enemy in _detectedEnemies)
            enemy.OnDeath += OnEnemyDeath;
    }

    public Enemy GetClosestEnemy()
    {
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Enemy potentialEnemy in _detectedEnemies)
        {
            Vector3 directionToTarget = potentialEnemy.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                _closestEnemy = potentialEnemy;
            }
        }

        return _closestEnemy;
    }

    private void OnEnemyDeath(Enemy enemy) => _detectedEnemies.Remove(enemy);
}