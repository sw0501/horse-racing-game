using UnityEngine;

// Screen Space - Overlay 루트 Canvas의 RectTransform.anchoredPosition이
// (구버전 Constant Pixel Size 설정의 잔여값으로) 렌더링 직전마다 되돌아가면서
// 화면 중앙이 어긋나는 문제가 있어, 렌더링 직전 시점(willRenderCanvases)에
// 매번 마지막으로 강제 고정한다.
[RequireComponent(typeof(RectTransform))]
public class CanvasRootPositionFix : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        Canvas.willRenderCanvases += Fix;
    }

    void OnDisable()
    {
        Canvas.willRenderCanvases -= Fix;
    }

    void Fix()
    {
        if (rect.anchoredPosition != Vector2.zero)
        {
            rect.anchoredPosition = Vector2.zero;
        }
    }
}
