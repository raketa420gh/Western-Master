using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class PlayerDetector : MonoBehaviour
{
    [BoxGroup("Parameters"), SerializeField] private LayerMask _playerLayerMask;
    [BoxGroup("Parameters"), SerializeField] private Transform _centerTransform;

    private float _viewRadius;
    private Player _player;
    private Transform _transform;
    private Transform _playerTransform;
    private Vector3 _lookDirection;

    [Inject]
    public void Construct(Player player) => _player = player;

    private void Awake()
    {
        _transform = transform;
        _playerTransform = _player.PlayerCenterTransform;
    }

    private void OnEnable() => _player.OnDeath += OnPlayerDeath;

    private void OnDisable() => _player.OnDeath -= OnPlayerDeath;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_centerTransform.position, _viewRadius);

        if (!_player) 
            return;
        
        Gizmos.color = Color.red;
        
        if (Vector3.Distance(_centerTransform.position, GetPlayerTransform().position) < _viewRadius)
            Gizmos.DrawLine(_centerTransform.position, GetPlayerTransform().position);
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

    public Transform GetPlayerTransform() => _player.PlayerCenterTransform;

    private void OnPlayerDeath() => Destroy(this);
}