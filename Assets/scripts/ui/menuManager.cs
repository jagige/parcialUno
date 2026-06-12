using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

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

    public void conectarServidor()
       {
        _CanvasInicial.SetActive(false);
        //_CanvasEsperando.SetActive(true);
        
        NetworkManager.Singleton.StartHost();
               }
    public void conectarCliente()
    {
        _CanvasInicial.SetActive(false);
        NetworkManager.Singleton.StartClient();
    }

    private void OnEnable()
    {
        _botonServidor.onClick.AddListener(conectarServidor);
        _botonCliente.onClick.AddListener(conectarCliente);
    }
    private void OnDisable()
    {
        _botonServidor.onClick.RemoveListener(conectarServidor);
        _botonCliente.onClick.RemoveListener(conectarCliente);
    }
}
