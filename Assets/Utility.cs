using UnityEngine;

public static class Utility
{
    public static TextMesh CreateWorldText(string text, Vector3 localPosition, int fontSize, Color color, Transform parent) 
    {
        GameObject gameObject = new GameObject("Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.parent = parent;
        transform.position = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.color = color;
        textMesh.fontSize = fontSize;
        return textMesh;
    }
}
