using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class BulletController : IPresenter<BulletView>
{
    private const float SPEED = 10f;
    
    private static readonly TimeSpan destroyDelay = TimeSpan.FromSeconds(5f);

    private BulletView _bulletView;

    public void Inject(BulletView view)
    {
        _bulletView = view;

        MessageSystem.AddListener<PlayerShot>(OnPlayerShot);
    }

    private void OnPlayerShot(PlayerShot playerShot)
    {
        _ = ShootBulletAsync(playerShot);
    }

    private async Task ShootBulletAsync(PlayerShot playerShot)
    {
        var bulletView = Object.Instantiate(_bulletView.gameObject).GetComponent<BulletView>();
        bulletView.transform.position = playerShot.PlayerPosition;
        
        var rotationVector = playerShot.PlayerRotation.eulerAngles;
        var rotationQuaternion = Quaternion.Euler(rotationVector);
        var translatedVector = rotationQuaternion * Vector3.up;

        bulletView.TranslateVector = translatedVector*SPEED;
        
        await Task.Delay(destroyDelay);
        
        Object.Destroy(bulletView.gameObject);
    }
}