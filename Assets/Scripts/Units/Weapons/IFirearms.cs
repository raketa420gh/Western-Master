using UnityEngine;

public interface IFirearms
{
    int MagazineCapacity { get; }

    void Shoot(Vector3 direction);
}