using DG.Tweening;
using UnityEngine;


public class ItemCollectorExtensions
{
    public static void CollectItem(Item item, IStackHolder stackHolder, StackRepository<Item,ItemType> stackRepository )
    {
        item.Rigidbody.isKinematic = true;
        item.Rigidbody.useGravity = false;
        item.Collider.enabled = false;

        const float FLY_DURATION = 0.75f;

        item.transform.parent = stackHolder.StackParent;
        item.AnimationBlocked = true;

        Pose pose = stackRepository.CalculateNewItemLocalPose(stackHolder, item);

        item.transform.DOLocalRotateQuaternion(pose.rotation, FLY_DURATION);
        item.transform.DOLocalPath(item.transform.localPosition
            .CreateFlyPath(pose.position, pose.position.y + 3f), FLY_DURATION, PathType.CatmullRom)
            .OnComplete(() =>
            {
                item.AnimationBlocked = false;
            });
    }
}
