using System;
using UnityEngine;

public class PlayerView : MonoBehaviour, IView
{
    public event Action OnAsteroidCollision;
    
    public void Rotate(Vector3 vector)
    {
        transform.Rotate(vector);
    }

    public void Move(Vector3 vector)
    {
        transform.Translate(vector);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(Const.AsteroidTagName))
        {
            OnAsteroidCollision?.Invoke();
        }
    }
}