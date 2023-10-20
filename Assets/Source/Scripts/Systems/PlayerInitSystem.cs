using Cinemachine;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private Player _player;
    private CinemachineVirtualCamera _camera;

    public void Init()
    {
        EcsEntity playerEntity = _ecsWorld.NewEntity();

        Player player = Object.Instantiate(_player, Vector3.zero, Quaternion.identity);

        _camera.Follow = player.transform;
        _camera.LookAt = player.transform;

        player.Init(playerEntity);

        ModelComponent model = new ModelComponent(player.CharacterController.transform);
        MoveableComponent moveable = new MoveableComponent(Vector3.zero);
        CharacterComponent characterComponent = new CharacterComponent(player.CharacterController);
        RotateComponent rotate = new RotateComponent(player.transform);
        StackHolderComponent stackHolder = new StackHolderComponent(player.StackParent, moveable.Velocity);

        playerEntity.Get<PlayerTag>();
        playerEntity.Replace(model).Replace(moveable)
            .Replace(characterComponent).Replace(rotate).Replace(stackHolder);
    }
}
