using UnityEngine;

public struct PlayerShot
{
    public Vector3 PlayerPosition;

    public Quaternion PlayerRotation;

    public PlayerShot(Vector3 playerPosition, Quaternion playerRotation)
    {
        PlayerPosition = playerPosition;
        PlayerRotation = playerRotation;
    }
}