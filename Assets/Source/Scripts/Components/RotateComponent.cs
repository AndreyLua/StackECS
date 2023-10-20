using UnityEngine;

internal struct RotateComponent
{
    public Transform Transform;

    public RotateComponent(Transform transform)
    {
        Transform = transform;
    }
}