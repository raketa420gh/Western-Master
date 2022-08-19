using RootMotion.Dynamics;
using Sirenix.OdinInspector;
using UnityEngine;

public class HumanoidRagdoll : MonoBehaviour
{
    [BoxGroup("Components"), SerializeField] private PuppetMaster _puppetMaster;
    [BoxGroup("MusclesIndexes"), SerializeField] private int _hipsIndex = 0;
    [BoxGroup("MusclesIndexes"), SerializeField] private int _lefLegIndex = 2;
    [BoxGroup("MusclesIndexes"), SerializeField] private int _rightLegIndex = 5;
    [BoxGroup("MusclesIndexes"), SerializeField] private int _leftArmIndex = 8;
    [BoxGroup("MusclesIndexes"), SerializeField] private int _rightArmIndex = 12;

    private Muscle _hipsMuscle;
    private Muscle _leftLegMuscle;
    private Muscle _rightLegMuscle;
    private Muscle _leftArmMuscle;
    private Muscle _rightArmMuscle;

    private void Awake() => InitializeExtremities();

    public void Teleport(Vector3 position, Quaternion rotation, bool moveToTarget) =>
        _puppetMaster.Teleport(position, rotation, moveToTarget);

    public void Kill() => _puppetMaster.Kill();

    public void SetDeathRagdollSettings()
    {
        _puppetMaster.pinWeight = 1;
        _hipsMuscle.props.mappingWeight = 1;
        _hipsMuscle.props.pinWeight = 1;
        _hipsMuscle.props.muscleWeight = 1;
    }

    private void InitializeExtremities()
    {
        _hipsMuscle = _puppetMaster.muscles[_hipsIndex];
        _leftLegMuscle = _puppetMaster.muscles[_lefLegIndex];
        _rightLegMuscle = _puppetMaster.muscles[_rightLegIndex];
        _leftArmMuscle = _puppetMaster.muscles[_leftArmIndex];
        _rightArmMuscle = _puppetMaster.muscles[_rightArmIndex];
    }
}