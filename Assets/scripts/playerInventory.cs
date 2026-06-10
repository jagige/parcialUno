using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<string> _inventory = new List<string>();
    private int tieneRegalo;
    [SerializeField] private  GameObject duende2;
    [SerializeField] private GameObject duendeConRegalo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Item>(out var item))
        {
            _inventory.Add(item.GetItemData());
            item.PickUp();
            tieneRegalo = tieneRegalo + 1;
        }
        
    }

    private void Update()
    {
        if (tieneRegalo == 1)
        {
            duende2.SetActive(false);
            duendeConRegalo.SetActive(true);
        }

        
    }
}