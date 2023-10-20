using UnityEngine;

internal struct StackHolderComponent : IStackHolder
{
    private Transform _stackParent;
    private Vector3 _velocity;

    public Transform StackParent => _stackParent;
    public Vector3 Velocity => _velocity;

    public StackHolderComponent(Transform stackParent, Vector3 velocity)
    {
        _stackParent = stackParent;
        _velocity = velocity;
    }
}

