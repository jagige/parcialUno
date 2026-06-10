using UnityEngine;

public class stringInputField : MonoBehaviour
{
    string stringName;
    string numeroIp;
    public void ReadStringName (string name)
    {
        if (name.Length <= 0)
        {
            Debug.Log("nombre no válido");
        }
        else
        {
            Debug.Log(name);
        }
        stringName = name;
        Debug.Log(stringName);
    }

   /* public void caractrerIp(string ip)
    {
        if (ip.Length <= 0)
        {
            Debug.Log("ip no válida");
        }
        else if(ip.Any(char.IsDigit))
        {

        }
        else
        {
            Debug.Log(ip);
        }
        numeroIp = ip;
        Debug.Log(numeroIp);
    }*/

}
