using Leopotam.Ecs;

public class GiveItemEventHandler : IEcsRunSystem
{
    private EcsFilter<GiveItemEvent, StackHolderComponent> _giveItemrFilter;

    private StackRepository<Item, ItemType> _stackRepository;

    public void Run()
    {
        foreach (int i in _giveItemrFilter)
        {
            ref GiveItemEvent giveItemEvent = ref _giveItemrFilter.Get1(i);
            ref StackHolderComponent stackHolder = ref _giveItemrFilter.Get2(i);
            ref EcsEntity entity = ref _giveItemrFilter.GetEntity(i);

            if (_stackRepository.GetElementsCount(stackHolder)>0)
            {
                Item item = _stackRepository.GetLastElement(stackHolder, true);

                StackHolderComponent consumerStackHolder = 
                    giveItemEvent.Consumer.EcsEntity.Get<StackHolderComponent>();

                ItemCollectorExtensions.CollectItem(item, consumerStackHolder, _stackRepository);
                _stackRepository.AddElement(consumerStackHolder, item);
            }
            entity.Del<GiveItemEvent>();
        }
    }
}