using UnityEngine;

public interface IStackHolder
{
    public Transform StackParent { get; }
    public Vector3 Velocity { get; }
}
