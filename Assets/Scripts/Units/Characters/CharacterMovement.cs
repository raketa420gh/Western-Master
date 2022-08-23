using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour, IMovement
{
    private CharacterController _controller;
    
    public float Speed { get; private set; }

    private void Awake() => _controller = GetComponent<CharacterController>();

    public void Move(Vector3 direction) => _controller.Move(direction);

    public void Setup(CharacterData data) => Speed = data.MoveSpeed;
}