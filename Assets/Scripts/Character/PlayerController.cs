using System;
using Cinemachine.Examples;
using UnityEngine;


public class PlayerController : MonoBehaviour, IHealth
{
    [HideInInspector] public GameObject Enemy;
    public ChangeCharacterController ChangeCharacterController;
    public PlayerMove.ForwardMode InputForward;
    public Action<Bullet, Vector3> ShootAction;
    public event Action<float, float> OnHealthChanged;
    public event Action<CharacterPresenter> OnCharacterChanged;
    public bool _isHit;
    public bool _shootInProgress = false;
    public bool _characterIsReady;


    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterPresenter _currentCharacterPresenter;
    [SerializeField] protected float _currentHealth;


    private Vector3 m_currentVleocity;
    private event Action<float> _onHit;
    private float _shootdelay;
    private float _speed;


    public enum ForwardMode
    {
        Camera,
        Player,
        World
    };


    private void Start()
    {
        _onHit += PlayHitAnimation;
        _onHit += CallTakeDamage;
        ShootAction = Shoot;
        OnCharacterChanged += CharacterChanged;
        ChangeCharacter(0);
    }

    private void CharacterChanged(CharacterPresenter obj)
    {
        _currentHealth = _currentCharacterPresenter.GetCurrentHealth();
        OnHealthChanged.Invoke(_currentHealth, obj.GetMaxHealth());
        _speed = _currentCharacterPresenter.Speed;
    }

    public void ChangeCharacter(int id)
    {
        _characterIsReady = false;
        ChangeCharacterController.CharcterIsReady = () =>
        {
            _characterIsReady = true;
            _shootInProgress = false;
        };
        _currentCharacterPresenter = ChangeCharacterController.ChangeCharacter(id);
        OnCharacterChanged?.Invoke(_currentCharacterPresenter);
    }


    public void NextCharacter()
    {
        _characterIsReady = false;
        ChangeCharacterController.CharcterIsReady = () =>
        {
            _characterIsReady = true; 
            _shootInProgress = false;
        };
        _currentCharacterPresenter = ChangeCharacterController.NextCharacter();
        OnCharacterChanged?.Invoke(_currentCharacterPresenter);
    }


    private void CallTakeDamage(float amount)
    {
        _currentCharacterPresenter.TakeDamage(amount);
    }

    void Update()
    {
        Vector3 fwd;
        switch (InputForward)
        {
            case PlayerMove.ForwardMode.Camera:
                fwd = Camera.main.transform.forward;
                break;
            case PlayerMove.ForwardMode.Player:
                fwd = transform.forward;
                break;
            case PlayerMove.ForwardMode.World:
            default:
                fwd = Vector3.forward;
                break;
        }

        Move();
        if (!IsBlocked())
            _currentCharacterPresenter.Animate();
        _shootdelay += Time.deltaTime;
        if (Input.anyKeyDown)
            _isHit = false;
    }


    private bool IsBlocked()
    {
        return _isHit || !_characterIsReady || _shootInProgress;
    }


    public void PlayHitAnimation(float amount)
    {
        _currentCharacterPresenter.PlayHitAnimation();
    }

    public void Move()
    {
        RotateToEnemy();
        if (IsBlocked())
            return;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (input == Vector3.zero)
            return;

        Vector3 direction = Vector3.zero;
        direction = input.z == 0 ? Vector3.zero : (input.z > 0 ? Vector3.forward : Vector3.back);
        direction += input.x == 0 ? Vector3.zero : (input.x > 0 ? Vector3.right : Vector3.left);


        direction = transform.TransformDirection(direction * _speed);
        _characterController.Move(direction * Time.deltaTime);
    }


    public void RotateToEnemy(bool fast = false)
    {
        if (fast)
        {
            Vector3 direction = (Enemy.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            return;
        }


        Vector3 currentDirection = Enemy.transform.position - transform.position;
        currentDirection.y = 0;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(currentDirection),
            0.1f
        );
    }

    public void ShootToTarget()
    {
        if (_shootdelay > _currentCharacterPresenter.DelayBetweenShoot && !IsBlocked())
            if (Input.GetMouseButtonUp(0))
            {
                ShootAction(
                    _currentCharacterPresenter.ProjectilePrefab,
                    _currentCharacterPresenter.bulletShootPos.transform.position);
                _shootdelay = 0;
            }
    }

    public void Shoot(Bullet bulletPrefab, Vector3 shootLocation)
    {
        _shootInProgress = true;
        RotateToEnemy(true);

        _currentCharacterPresenter.ShowCastAnimation(
            () =>
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.Initialize(shootLocation, transform.forward * 10);
                _shootInProgress = false;
                _currentCharacterPresenter.SetAnimationIdle();
            }
            , () => { }
        );
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetMaxHealth()
    {
        return _currentCharacterPresenter.GetMaxHealth();
    }

    public void TakeDamage(float amount)
    {
        _onHit?.Invoke(amount);
        _isHit = true;
        _currentHealth = _currentCharacterPresenter.GetCurrentHealth();
        OnHealthChanged?.Invoke(_currentHealth, _currentCharacterPresenter.GetMaxHealth());
    }
}