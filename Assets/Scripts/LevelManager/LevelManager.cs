using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LevelUIManager))]

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Spot> _spots = new List<Spot>();

    private SceneLoader _sceneLoader;
    private SplineComputer _splineComputer;
    private Player _player;
    private CameraSwitcher _cameraSwitcher;
    private LevelUIManager _ui;

    [Inject]
    public void Construct(SceneLoader sceneLoader, SplineComputer splineComputer, Player player, CameraSwitcher cameraSwitcher)
    {
        _sceneLoader = sceneLoader;
        _splineComputer = splineComputer;
        _player = player;
        _cameraSwitcher = cameraSwitcher;
        
        _ui = GetComponent<LevelUIManager>();
    }

    private void OnEnable() => Initialize();

    private void OnDisable() => Deinitialize();

    private void Start()
    {
        _player.Setup();
        _cameraSwitcher.SetPlayerFollowCamera();
        
        _ui.ToggleStartPanel(true);
    }

    public void StartLevel()
    {
        _ui.ToggleStartPanel(false);
        _ui.ToggleHUD(true);
        
        StartRunPlayer();
    }

    public void Restart() => _sceneLoader.RestartScene();

    public void StartNextLevel() => _sceneLoader.LoadNextScene();

    private void Initialize()
    {
        _splineComputer.Rebuild();
        
        if (_spots.Capacity > 0)
            InitializeSpots();
        else
            Debug.LogError("Spots list is empty!");

        _player.OnDeath += OnPlayerDeath;
    }

    private void InitializeSpots()
    {
        for (var i = 0; i < _spots.Count; i++)
        {
            var spot = _spots[i];
            spot.Setup(i + 1, i == _spots.Capacity - 1);
            Debug.Log($"{spot.name} = Number = {i+1}, Is Last = {spot.IsLast}");
        }
        
        foreach (var spot in _spots)
            spot.OnVisited += OnSpotVisited;

        foreach (var spot in _spots)
            spot.OnPassed += OnSpotPassed;
    }

    private void Deinitialize()
    {
        foreach (var spot in _spots)
            spot.OnVisited -= OnSpotVisited;

        foreach (var spot in _spots)
            spot.OnPassed -= OnSpotPassed;
    }

    private void StartRunPlayer()
    {
        _player.SplineFollower.followSpeed = 3f;
        _player.SetRunningState();
    }

    private async Task PassSpotAfterDelay(float seconds, CancellationToken cancellationToken)
    {
        _player.SetIdleState();
        
        await Task.Delay(TimeSpan.FromSeconds(seconds), cancellationToken);
        
        _player.SplineFollower.followSpeed = 2f;
        _player.SetRunningState();

        _cameraSwitcher.SetPlayerFollowCamera();
    }

    private void LoseLevel()
    {
        _cameraSwitcher.SetLoseCamera();
        _ui.ToggleWinPanel(true);
    }

    private void OnSpotVisited(Spot spot)
    {
        _player.SplineFollower.followSpeed = 0f;

        if (spot.IsLast)
        {
            _player.SetVictoryState();
            _cameraSwitcher.SetFinishCamera();
            
            _ui.ToggleHUD(false);
            _ui.ToggleWinPanel(true);
        }
        else
        {
            _player.SetAggroState();
            _player.LookAtSmoothOnlyY(spot.ObservationPoint);
            _cameraSwitcher.SetSpotCamera(spot.Number);
        }
    }

    private void OnSpotPassed(Spot spot)
    {
        PassSpotAfterDelay(1f, new CancellationToken());
    }

    private void OnPlayerDeath(Character character)
    {
        LoseLevel();
    }
}