using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    //Creates an new instance of the item definition class
    public ItemList itemInfo = new ItemList();

    private void Awake()
    {
        TextAsset asset = Resources.Load("Items/ItemInfo") as TextAsset;
        if (asset != null) itemInfo = JsonUtility.FromJson<ItemList>(asset.text);
    }

}
