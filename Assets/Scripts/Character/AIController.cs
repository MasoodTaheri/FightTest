using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour, IHealth
{
    public CharacterPresenter _currentCharacterPresenter;
    public ChangeCharacterController ChangeCharacterController;

    [Header("Targeting & Movement")] 
    [HideInInspector] public Transform Target;
    public float attackRange = 3f;
    public float evadeRange = 1f;
    public float attackDelay = 6f;
    public float rotationSpeed = 720f;
    public float evadeDuration = 1.5f;
    public float lowHealthThreshold = 30f;
    public Bullet ProjectilePrefab;
    public GameObject bulletShootPos;
    public bool _characterIsReady;
    public event Action<CharacterPresenter> OnCharacterChanged;
    public event Action<float, float> OnHealthChanged;

    [SerializeField] private NavMeshAgent m_agent;
    [SerializeField] private string _currentStateName;
    [SerializeField] private float _currentHealth;


    private AIState currentState;
    private float _speed;
    private bool _isHit;
    private event Action<float> _onHit;

    private void Start()
    {
        OnCharacterChanged += CharacterChanged;
        ChangeCharacter(0);
        _onHit += PlayHitAnimation;
        _onHit += CallTakeDamage;
        OnHealthChanged += CheckLowHealthThreshold;
    }

    private void CheckLowHealthThreshold(float obj, float maxHealth)
    {
        if (_currentHealth < lowHealthThreshold)
        {
            int? id = ChangeCharacterController.FindHighestHealth();
            if (id.HasValue)
                ChangeCharacter(id.Value);
        }
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(AIState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        _currentStateName = currentState.StateName;
    }

    public void Move(Vector3 target)
    {
        m_agent.speed = _speed;
        m_agent.destination = target;
    }

    public void Stop()
    {
        m_agent.isStopped = true;
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

    public virtual void Shoot(Bullet bulletPrefab, Vector3 shootLocation)
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.Initialize(shootLocation, transform.forward * 10);
    }

    private void CharacterChanged(CharacterPresenter obj)
    {
        _currentHealth = _currentCharacterPresenter.GetCurrentHealth();
        OnHealthChanged.Invoke(_currentHealth, _currentCharacterPresenter.GetMaxHealth());
        _speed = _currentCharacterPresenter.Speed;
        ChangeState(new IdleState(this));
    }

    public void ChangeCharacter(int id)
    {
        _characterIsReady = false;
        ChangeCharacterController.CharcterIsReady = () => { _characterIsReady = true; };
        _currentCharacterPresenter = ChangeCharacterController.ChangeCharacter(id);
        OnCharacterChanged?.Invoke(_currentCharacterPresenter);
    }


    public void NextCharacter()
    {
        _characterIsReady = false;
        ChangeCharacterController.CharcterIsReady = () => { _characterIsReady = true; };
        _currentCharacterPresenter = ChangeCharacterController.NextCharacter();
        OnCharacterChanged?.Invoke(_currentCharacterPresenter);
    }

    public void PlayHitAnimation(float amount)
    {
        _currentCharacterPresenter.PlayHitAnimation();
    }

    private void CallTakeDamage(float amount)
    {
        _currentCharacterPresenter.TakeDamage(amount);
    }
}