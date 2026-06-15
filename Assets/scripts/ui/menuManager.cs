using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;

public class menuManager : MonoBehaviour
{
    [SerializeField] private GameObject _CanvasInicial;
    [SerializeField] private GameObject _CanvasEsperando;
    [SerializeField] private GameObject _CanvasCliente;
    [SerializeField] private GameObject _CanvasFinal;

    [SerializeField] private TMP_InputField _inputIP;
    [SerializeField] private TextMeshProUGUI _advertencia;

    [SerializeField] private Button _botonServidor;
    [SerializeField] private Button _botonCliente;
    [SerializeField] private Button _botonConectar;

    string numeroIp;

    public void conectarServidor()
    {
        _CanvasInicial.SetActive(false);
        _CanvasEsperando.SetActive(true);
        Time.timeScale = 0f; // ← pause while waiting for client
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId) return;

        _CanvasEsperando.SetActive(false);
        Time.timeScale = 1f; // ← resume when client joins
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
    }
    public void pantallaCliente()
    {
        _CanvasInicial.SetActive(false);
        _CanvasCliente.SetActive(true);
       
    }

    private void OnEnable()
    {
        _botonServidor.onClick.AddListener(conectarServidor);
        _botonCliente.onClick.AddListener(pantallaCliente);
        _botonConectar.onClick.AddListener(conectarCliente);
    }
    private void OnDisable()
    {
        _botonServidor.onClick.RemoveListener(conectarServidor);
        _botonCliente.onClick.RemoveListener(pantallaCliente);
    }

    
    public void ReadStringName(string name)
    {
        if (name.Length <= 0)
        {
            Debug.Log("nombre no válido");
        }
        else
        {
            Debug.Log(name);
        }
        numeroIp = name;
        Debug.Log(numeroIp);
    }

    private void conectarCliente()
    {
       // numIp = numeroIp;
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(numeroIp,7777);
        NetworkManager.Singleton.StartClient();
        _CanvasCliente.SetActive(false);
        Time.timeScale = 1f;
       
    }

}
