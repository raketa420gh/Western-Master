using UnityEngine;

public class PistolGun : MonoBehaviour, IFirearms
{
    [SerializeField] private PlayerBullet playerBullet;
    [SerializeField] private Transform _muzzle;

    public Transform Muzzle => _muzzle;

    private int _magazineCapacity = 1;

    public int MagazineCapacity => _magazineCapacity;
    
    public void Shoot(Vector3 direction)
    {
        var bullet = Instantiate(playerBullet, _muzzle.position, Quaternion.identity);
        bullet.SetDirection(direction);
    }
}