using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Spot> _spots = new List<Spot>();
    [SerializeField] private Player _player;
    
    private Spot _lastSpot;

    private void OnEnable()
    {
        if (_spots.Capacity > 0)
        {
            _lastSpot = _spots[_spots.Capacity - 1];

            foreach (var spot in _spots)
                spot.OnVisited += OnSpotVisited;
            
            foreach (var spot in _spots)
                spot.OnPassed += OnSpotPassed;
        }
        else
            Debug.LogError("Spots list is empty!");
    }

    private void OnDisable()
    {
        foreach (var spot in _spots)
            spot.OnVisited -= OnSpotVisited;
        
        foreach (var spot in _spots)
            spot.OnPassed -= OnSpotPassed;
    }

    private void Start()
    {
        _player.Setup();
        
        _player.SplineFollower.followSpeed = 2f;
        _player.SetRunningState();
    }

    private void OnSpotVisited()
    {
        _player.SplineFollower.followSpeed = 0f;
        _player.SetAimingState();
    }

    private void OnSpotPassed()
    {
        _player.SplineFollower.followSpeed = 2f;
        _player.SetRunningState();
    }
}