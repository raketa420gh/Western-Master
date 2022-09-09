using UnityEngine;

public interface IMovement
{
    float Speed { get; }
    
    void Move(Vector3 direction);
}