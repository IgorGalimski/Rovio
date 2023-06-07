using System;
using UnityEngine;

public class AsteroidView : MonoBehaviour, IView
{
    public event Action<GameObject> OnBulletCollision;
    
    public void Move(Vector3 position)
    {
        transform.position += position;
    }
    
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(Const.BulletTagName))
        {
            OnBulletCollision?.Invoke(collision.gameObject);
        }
    }
}