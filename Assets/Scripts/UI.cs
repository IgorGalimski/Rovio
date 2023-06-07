using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _counter;

    private int _asteroids;
    
    public void Start()
    {
        UpdateCounter();
        
        MessageSystem.AddListener<GameEnd>(OnGameEnd);
        MessageSystem.AddListener<AsteroidDestoyed>(OnAsteroidDestroyed);
    }
    
    private void OnGameEnd(GameEnd _)
    {
        _asteroids = 0;
            
        UpdateCounter();
    }

    private void OnAsteroidDestroyed(AsteroidDestoyed _)
    {
        _asteroids++;

        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _counter.text = _asteroids.ToString();
    }
}