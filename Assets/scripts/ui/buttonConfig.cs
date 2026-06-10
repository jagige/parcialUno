using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class buttonConfig : MonoBehaviour
{
    [SerializeField] private Button _botonServidor;
    [SerializeField] private GameObject CanvasInicial
        ;
    public void conectarCliente()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void conectarServidor()
    {
        CanvasInicial.SetActive(false);
        NetworkManager.Singleton.StartHost();
    }

    private void OnEnable()
    {
        _botonServidor.onClick.AddListener(conectarServidor);
    }
    private void OnDisable()
    {
        _botonServidor.onClick.RemoveListener(conectarServidor);
    }

}
