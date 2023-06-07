using UnityEngine;
public class PlayerController : IPresenter<PlayerView>
{
    private const float ROTATION_SPEED = 150f;
    private const float MOVEMENT_SPEED = 20f;
    
    private readonly PlayerModel playerModel;

    private PlayerView _playerView;

    public PlayerController()
    {
        playerModel = new PlayerModel(ROTATION_SPEED, MOVEMENT_SPEED);
    }
    
    public void Inject(PlayerView view)
    {
        _playerView = view;
        _playerView.OnAsteroidCollision += OnAsteroidCollision;

        MessageSystem.AddListener<MonobehaviourUpdate>(OnUpdate);
    }

    private void OnAsteroidCollision()
    {
        MessageSystem.SendMessage(new GameEnd());
    }

    private void OnUpdate(MonobehaviourUpdate _)
    {
        var deltaTime = Time.deltaTime;

        var rotationInput = Input.GetAxis("Horizontal") * playerModel.RotationSpeed * deltaTime;
        var movementInput = Input.GetAxis("Vertical") * playerModel.MovementSpeed * deltaTime;
        
        var screenPosition = _playerView.transform.position.ToScreen();
        screenPosition.x = Mathf.Clamp(screenPosition.x, 0f, Const.ScreenWight);
        screenPosition.y = Mathf.Clamp(screenPosition.y, 0f, Const.ScreenHeight);
        _playerView.transform.position = screenPosition.ToWorld();
        
        _playerView.Rotate(rotationInput*Vector3.back);
        _playerView.Move(movementInput*Vector3.up);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MessageSystem.SendMessage(new PlayerShot(_playerView.transform.position, _playerView.transform.rotation));
        }
    }
}