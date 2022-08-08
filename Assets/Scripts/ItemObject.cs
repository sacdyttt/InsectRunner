using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData MyItem;

    [HideInInspector]
    public int ItemPrice;
    [HideInInspector]
    public GameObject Playermodel;
    public Color PlayerColor;
    // Start is called before the first frame update
    void Start()
    {
        ItemPrice = MyItem.Price;
        Playermodel = MyItem.PlayerModel;
        PlayerColor = MyItem.PlayerColor;
    }

}
