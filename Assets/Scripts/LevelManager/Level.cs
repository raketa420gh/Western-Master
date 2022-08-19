using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Level : MonoBehaviour
{
    public event Action OnStarted;
    public event Action OnFinished;

    private SceneLoader _sceneLoader;
    private List<Enemy> _levelEnemies = new List<Enemy>();
    private int _enemiesCount;

    [Inject]
    public void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

    //private void Start() => StartLevel();

    private void StartLevel()
    {
        FindEnemies();

        _enemiesCount = _levelEnemies.Count;

        foreach (var enemy in _levelEnemies)
            enemy.OnDeath += OnEnemyDeath;
    }

    private void FinishLevel()
    {
        foreach (var enemy in _levelEnemies)
            enemy.OnDeath -= OnEnemyDeath;
        
        _sceneLoader.LoadNextScene();
        
        OnFinished?.Invoke();
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        _enemiesCount--;

        if (_enemiesCount <= 0)
            FinishLevel();
    }

    private void FindEnemies() => _levelEnemies.AddRange(FindObjectsOfType<Enemy>());
}