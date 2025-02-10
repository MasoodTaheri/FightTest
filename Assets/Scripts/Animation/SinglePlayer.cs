using System;
using System.Collections.Generic;
using Animancer;
using Random = UnityEngine.Random;

public class SinglePlayer : IAnimationPlayer
{
    public event Action OnFinished;
    
    private AnimationController _animationController;
    private AnimancerComponent _animancer;
    private AnimationType _currentAnimationType;
    public SinglePlayer(AnimationController animationController, AnimationType animationType,
        AnimancerComponent animancer)
    {
        _animationController = animationController;
        _currentAnimationType = animationType;
        _animancer = animancer;
        _animancer.Stop();
    }

    private void PlayRandomClip(AnimationType animationType)
    {
        List<ClipTransition> clips = _animationController.animations.Find(x => x.animationType == animationType)?.clip;
        int rnd = Random.Range(0, clips.Count);
        ClipTransition clip = clips[rnd];
        AnimancerState state = _animancer.Play(clip);
        OnAnimationTypeChanged?.Invoke(animationType);
        OnClipChanged?.Invoke(clip);
    }


    public void Play()
    {
        PlayRandomClip(_currentAnimationType);
    }

    public AnimationType GetCurrentAnimationType()
    {
        return _currentAnimationType;
    }

    public event Action<ClipTransition> OnClipChanged;
    public event Action<AnimationType> OnAnimationTypeChanged;
}