using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterPresenter : MonoBehaviour, IHealth
{
    public Bullet ProjectilePrefab;
    public GameObject bulletShootPos;
    public float DelayBetweenShoot;
    public float Speed;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;


    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public void SetAnimationIdle() => _animationController.SinglePlayer(AnimationType.Idle);
    public void SetAnimationWalk() => _animationController.SinglePlayer(AnimationType.Walk);
    public void SetAnimationWalkBackward() => _animationController.SinglePlayer(AnimationType.WalkBackward);
    public void SetAnimationLeft() => _animationController.SinglePlayer(AnimationType.Left);
    public void SetAnimationRight() => _animationController.SinglePlayer(AnimationType.Right);

    public void Animate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (input == Vector3.zero)
            SetAnimationIdle();


        if (Input.GetAxis("Vertical") > 0)
            SetAnimationWalk();
        else if (Input.GetAxis("Vertical") < 0)
            SetAnimationWalkBackward();

        else if (Input.GetAxis("Horizontal") < 0)
            SetAnimationLeft();
        else if (Input.GetAxis("Horizontal") > 0)
            SetAnimationRight();
    }

    public void ShowCastAnimation(Action action, Action onFinished)
    {
        _animationController.SetShootEvent(action, AnimationType.Attack);
        _animationController.SinglePlayer(AnimationType.Attack);
    }

    public void PlayHitAnimation()
    {
        var anims = new List<AnimationType>();
        anims.Add(AnimationType.Hit);
        anims.Add(AnimationType.Idle);
        _animationController.SequencePlayer(anims);
    }


    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    public void InitializeHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }


    private void Start()
    {
        InitializeHealth(_maxHealth);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}