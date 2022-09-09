using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class PlayerDetector : MonoBehaviour
{
    [BoxGroup("Parameters"), SerializeField] private LayerMask _playerLayerMask;
    [BoxGroup("Parameters"), SerializeField] private Transform _centerTransform;

    private float _viewRadius = 250;
    private Player _player;
    private Transform _transform;
    private Transform _playerTransform;
    private Vector3 _lookDirection;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
        
        _transform = transform;
        _playerTransform = _player.CenterTransform;
    }

    public void Setup(float viewRadius) => _viewRadius = viewRadius;

    public bool IsSeePlayer()
    {
        _lookDirection = _playerTransform.position - _transform.position;
 
        if (Vector2.Distance(_transform.position, _playerTransform.position) <= _viewRadius
            && Physics.Raycast(_transform.position, _lookDirection, out var hit, _viewRadius, _playerLayerMask))
            return hit.transform.CompareTag(TagNames.Player);

        return false;
    }

    public Vector3 GetPlayerPosition() => _player.GetPosition;
}