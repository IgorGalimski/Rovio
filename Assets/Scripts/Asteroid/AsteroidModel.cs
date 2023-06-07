using System;
using UnityEngine;
public class AsteroidModel
{
    public Vector3 Direction;

    public Vector3 SpawnPosition;

    public DateTime DeathDate;

    public AsteroidModel(Vector3 spawnPosition, Vector3 direction, DateTime deathDate)
    {
        SpawnPosition = spawnPosition;
        Direction = direction;
        DeathDate = deathDate;
    }
}