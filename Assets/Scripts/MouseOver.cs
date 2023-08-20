using UnityEngine;

public class MouseOver : MonoBehaviour
{
    void OnMouseOver()
    {
        AudioManager.Instance.PlaySFX("MouseOver");
    }
}
