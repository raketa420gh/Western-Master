using UnityEngine;

public class PistolGun : MonoBehaviour, IFirearms
{
    [SerializeField] private ProjectileBase _bullet;
    [SerializeField] private Transform _muzzle;

    public Transform Muzzle => _muzzle;

    private int _magazineCapacity = 1;
    private int _currentBullets = 1;
    private bool _isMagazineEmpty;

    public int MagazineCapacity => _magazineCapacity;

    public void Setup(int capacity)
    {
        _magazineCapacity = capacity;
        _currentBullets = _magazineCapacity;
    }
    
    public void Shoot(Vector3 direction)
    {
        if (_isMagazineEmpty)
        {
            Debug.Log("Magazine is empty");
            return;
        }
        
        var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
        bullet.SetDirection(direction);

        _currentBullets--;

        if (_currentBullets <= 0)
            _isMagazineEmpty = true;
    }
}