using System;
using System.Collections.Generic;
using Animancer;

[Serializable]
public class AnimationData
{
    public string Name;
    public List<ClipTransition> clip;
    public AnimationType animationType;
}