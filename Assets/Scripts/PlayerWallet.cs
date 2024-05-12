using UnityEngine;

public static class PlayerWaller
{
    public static Resource Stars = new();

    public static void AddStart(int value) 
    {
        Stars.AddAmount(value);
        Debug.Log(Stars.Amount);
    }
}
