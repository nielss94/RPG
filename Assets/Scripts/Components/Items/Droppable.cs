using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Droppable {
    public Item item;
    public bool isCurrency;
    public int chance;
    public int minQuantity;
    public int maxQuantity;
}
