using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    private RectTransform m_lightButton;
    void Start()
    {
        m_lightButton = GetComponent<RectTransform>();

        m_lightButton.DOAnchorPosY(174f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
    }
    
    void Update()
    {
        
    }
}
