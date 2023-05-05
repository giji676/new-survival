using UnityEngine;
using UnityEngine.UI;

public class RebuildUI : MonoBehaviour {
    public Canvas canvas;

    void Start()
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            RectTransform childRectTransform = canvas.transform.GetChild(i).GetComponent<RectTransform>();

            LayoutRebuilder.MarkLayoutForRebuild(childRectTransform);
        }
    }
}