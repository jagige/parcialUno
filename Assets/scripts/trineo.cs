using UnityEngine;
using Unity.Netcode;

public class trineo : Item
{
    public override string GetItemData()
    {
        return _itemName;
    }

    public override void PickUp()
    {
        
    }
}
