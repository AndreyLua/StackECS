using System;
using UnityEngine;

public interface IStackable
{
    public Vector3 LocalScale { get; }
    public Transform Transform { get; }
    public Pose OriginLocalPose { get; set; }
    public Pose OriginWorldPose { get; set; }
    public bool AnimationBlocked { get; set; }
}

public interface IStackable<TIdentifier> : IStackable
{
    public TIdentifier Identifier { get; }
}
