using Leopotam.Ecs;
using UnityEngine;

public class PlayerSetDirectionSystem : IEcsRunSystem
{
    private EcsFilter<ModelComponent, MoveableComponent, PlayerTag> _movePlayerFilter;

    private JoystickOffcetTransmitter _joystickOffcetTransmitter;

    public void Run()
    {
        foreach (int i in _movePlayerFilter)
        {
            ref ModelComponent model = ref _movePlayerFilter.Get1(i);
            ref MoveableComponent moveable = ref _movePlayerFilter.Get2(i);

            moveable.Velocity = new Vector3(_joystickOffcetTransmitter.JoystickOffcet.x,
                0, _joystickOffcetTransmitter.JoystickOffcet.y) * 5;
        }
    }
}
