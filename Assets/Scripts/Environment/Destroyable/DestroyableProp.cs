using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent((typeof(Collider)))]

public class DestroyableProp : MonoBehaviour, IDestroyable
{
    [BoxGroup("Main Object"), SerializeField] private Collider _mainCollider;
    [BoxGroup("Main Object"), SerializeField] private MeshRenderer _defaultMeshRenderer;
    [BoxGroup("Destroyable Objects"), SerializeField] private MeshRenderer[] _destroyableMeshRenderers;

    private void Awake()
    {
        if (!_mainCollider)
            _mainCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        _mainCollider.enabled = true;
        
        foreach (var meshRenderer in _destroyableMeshRenderers)
            meshRenderer.gameObject.SetActive(true);
    }

    public void Destroy()
    {
        _mainCollider.enabled = false;
        _defaultMeshRenderer.gameObject.SetActive(false);

        foreach (var meshRenderer in _destroyableMeshRenderers)
        {
            meshRenderer.gameObject.AddComponent<Rigidbody>();
            meshRenderer.gameObject.SetActive(true);
        }
    }
}