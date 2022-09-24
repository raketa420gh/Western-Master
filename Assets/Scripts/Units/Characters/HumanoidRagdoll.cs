using RootMotion.Dynamics;
using Sirenix.OdinInspector;
using UnityEngine;

public class HumanoidRagdoll : MonoBehaviour
{
    [BoxGroup("Components"), SerializeField] private PuppetMaster _puppetMaster;

    public void Teleport(Vector3 position, Quaternion rotation, bool moveToTarget) =>
        _puppetMaster.Teleport(position, rotation, moveToTarget);

    public void Kill() => _puppetMaster.Kill();
    
    public void Rebuild() => _puppetMaster.Rebuild();
}