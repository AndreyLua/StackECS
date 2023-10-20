using Leopotam.Ecs;
using UnityEngine;

public class Player : EntityReference, IStackHolder
{
    [SerializeField] private Transform _stackParent;

    private EcsEntity _ecsEntity;
    private Vector3 _velocity;
    private CharacterController _characterController;

    public Transform StackParent => _stackParent;
    public Vector3 Velocity => _velocity;
    public CharacterController CharacterController => _characterController;

    private void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
    }

    public override void Init(EcsEntity entity)
    {
        _ecsEntity = entity;
    }

    private void OnTriggerStay(Collider other)
    {
        Item item;
        if (other.TryGetComponent<Item>(out item))
        {
            CollectItemEvent collectItemEvent = new CollectItemEvent(item);
            _ecsEntity.Replace(collectItemEvent);
        }

        ItemConsumer consumer;
        if (other.TryGetComponent<ItemConsumer>(out consumer))
        {
            GiveItemEvent giveItemEvent = new GiveItemEvent(consumer);
            _ecsEntity.Replace(giveItemEvent);
        }
    }

}
