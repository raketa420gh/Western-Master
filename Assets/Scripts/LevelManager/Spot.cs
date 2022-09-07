using System;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public event Action<Spot> OnVisited;
    public event Action<Spot> OnPassed;

    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    private int _enemiesCount;
    private int _number;
    private bool _isLast;

    public int Number => _number;
    public bool IsLast => _isLast;

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

    public void Initialize(int number, bool isLast)
    {
        _number = number;
        _isLast = isLast;
    }

    public void Visit()
    {
        foreach (var enemy in _enemies)
        {
            enemy.gameObject.SetActive(true);
            enemy.SetAggroState();
        }

        OnVisited?.Invoke(this);
    }

    private void Pass()
    {
        OnPassed?.Invoke(this);
    }

    private void OnEnemyDeath(Character character)
    {
        _enemiesCount--;

        if (_enemiesCount <= 0)
            Pass();
    }
}