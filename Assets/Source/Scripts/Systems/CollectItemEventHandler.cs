using Leopotam.Ecs;

public class CollectItemEventHandler : IEcsRunSystem
{
    private EcsFilter<CollectItemEvent, StackHolderComponent> _collectItemrFilter;

    private StackRepository<Item, ItemType> _stackRepository;

    public void Run()
    {
        foreach (int i in _collectItemrFilter)
        {
            ref CollectItemEvent collectItemEvent = ref _collectItemrFilter.Get1(i);
            ref StackHolderComponent stackHolder = ref _collectItemrFilter.Get2(i);
            ref EcsEntity entity = ref _collectItemrFilter.GetEntity(i);

            ItemCollectorExtensions.CollectItem(collectItemEvent.Item, stackHolder, _stackRepository);
            _stackRepository.AddElement(stackHolder, collectItemEvent.Item);

            entity.Del<CollectItemEvent>();
        }
    }
}