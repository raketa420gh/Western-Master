using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Spot> _spots = new List<Spot>();
    
    private Player _player;
    private CameraSwitcher _cameraSwitcher;

    [Inject]
    public void Construct(Player player, CameraSwitcher cameraSwitcher)
    {
        _player = player;
        _cameraSwitcher = cameraSwitcher;
    }

    private void OnEnable() => Initialize();

    private void OnDisable() => Disable();

    private void Start()
    {
        _player.Setup();

        _player.SplineFollower.followSpeed = 2f;
        _player.SetRunningState();
        
        _cameraSwitcher.SetPlayerFollowCamera();
    }

    private void Initialize()
    {
        if (_spots.Capacity > 0)
            InitializeWay();
        else
            Debug.LogError("Spots list is empty!");
    }

    private void InitializeWay()
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
    
    private void Disable()
    {
        foreach (var spot in _spots)
            spot.OnVisited -= OnSpotVisited;

        foreach (var spot in _spots)
            spot.OnPassed -= OnSpotPassed;
    }

    private void OnSpotVisited(Spot spot)
    {
        _player.SplineFollower.followSpeed = 0f;

        if (spot.IsLast)
        {
            _player.SetIdleState();
            _cameraSwitcher.SetFinishCamera();
            //ui finish panel on
        }
        else
        {
            _player.SetAggroState();
            _cameraSwitcher.SetSpotCamera(spot.Number);
        }
    }

    private void OnSpotPassed(Spot spot)
    {
        _player.SplineFollower.followSpeed = 2f;
        _player.SetRunningState();

        _cameraSwitcher.SetPlayerFollowCamera();
    }
}