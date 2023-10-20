using Leopotam.Ecs;
using UnityEngine;

public class RotateToDirectonSystem : IEcsRunSystem
{
    private EcsFilter<ModelComponent, MoveableComponent, RotateComponent> _rotateFilter;
  
    public void Run()
    {
        foreach (int i in _rotateFilter)
        {
            ref ModelComponent model = ref _rotateFilter.Get1(i);
            ref MoveableComponent moveable = ref _rotateFilter.Get2(i);
            ref RotateComponent rotate = ref _rotateFilter.Get3(i);

            if (moveable.Velocity != Vector3.zero)
                rotate.Transform.rotation = Quaternion.LookRotation(moveable.Velocity);
        }
    }
}