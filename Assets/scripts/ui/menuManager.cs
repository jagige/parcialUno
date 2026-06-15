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

        NetworkManager.Singleton.StartHost();

        // Subscribe to the client connected event
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        // Ignore the host's own connection (clientId == 0 is usually the host)
        if (clientId == NetworkManager.Singleton.LocalClientId) return;

        // A real client joined!
        _CanvasEsperando.SetActive(false);
        Time.timeScale = 1f;

        // Unsubscribe so it only fires once (or keep it if you want it per-client)
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
