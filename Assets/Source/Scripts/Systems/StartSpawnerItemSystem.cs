using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

public class StartSpawnerItemSystem : IEcsRunSystem
{
    private EcsFilter<SpawnerTag, ModelComponent>.Exclude<StartedTag> _spawnerFilter;

    private ItemFactory _itemFactory;

    public void Run()
    {
        foreach (int i in _spawnerFilter)
        {
            ref SpawnerTag spawner = ref _spawnerFilter.Get1(i);
            ref ModelComponent model = ref _spawnerFilter.Get2(i);

            ref EcsEntity entity = ref _spawnerFilter.GetEntity(i);

            Vector3 spawnPosition = model.Transform.position;

            if (spawner.SequenceSpawner == null)
            {
                spawner.SequenceSpawner = DOTween.Sequence().AppendInterval(5).AppendCallback(() =>
                {
                    _itemFactory.SpawnItem(spawnPosition);
                }).SetLoops(-1);
            }

            entity.Get<StartedTag>();
        }
    }
}