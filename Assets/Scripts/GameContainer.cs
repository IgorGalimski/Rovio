using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class GameContainer : MonoBehaviour
{
    [SerializeField] 
    private AssetReference _playerView;

    [SerializeField] 
    private AssetReference _asteroidView;

    [SerializeField] 
    private AssetReference _bulletView;

    public IEnumerator Start()
    {
        var playerViewOperation = Addressables.LoadAssetAsync<GameObject>(_playerView);
        var asteroidViewOperation = Addressables.LoadAssetAsync<GameObject>(_asteroidView);
        var bulletViewOperation = Addressables.LoadAssetAsync<GameObject>(_bulletView);

        yield return playerViewOperation;
        yield return asteroidViewOperation;
        yield return bulletViewOperation;
        
        var lifecycleNotifier = new GameObject().AddComponent<LifecycleNotifier>();
        DontDestroyOnLoad(lifecycleNotifier);
        
        var playerController = new PlayerController();
        playerController.Inject(Instantiate(playerViewOperation.Result.GetComponent<PlayerView>()));

        var asteroidController = new AsteroidController();
        asteroidController.Inject(asteroidViewOperation.Result.GetComponent<AsteroidView>());

        var bulletController = new BulletController();
        bulletController.Inject(bulletViewOperation.Result.GetComponent<BulletView>());
    }
}