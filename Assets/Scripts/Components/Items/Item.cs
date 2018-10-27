using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Items/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private new string name;
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
}
