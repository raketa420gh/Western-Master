using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void SetPlayerFollowCamera() => _animator.Play(AnimationCameraStateNames.PlayerFollow);

    public void SetSpotCamera(int spotNumber) => _animator.Play(AnimationCameraStateNames.Spot + $"{spotNumber}");
}
