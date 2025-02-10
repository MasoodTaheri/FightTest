using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;
using Random = UnityEngine.Random;

public class SequencePlayer : IAnimationPlayer
{
    public event Action<ClipTransition> OnClipChanged;
    public event Action<AnimationType> OnAnimationTypeChanged;
    public event Action OnFinished;


    private AnimationController _animationController;
    private AnimancerComponent _Animancer;
    private List<AnimationType> _sequenceAnimationTypes;
    private AnimationType _currentAnimationType;



    public SequencePlayer(AnimationController animationController,
        AnimancerComponent Animancer
        , List<AnimationType> animationTypes)
    {
        _animationController = animationController;
        _Animancer = Animancer;
        _sequenceAnimationTypes = animationTypes;
        _Animancer.Stop();
    }

    public void Play()
    {
        _animationController.StartCoroutine(PlayAnimationSequently());
    }

    IEnumerator PlayAnimationSequently()
    {
        foreach (var animtype in _sequenceAnimationTypes)
        {
            List<ClipTransition> clips = _animationController.animations.Find(
                x => x.animationType == animtype)?.clip;
            Debug.Log("SequencePlayer  " + _currentAnimationType);
            if (clips != null)
            {
                _currentAnimationType = animtype;
                int rnd = Random.Range(0, clips.Count);
                //_animationController.clip = clips[rnd];
                Debug.Log("SequencePlayer  " + _currentAnimationType + "->" + clips[rnd]);

                OnAnimationTypeChanged?.Invoke(_currentAnimationType);
                OnClipChanged?.Invoke(clips[rnd]);
                AnimancerState state = _Animancer.Play(clips[rnd]);
                yield return state;
            }
        }

        OnFinished?.Invoke();
        Debug.Log("SequencePlayer play finished");
    }

    public AnimationType GetCurrentAnimationType()
    {
        return _currentAnimationType;
    }
}