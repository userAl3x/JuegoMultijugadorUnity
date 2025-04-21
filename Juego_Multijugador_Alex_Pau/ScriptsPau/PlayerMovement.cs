using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private PhotonView view;
    private Rigidbody2D rb;

    public TMP_Text playerNameText;
    public GameObject playerCamera; // Referencia a la cámara del jugador

    public TMP_Text coinText; // Texto para mostrar las coins
    private int coins = 0; // Contador de monedas

    void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();

        if (view.IsMine)
        {
            view.RPC("SetPlayerName", RpcTarget.AllBuffered, PhotonNetwork.NickName);
            playerCamera.SetActive(true); // Activar la cámara solo para el jugador local
        }
        else
        {
            playerCamera.SetActive(false); // Desactivar la cámara de los otros jugadores
        }
    }

    [PunRPC]
    void SetPlayerName(string name)
    {
        playerNameText.text = name; // Asigna el nombre en la UI encima del jugador
    }
    

    void Update()
{
    if (view.IsMine) // Solo mueve el jugador local
    {   
        float moveX = 0f;
        float moveY = 0f;
        
        view.RPC("UpdateCoinUI", RpcTarget.AllBuffered); // Actualizar la UI de monedas al inicio

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = 1f;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }

        Vector2 movement = new Vector2(moveX, moveY).normalized * speed;
        rb.linearVelocity = movement; // Movimiento correcto en 2D
    }
}

//Metodo para las coins
void OnTriggerEnter2D(Collider2D other)
    {
        if (view.IsMine && other.CompareTag("Coin"))
        {   
            //other.gameObject.SetActive(false); // Desactiva la moneda SOLO para este jugador
            coins ++; // Incrementa el contador de monedas por 2
            UpdateCoinUI();
        }
    }

    void UpdateCoinUI()
    {
        if (view.IsMine && coinText != null)
        {
            coinText.text = "Coins: " + coins;
        }
    }

}
