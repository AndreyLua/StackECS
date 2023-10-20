using Leopotam.Ecs;
using UnityEngine;

public class ItemConsumer : EntityReference, IStackHolder
{
    public Transform StackParent => gameObject.transform;
    public Vector3 Velocity =>Vector3.zero;
    public EcsEntity EcsEntity => _ecsEntity;

    private EcsEntity _ecsEntity;

    public override void Init(EcsEntity entity)
    {
        _ecsEntity = entity;
    }
}
