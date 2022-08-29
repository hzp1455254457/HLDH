using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SafeArea : MonoBehaviour
{
    RectTransform Pannel;
    Rect LastSafeArea = new Rect(0, 0, 0, 0);
    private void Awake()
    {
        Pannel = GetComponent<RectTransform>();
    }
    void Update()
    {
        Refresh();
    }
    void Refresh()
    {
        Rect safeArea = GetSafeArea();
        if (safeArea != LastSafeArea)
        {
            ApplySafeArea(safeArea);
        }
    }
    Rect GetSafeArea()
    {
        return Screen.safeArea;
    }
    void ApplySafeArea(Rect r)
    {
        LastSafeArea = r; Vector2 anchorMin = r.position; Vector2 anchorMax = r.position + r.size; anchorMin.x /= Screen.width; anchorMin.y /= Screen.height; anchorMax.x /= Screen.width; anchorMax.y /= Screen.height; Pannel.anchorMin = anchorMin; Pannel.anchorMax = anchorMax; Debug.LogFormat("new safe area applied to{0}:X={1},y={2},w={3},h={4},on full extents w={5},h={6}", name, r.x, r.y, r.width, r.height, Screen.width, Screen.height);
    }
}