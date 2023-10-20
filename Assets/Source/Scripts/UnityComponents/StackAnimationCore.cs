using System.Collections.Generic;
using UnityEngine;

public class StackAnimationCore
{
    private IStackHolder _stackHolder;
    private IReadOnlyList<IStackable> _elements;

    private Vector3 _lastTickVelocity;
    private float _lastTickAcceleration;
    private float _lastTickAngularSpeedY;

    public StackAnimationCore(IStackHolder stackHolder, IReadOnlyList<IStackable> elements)
    {
        _stackHolder = stackHolder;
        _elements = elements;
    }

    public void TickAnimation(float frameTime)
    {
        Vector3 velocity = Vector3.Slerp(_lastTickVelocity, _stackHolder.Velocity, frameTime * 5f);

        float angularSpeedY = Mathf.Lerp(_lastTickAngularSpeedY,
            Mathf.Clamp(Vector3.SignedAngle(_lastTickVelocity, velocity, Vector3.up), -110f, 110f), frameTime * 5f);

        float acceleration = Mathf.Lerp(_lastTickAcceleration, velocity.magnitude - _lastTickVelocity.magnitude, frameTime * 2f);

        _lastTickVelocity = velocity;
        _lastTickAcceleration = acceleration;
        _lastTickAngularSpeedY = angularSpeedY;

        for (int i = 1; i < _elements.Count; i++)
        {
            _elements[i].OriginLocalPose = CalculateExistElementLocalPose(i - 1, _elements[i], frameTime);
            _elements[i].OriginWorldPose =
                new Pose(Vector3.zero, _elements[i].Transform.GetGlobalRotation(_elements[i].OriginLocalPose.rotation));

            if (_elements[i].AnimationBlocked)
                continue;

            _elements[i].Transform.localRotation = _elements[i].OriginLocalPose.rotation;
            _elements[i].Transform.localPosition = _elements[i].OriginLocalPose.position;
        }
    }

    public Pose CalculateNewElementWorldPose(IStackable element)
    {
        if (_elements.Count is 0)
            return TransformExtensions.LocalToWorldPose(new Pose(Vector3.zero, _stackHolder.StackParent.localRotation), _stackHolder.StackParent);

        return TransformExtensions.LocalToWorldPose(CalculateNewElementLocalPose(element), _stackHolder.StackParent);
    }

    public Pose CalculateNewElementLocalPose(IStackable element)
    {
        if (_elements.Count is 0)
            return new Pose(Vector3.zero, _stackHolder.StackParent.localRotation);

        return CalculateExistElementLocalPose(_elements.Count - 1, element);
    }

    public Pose CalculateLastExistElementLocalPose(IStackable element)
    {
        if (_elements.Count is 1)
            return new Pose(Vector3.zero, _stackHolder.StackParent.localRotation);

        return CalculateExistElementLocalPose(_elements.Count - 1, element);
    }

    private Pose CalculateExistElementLocalPose(int lastElementIndex, IStackable currentElement, float? frameTime = null)
    {
        IStackable lastElement = _elements[lastElementIndex];

        Vector3 currentElementLocalEuler = Vector3.zero;

        if (frameTime.HasValue)
            currentElementLocalEuler = currentElement.OriginLocalPose.rotation.eulerAngles;

        float rotationX = Mathf.LerpAngle(currentElementLocalEuler.x,
            CalculateTargetLocalRotationX(lastElementIndex + 1, _lastTickAcceleration),
            frameTime.HasValue ? frameTime.Value * 5f : 1f);

        float rotationZ = Mathf.LerpAngle(currentElementLocalEuler.z,
            CalculateTargetLocalRotationZ(lastElementIndex + 1, _lastTickAngularSpeedY),
            frameTime.HasValue ? frameTime.Value * 1f : 1f);

        float deltaRotationX = Mathf.Abs(rotationX - lastElement.OriginLocalPose.rotation.eulerAngles.x);
        float deltaRotationZ = Mathf.Abs(rotationZ - lastElement.OriginLocalPose.rotation.eulerAngles.z);

        Vector3 up = lastElement.OriginWorldPose.rotation * Vector3.up;

        Vector3 localPosition = lastElement.OriginLocalPose.position +
            _stackHolder.StackParent.InverseTransformDirection(up) * (lastElement.LocalScale.y
            + (currentElement.LocalScale.z / 2f) * Mathf.Tan(Mathf.Max(deltaRotationX, deltaRotationZ) * Mathf.Deg2Rad));

        return new Pose(localPosition, Quaternion.Euler(Vector3.right * rotationX + Vector3.forward * rotationZ));
    }

    private float CalculateTargetLocalRotationX(int elementIndex, float acceleration)
    {
        return -acceleration * elementIndex * 60f * (4f / _elements.Count);
    }

    private float CalculateTargetLocalRotationZ(int elementIndex, float angularSpeed)
    {
        return elementIndex * angularSpeed * 1f * (4f / _elements.Count);
    }
}

