using UnityEngine;
public class BulletView : MonoBehaviour, IView
{
    public Vector3 TranslateVector;
    
    private void Update()
    {
        transform.Translate(TranslateVector * Time.deltaTime);
    }
}