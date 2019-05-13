using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    //Creates an new instance of the item definition class
    public static ItemList itemInfo = new ItemList();

    private void Awake()
    {
        //Converts a json file to a text asset
        TextAsset asset = Resources.Load("Items/ItemInfo") as TextAsset;
        //Checks if the asset is null or not if not it converts the asset to text to a list of items
        if (asset != null) itemInfo = JsonUtility.FromJson<ItemList>(asset.text);
    }

}
