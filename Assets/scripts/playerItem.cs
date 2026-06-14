using Unity.Netcode;
using UnityEngine;

public class playerItem : NetworkBehaviour
{
    // Variable local para saber si este jugador lleva el objeto
    public bool hasItem = false;

    // Opcional: Referencia visual del objeto que lleva (para activarlo/desactivarlo)
    [SerializeField] private GameObject visualItem;

    public void PickUpItem()
    {
        if (!IsOwner) return; // Solo el dueño del personaje puede recogerlo activamente

        hasItem = true;
        if (visualItem != null) visualItem.SetActive(true);
    }

    public void DeliverItem()
    {
        if (!IsOwner || !hasItem) return;

        hasItem = false;
        if (visualItem != null) visualItem.SetActive(false);

        // Le avisamos al servidor que entregamos el objeto con éxito
        SubmitScoreServerRpc();
    }

    // Un ServerRpc se ejecuta en el servidor, solicitado por el cliente
    [ServerRpc]
    private void SubmitScoreServerRpc(ServerRpcParams rpcParams = default)
    {
        // El servidor sabe qué cliente llamó este método gracias a OwnerClientId
        gameManager.Instance.AddPoint(OwnerClientId);
    }
}
