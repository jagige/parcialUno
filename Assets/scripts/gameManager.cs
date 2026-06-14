using TMPro;
using Unity.Netcode;
using UnityEngine;

public class gameManager : NetworkBehaviour
{
    public static gameManager Instance;

    // NetworkVariables para sincronizar los puntajes entre servidor y clientes
    public NetworkVariable<int> scorePlayer1 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> scorePlayer2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI txtScorePlayer1;
    [SerializeField] private TextMeshProUGUI txtScorePlayer2;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public override void OnNetworkSpawn()
    {
        // Nos suscribimos al evento de cambio de valor para actualizar la UI
        scorePlayer1.OnValueChanged += OnScoreChanged;
        scorePlayer2.OnValueChanged += OnScoreChanged;

        UpdateUI(0, 0); // Inicializar UI
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
}
