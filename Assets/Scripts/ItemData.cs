using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slot", menuName = "Make Item")]
public class ItemData : ScriptableObject
{
    public int Price;
    public GameObject PlayerModel;
    public Color PlayerColor;
}
