using UnityEngine;

public class UIPanel : MonoBehaviour, IUIPanel
{
    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);
}