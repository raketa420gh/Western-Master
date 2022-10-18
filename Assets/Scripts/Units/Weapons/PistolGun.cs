using System;
using UnityEngine;

public class PistolGun : MonoBehaviour, IFirearms
{
    public event Action<int> OnBulletCountChanged;
    
    [SerializeField] private ProjectileBase _bullet;
    [SerializeField] private Transform _muzzle;

    public Vector3 MuzzlePosition => _muzzle.transform.position;

    private int _magazineCapacity = 1;
    private int _currentBullets = 1;
    private bool _isMagazineEmpty;

    public int MagazineCapacity => _magazineCapacity;

    public void Setup(int capacity)
    {
        _magazineCapacity = capacity;
        _currentBullets = _magazineCapacity;
        
        OnBulletCountChanged?.Invoke(_currentBullets);
    }
    
    public void Shoot(Vector3 direction)
    {
        if (_isMagazineEmpty)
            return;

        CreateBullet(direction);
        DecreaseBulletAmount();
    }

    private void CreateBullet(Vector3 direction)
    {
        var bullet = Instantiate(_bullet, MuzzlePosition, Quaternion.identity);
        bullet.SetDirection(direction);
    }

    private void DecreaseBulletAmount()
    {
        _currentBullets--;
        
        OnBulletCountChanged?.Invoke(_currentBullets);
        
        if (_currentBullets <= 0)
            _isMagazineEmpty = true;
    }
}