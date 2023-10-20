using Leopotam.Ecs;

public class ItemSpawner : EntityReference
{
    private EcsEntity _ecsEntity;

    public override void Init(EcsEntity entity)
    {
        _ecsEntity = entity;
    }
}