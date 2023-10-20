using Leopotam.Ecs;
using UnityEngine;

public abstract class EntityReference : MonoBehaviour
{
    public abstract void Init(EcsEntity entity);
}
