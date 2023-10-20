using UnityEngine;

public class Item : MonoBehaviour, IStackable<ItemType>
{
    [SerializeField] private Vector3 _localScale;

    private ItemType _itemType = ItemType.Cookies;
    private Rigidbody _rigidbody;
    private Collider _collider;

    public Vector3 LocalScale => _localScale;
    public Transform Transform => gameObject.transform;
    public Vector3 OriginPosition { get; set; }
    public ItemType Identifier => _itemType;
    public bool AnimationBlocked { get; set; }


    public Collider Collider => _collider;
    public Rigidbody Rigidbody => _rigidbody;

    public Pose OriginLocalPose { get; set; }
    public Pose OriginWorldPose { get; set; }

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _collider = gameObject.GetComponent<Collider>();
    }

}

public enum ItemType
{
    Cookies
}