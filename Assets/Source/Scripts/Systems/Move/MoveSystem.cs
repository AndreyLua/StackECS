using Leopotam.Ecs;
using UnityEngine;

public class MoveSystem : IEcsRunSystem
{
    private EcsFilter<ModelComponent, MoveableComponent>.Exclude<CharacterComponent> _moveFilter;
    private EcsFilter<ModelComponent, MoveableComponent, CharacterComponent> _moveCharacterFilter;

    public void Run()
    {
        foreach (int i in _moveFilter)
        {
            ref ModelComponent model = ref _moveFilter.Get1(i);
            ref MoveableComponent moveable = ref _moveFilter.Get2(i);

            model.Transform.position += (Vector3)moveable.Velocity * Time.deltaTime;
        }

        foreach (int i in _moveCharacterFilter)
        {
            ref ModelComponent model = ref _moveCharacterFilter.Get1(i);
            ref MoveableComponent moveable = ref _moveCharacterFilter.Get2(i);
            ref CharacterComponent character = ref _moveCharacterFilter.Get3(i);

            character.CharacterController.Move(moveable.Velocity * Time.deltaTime);
        }
    }
}
