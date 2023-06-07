using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class AsteroidController : IPresenter<AsteroidView>
{
    private const uint MAX_ASTEROIDS = 10;
    private const int LIFETIME_SECONDS = 8;

    private const float DIRECTION_SPEED_MIN = 0.005f;
    private const float DIRECTION_SPEED_MAX = 0.025f;

    private const float DIRECTION_DELTA = 0.5f;

    private const float SCREEN_BOUND = 1.5f;

    private AsteroidView _asteroidView;
    private Dictionary<AsteroidView, AsteroidModel> _asteroids;

    private AsteroidDestoyed _asteroidDestoyed;

    private float _screenHeight;
    private float _screenWidth;

    public AsteroidController()
    { 
        _screenHeight = Const.MainCamera.orthographicSize;
        _screenWidth = _screenHeight * Const.MainCamera.aspect;
    }

    public void Inject(AsteroidView view)
    {
        _asteroidView = view;
        
        CreateAsteroids();

        MessageSystem.AddListener<MonobehaviourUpdate>(OnUpdate);
    }

    private void CreateAsteroids()
    {
        _asteroids = new Dictionary<AsteroidView, AsteroidModel>();
        for (var i = 0; i < MAX_ASTEROIDS; i++)
        {
            var asteroidView = Object.Instantiate(_asteroidView.gameObject).GetComponent<AsteroidView>();
            asteroidView.OnBulletCollision += bullet =>
            {
                MessageSystem.SendMessage(_asteroidDestoyed);
        
                Object.Destroy(bullet);

                UpdateAsteroid(asteroidView);
            };

            UpdateAsteroid(asteroidView);
        }
    }

    private void OnUpdate(MonobehaviourUpdate _)
    {
        for (var i = 0; i < _asteroids.Count; i++)
        {
            var asteroid = _asteroids.ElementAt(i);
            
            asteroid.Key.Move(asteroid.Value.Direction);

            if (asteroid.Value.DeathDate < DateTime.Now)
            {
                UpdateAsteroid(asteroid.Key);
            }
        }
    }

    private void UpdateAsteroid(AsteroidView asteroidView)
    {
        _asteroids[asteroidView] = CreateNewModel();
            
        asteroidView.transform.position = _asteroids[asteroidView].SpawnPosition;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        var spawnX = Random.Range(_screenWidth, _screenWidth * SCREEN_BOUND);
        var spawnY = Random.Range(_screenHeight, _screenHeight * SCREEN_BOUND);

        var corner = Random.Range(0, 4);
        switch (corner)
        {
            case 0:
                return new Vector3(-spawnX, spawnY, 0f);
            case 1:
                return new Vector3(spawnX, spawnY, 0f);
            case 2:
                return new Vector3(-spawnX, -spawnY, 0f);
            case 3:
                return new Vector3(spawnX, -spawnY, 0f);
            default:
                return Vector3.zero;
        }
    }

    private AsteroidModel CreateNewModel()
    {
        var spawnPosition = GetRandomSpawnPosition();

        return new AsteroidModel(spawnPosition, GetDirection(spawnPosition), GetDeathDate());
    }

    private Vector3 GetDirection(Vector3 position)
    {
        var normalized = -position.normalized;
        var newVector = new Vector3(Random.Range(normalized.x - DIRECTION_DELTA, normalized.x + DIRECTION_DELTA), Random.Range(normalized.y - DIRECTION_DELTA, normalized.y + DIRECTION_DELTA));

        return Random.Range(DIRECTION_SPEED_MIN, DIRECTION_SPEED_MAX) * newVector;
    }

    private DateTime GetDeathDate()
    {
        return DateTime.Now.AddSeconds(LIFETIME_SECONDS);
    }
}
