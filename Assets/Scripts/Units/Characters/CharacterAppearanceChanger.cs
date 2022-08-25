using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterAppearanceChanger : MonoBehaviour
{
    [BoxGroup("Meshes"), SerializeField] private List<SkinnedMeshRenderer> _skinnedMeshRenderers = new ();
    [BoxGroup("Materials"), SerializeField] private List<Material> _materials;

    public void SetRandomAppearance()
    {
        foreach (var meshRenderer in _skinnedMeshRenderers)
            meshRenderer.gameObject.SetActive(false);

        var randomMeshRenderer = GetRandomMeshRenderer();

        randomMeshRenderer.material = GetRandomMaterial();
        randomMeshRenderer.gameObject.SetActive(true);
    }

    private SkinnedMeshRenderer GetRandomMeshRenderer()
    {
        var randomIndex = Random.Range(0, _skinnedMeshRenderers.Count - 1);

        return _skinnedMeshRenderers[randomIndex];
    }

    private Material GetRandomMaterial()
    {
        var randomIndex = Random.Range(0, _materials.Count - 1);
        
        return _materials[randomIndex];
    }
}