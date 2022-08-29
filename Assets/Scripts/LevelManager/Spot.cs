using System;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public event Action OnVisited;
    public event Action OnPassed;

    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    private int _enemiesCount;

    private void OnEnable()
    {
        if (_enemies.Capacity > 0)
        {
            foreach (var enemy in _enemies)
            {
                enemy.OnDeath += OnEnemyDeath;
                _enemiesCount++;
            }
        }
        else
            Debug.LogError("Enemies list is empty!");
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
            enemy.OnDeath -= OnEnemyDeath;
    }

    public void Visit()
    {
        foreach (var enemy in _enemies)
            enemy.gameObject.SetActive(true);
        
        OnVisited?.Invoke();
    }

    private void Pass()
    {
        OnPassed?.Invoke();
    }

    private void OnEnemyDeath(Character character)
    {
        _enemiesCount--;

        if (_enemiesCount <= 0)
            Pass();
    }
}