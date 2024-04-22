using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        int? x = 10;
        int y = x ?? 10;
    }
}
