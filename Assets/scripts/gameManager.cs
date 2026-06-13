using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI puntosP1Text;
    [SerializeField] TextMeshProUGUI puntosP2Text;
    [SerializeField] TextMeshProUGUI tiempoText;
    [SerializeField] float tiempoRestante;
    [SerializeField] private GameObject _CanvasFinal;
    void Start()
    {
        tiempoRestante = tiempoRestante + Time.time;
    }

    
    void Update()
    {
        tiempoText.text = (tiempoRestante - Time.time).ToString("F0");

        if ((tiempoRestante - Time.time) <= 0)
        {
            _CanvasFinal.SetActive(true);
        }
    }
}
