using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private Item _item;

    public Item SpawnItem(Vector3 position)
    {
        Item item = Instantiate<Item>(_item);
        item.transform.position = position;
        return item;
    }
}