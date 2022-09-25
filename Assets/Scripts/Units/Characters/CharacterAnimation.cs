using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CharacterAnimation : MonoBehaviour, ICharacterAnimation
{
    [SerializeField] private Animator _animator;

    public void PlayIdle() => _animator.SetTrigger(AnimationParameterNames.Idle);
    
    public void StopIdle() => _animator.SetBool(AnimationParameterNames.Idle, false);

    public void PlayAim() => _animator.SetTrigger(AnimationParameterNames.Aim);
    
    public void StopAim() => _animator.SetBool(AnimationParameterNames.Aim, false);
    
    public void PlayRun() => _animator.SetBool(AnimationParameterNames.Run, true);

    public void StopRun() => _animator.SetBool(AnimationParameterNames.Run, false);

    public void PlayDance() => _animator.SetTrigger(AnimationParameterNames.Dance);
}