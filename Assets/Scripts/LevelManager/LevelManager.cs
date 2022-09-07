using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Spot> _spots = new List<Spot>();
    [SerializeField] private Player _player;

    private CameraSwitcher _cameraSwitcher;

    [Inject]
    public void Construct(CameraSwitcher cameraSwitcher)
    {
        _cameraSwitcher = cameraSwitcher;
    }

    private void OnEnable()
    {
        if (_spots.Capacity > 0)
            InitializeSpots();
        else
            Debug.LogError("Spots list is empty!");
    }

    private void OnDisable() => UnsubscribeSpotsEvents();

    private void Start()
    {
        _player.Setup();

        _player.SplineFollower.followSpeed = 2f;
        _player.SetRunningState();
        
        _cameraSwitcher.SetPlayerFollowCamera();
    }
    
    private void InitializeSpots()
    {
        for (var i = 0; i < _spots.Count; i++)
        {
            var spot = _spots[i];
            spot.Initialize(i + 1, i == _spots.Capacity - 1);
            Debug.Log($"{spot.name} = Number = {i+1}, Is Last = {spot.IsLast}");
        }

        SubscribeSpotsEvents();
    }

    private void SubscribeSpotsEvents()
    {
        foreach (var spot in _spots)
            spot.OnVisited += OnSpotVisited;

        foreach (var spot in _spots)
            spot.OnPassed += OnSpotPassed;
    }

    private void UnsubscribeSpotsEvents()
    {
        foreach (var spot in _spots)
            spot.OnVisited -= OnSpotVisited;

        foreach (var spot in _spots)
            spot.OnPassed -= OnSpotPassed;
    }

    private void OnSpotVisited(Spot spot)
    {
        _player.SplineFollower.followSpeed = 0f;
        _player.SetAggroState();
        
        _cameraSwitcher.SetSpotCamera(spot.Number);
    }

    private void OnSpotPassed(Spot spot)
    {
        _player.SplineFollower.followSpeed = 2f;
        _player.SetRunningState();

        _cameraSwitcher.SetPlayerFollowCamera();
    }
}