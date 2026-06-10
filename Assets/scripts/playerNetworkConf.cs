using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkPlayerConfig : NetworkBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _camera;

    public Material verde;
    public Material rojo;
    public MeshRenderer meshRenderer;
    public MeshRenderer meshRendererR;

    private void Awake()
    {
        //PlayerInput
        _playerInput.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        _playerInput.enabled = IsOwner;
        _camera.SetActive(IsOwner);

        if (!IsOwner) return;

        if (NetworkManager.LocalClientId == 0)
        {
            meshRenderer.material = verde;
        }

        if (NetworkManager.LocalClientId == 1)
        {
            meshRenderer.material = rojo;
            meshRendererR.material = rojo;
        }
    }
   

    public override void OnNetworkDespawn()
    {
        _playerInput.enabled = false;
        _camera.SetActive(false);
    }
}