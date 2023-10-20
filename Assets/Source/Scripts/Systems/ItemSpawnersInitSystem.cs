using Leopotam.Ecs;
using UnityEngine;

public class ItemSpawnersInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    public void Init()
    {
        ItemSpawner[] itemSpawners = GameObject.FindObjectsOfType<ItemSpawner>();

        foreach (ItemSpawner itemSpawner in itemSpawners)
        {
            EcsEntity spawner = _ecsWorld.NewEntity();
            itemSpawner.Init(spawner);

            ModelComponent model = new ModelComponent(itemSpawner.transform);
         
            spawner.Get<SpawnerTag>();
            spawner.Replace(model);
        }
    }
}