using Unity.Netcode;
using UnityEngine;

public class regalo : Item
{
    public override string GetItemData()
    {
        return _itemName;
    }

    public override void PickUp()
    {
        if (IsServer)
        {
            NetworkObject.Despawn();
        }
        else
        {
            PickUpServerRpc();
        }
        Debug.Log($"Picked: {_itemName}");
    }

    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    private void PickUpServerRpc()
    {
        NetworkObject.Despawn(true);
    }
}
