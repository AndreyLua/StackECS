using Leopotam.Ecs;
using UnityEngine;

public class ItemConsumersInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private ItemFactory _itemFactory;
    private StackRepository<Item, ItemType> _stackRepository;

    public void Init()
    {
        ItemConsumer[] itemConsumers = GameObject.FindObjectsOfType<ItemConsumer>();

        foreach (ItemConsumer itemConsumer in itemConsumers)
        {
            EcsEntity consumer = _ecsWorld.NewEntity();
            itemConsumer.Init(consumer);

            ModelComponent model = new ModelComponent(itemConsumer.transform);
            StackHolderComponent stackHolder = new StackHolderComponent(itemConsumer.StackParent, itemConsumer.Velocity);
            consumer.Replace(model).Replace(stackHolder);

            Item item = _itemFactory.SpawnItem(model.Transform.position);
            _stackRepository.AddElement(stackHolder, item);
        }
    }
}
