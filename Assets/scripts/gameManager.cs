using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : NetworkBehaviour
{
    public static gameManager Instance;

    // NetworkVariables para sincronizar los puntajes entre servidor y clientes
    public NetworkVariable<int> scorePlayer1 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> scorePlayer2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI txtScorePlayer1;
    [SerializeField] private TextMeshProUGUI txtScorePlayer2;
    //pantalla final
    [SerializeField] private TextMeshProUGUI txtoGanador;
    [SerializeField] private Button _botonVolveraJugar;
    [SerializeField] private Button _botonSalir;
    //reloj
    [SerializeField] float tiempoRestante;
    [SerializeField] private GameObject _CanvasFinal;
    [SerializeField] TextMeshProUGUI tiempoText;
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
       
    }


    void Update()
    {
        tiempoText.text = (tiempoRestante - Time.time).ToString("F0");

        if ((tiempoRestante - Time.time) <= 0)
        {
             gameOver();
           // _CanvasFinal.SetActive(true);
        }
    }


    public override void OnNetworkSpawn()
    {
        // Nos suscribimos al evento de cambio de valor para actualizar la UI
        scorePlayer1.OnValueChanged += OnScoreChanged;
        scorePlayer2.OnValueChanged += OnScoreChanged;

        UpdateUI(0, 0); // Inicializar UI
        tiempoRestante = tiempoRestante + Time.time;
    }

    public override void OnNetworkDespawn()
    {
        scorePlayer1.OnValueChanged -= OnScoreChanged;
        scorePlayer2.OnValueChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int previousValue, int newValue)
    {
        UpdateUI(scorePlayer1.Value, scorePlayer2.Value);
    }

    private void UpdateUI(int p1Score, int p2Score)
    {
        txtScorePlayer1.text = $"Jugador 1: {p1Score}";
        txtScorePlayer2.text = $"Jugador 2: {p2Score}";
    }

    // Método que solo el servidor puede ejecutar para sumar puntos
    public void AddPoint(ulong clientId)
    {
        if (!IsServer) return;

        // Asignamos el punto según el ID del cliente (0 para el Host/J1, 1 para J2, etc.)
        if (clientId == 0)
        {
            scorePlayer1.Value++;
        }
        else
        {
            scorePlayer2.Value++;
        }
    }

    private void gameOver()
    {
        _CanvasFinal.SetActive(true);

        if (scorePlayer1.Value > scorePlayer2.Value)
        {
            txtoGanador.text = "GANÓ EL JUGADOR VERDE";
        }
        else if (scorePlayer2.Value > scorePlayer1.Value)
        {
            txtoGanador.text = "GANÓ EL JUGADOR ROJO";
        }
        else
        {
            txtoGanador.text = "EMPATARON";
        }
    }

    private void OnEnable()
    {
        //_botonVolveraJugar.onClick.AddListener(cargarEscena);
        _botonSalir.onClick.AddListener(Application.Quit);
    }
    private void OnDisable()
    {
       // _botonVolveraJugar.onClick.RemoveListener(cargarEscena);
    }

    /*private void cargarEscena()
    {
        SceneManager.LoadScene("JuegoNavidad");
    }*/
}
