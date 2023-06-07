using UnityEngine;

public static class Extension
{
    public static Vector3 ToWorld(this Vector3 vector)
    {
        return Const.MainCamera.ScreenToWorldPoint(vector);
    }

    public static Vector3 ToScreen(this Vector3 vector)
    {
        return Const.MainCamera.WorldToScreenPoint(vector);
    }
}