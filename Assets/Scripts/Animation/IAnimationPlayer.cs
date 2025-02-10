using System;
using Animancer;

public interface IAnimationPlayer
{
    public void Play();
    public AnimationType GetCurrentAnimationType();
    public event Action<ClipTransition> OnClipChanged;
    public event Action<AnimationType> OnAnimationTypeChanged;
    public event Action OnFinished;
}