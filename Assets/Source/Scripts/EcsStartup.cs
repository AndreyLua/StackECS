using Cinemachine;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {

        [SerializeField] private Player _player;
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private JoystickOffcetTransmitter _joystickOffcetTransmitter;
        [SerializeField] private ItemFactory _itemFactory;

        private StackRepository<Item, ItemType> _stackRepository;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start () 
        {
            _stackRepository = _stackRepository = new StackRepository<Item, ItemType>();
            _stackRepository.Init();

            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                .Add (new PlayerInitSystem())
                .Add(new ItemSpawnersInitSystem())
                .Add(new ItemConsumersInitSystem())
                .Add (new PlayerSetDirectionSystem())
                .Add (new MoveSystem())
                .Add (new RotateToDirectonSystem())
                .Add (new CollectItemEventHandler())
                .Add (new GiveItemEventHandler())
                .Add (new StartSpawnerItemSystem())

                .Inject(_stackRepository)
                .Inject(_playerCamera)
                .Inject (_joystickOffcetTransmitter)
                .Inject (_player)
                .Inject(_itemFactory)

                .Init ();
        }

        private void Update ()
        {
            _stackRepository.TickAnimation(Time.deltaTime);
            _systems?.Run ();
        }

        private void OnDestroy ()
        {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}