using System.Collections;
using UnityEngine;

public class Log : MonoBehaviour
{
    public static string TAG = "InvenoLog:";
    public static void Print(string s)
    {
        Debug.Log(TAG + s);
    }
    public static void Error(string s)
    {
        Debug.LogError(TAG + s);
    }
}