using UnityEngine;

public enum ItemType
{
    Equipment,
    Use,
    Etc,
    Projectile,
    Currency
}

[CreateAssetMenu(menuName = "RPG/Items/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private bool stackable;
    [SerializeField]
    private ItemType itemType;
    public Sprite Icon;
    

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public bool Stackable
    {
        get
        {
            return stackable;
        }

        set
        {
            stackable = value;
        }
    }

    public ItemType ItemType
    {
        get
        {
            return itemType;
        }

        set
        {
            itemType = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }
}
