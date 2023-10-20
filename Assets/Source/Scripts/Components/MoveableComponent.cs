using UnityEngine;

internal struct MoveableComponent
{
    public Vector3 Velocity;

    public MoveableComponent(Vector3 velocity)
    {
        Velocity = velocity;
    }
}

internal struct CollectItemEvent
{
    public Item Item;

    public CollectItemEvent(Item item)
    {
        Item = item;
    }
}

internal struct GiveItemEvent
{
    public ItemConsumer Consumer;

    public GiveItemEvent(ItemConsumer consumer)
    {
        Consumer = consumer;
    }
}