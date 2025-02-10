using System;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public enum AnimationType
{
    Idle = 0,
    Run = 1,
    Walk = 2,
    Attack,
    WalkBackward,
    None,
    Hit,
    Left,
    Right
}

public class AnimationController : MonoBehaviour
{
    public List<AnimationData> animations = new List<AnimationData>();
    public AnimationType CurrentanimationType;
    public ClipTransition clip;
    
    [SerializeField] private AnimancerComponent _Animancer;
    
    private IAnimationPlayer player;
    private static readonly StringReference AttackEventName = "Attack";
    void Start()
    {
        SinglePlayer(AnimationType.Idle);
    }

    public void SequencePlayer(List<AnimationType> animationTypes, Action onFinished = null)
    {
        player = new SequencePlayer(this, _Animancer, animationTypes);
        player.OnClipChanged += transition => clip = transition;
        player.OnAnimationTypeChanged += animType => CurrentanimationType = animType;
        player.OnFinished += onFinished;
        player.Play();
    }

    public void SinglePlayer(AnimationType animationType)
    {
        if ((player != null) && (player.GetCurrentAnimationType() == animationType))
            return;
        player = new SinglePlayer(this, animationType, _Animancer);
        player.OnClipChanged += transition => clip = transition;
        player.OnAnimationTypeChanged += animType => CurrentanimationType = animType;
        player.Play();
    }


    public void SetShootEvent(Action action,AnimationType animationType)
    {
        animations.Find((x=>x.animationType==animationType))
            .clip[0].Events.SetCallback(AttackEventName,action);
    }
}