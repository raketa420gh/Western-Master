using UnityEngine;

public class PistolGun : MonoBehaviour, IFirearms
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _muzzle;

    private int _magazineCapacity = 1;

    public int MagazineCapacity => _magazineCapacity;
    
    public void Shoot(Vector3 direction)
    {
        var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
        bullet.SetDirection(direction);
    }
}