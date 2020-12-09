using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static void Print(string msg)
    {
        Debug.Log(msg);
    }

    public static void Print(object obj)
    {
        Debug.Log(obj);
    }
}