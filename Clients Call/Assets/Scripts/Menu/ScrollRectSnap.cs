using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectSnap : MonoBehaviour {
    [SerializeField] private float _animTime;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private RectTransform _viewportTransform;
    [SerializeField] private bool _isHorizontal;


    private RectTransform _content;
    private ScrollRect _sr;

    private void Awake() {
        _sr = GetComponent<ScrollRect>();
        _content = _sr.content;
    }

    public void Update() {
        if (_eventSystem.currentSelectedGameObject != null) {
            if (_eventSystem.currentSelectedGameObject.transform.IsChildOf(_viewportTransform.gameObject.transform)) {
                CenterOnItem(_eventSystem.currentSelectedGameObject.GetComponent<RectTransform>());
            }
        }
    }

    public void CenterOnItem(RectTransform target) {
        //this is the center point of the visible area
        var maskHalfSize = _viewportTransform.rect.size * 0.5f;
        var contentSize = _content.rect.size;

        //get object position inside content
        var targetRelativePosition =
            _content.InverseTransformPoint(target.position);

        //adjust for item size
        targetRelativePosition += new Vector3(target.rect.size.x, target.rect.size.y, 0f) * 0.25f;

        //get the normalized position inside content
        var normalizedPosition = new Vector2(
            Mathf.Clamp01(targetRelativePosition.x / (contentSize.x - maskHalfSize.x)),
            1f - Mathf.Clamp01(targetRelativePosition.y / -(contentSize.y - maskHalfSize.y))
            );

        //we want the position to be at the middle of the visible area
        //so get the normalized center offset based on the visible area width and height
        var normalizedOffsetPosition = new Vector2(maskHalfSize.x / contentSize.x, maskHalfSize.y / contentSize.y);

        //and apply it
        if (_isHorizontal) {
            normalizedPosition.x -= (1f - normalizedPosition.x) * normalizedOffsetPosition.x;
            normalizedPosition.y += normalizedPosition.y * normalizedOffsetPosition.y;

            normalizedPosition.x = Mathf.Clamp01(normalizedPosition.x);
            normalizedPosition.y = Mathf.Clamp01(normalizedPosition.y);
        } else {
            normalizedPosition.y += normalizedPosition.y * normalizedOffsetPosition.y;

            normalizedPosition.x = 0;
            normalizedPosition.y = Mathf.Clamp01(normalizedPosition.y);
        }
        
        _sr.normalizedPosition = normalizedPosition;
    }

    private float TranslateRange(float pValue) {
        return (pValue + ((1f - pValue) * -1f));
    }
}
