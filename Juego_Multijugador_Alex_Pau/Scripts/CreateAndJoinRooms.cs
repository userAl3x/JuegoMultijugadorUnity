using UnityEngine;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_InputField nameInput;                                    //Input field para el nombre

    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            nameInput.text = PlayerPrefs.GetString("PlayerName");       //Cargar el nombre si ya existe
        }
    }
    //Guardar el nombre al jugador
    public void SetPlayerName()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            PhotonNetwork.NickName = nameInput.text;                    //Asigna el nombre a Photon
            PlayerPrefs.SetString("PlayerName", nameInput.text);        //Guarda el nombre
        }
    }
    
    //Crear sala
    public void CreateRoom()
    {
        SetPlayerName();                                                //Asegura que el nombre se asigna antes de entrar
        PhotonNetwork.CreateRoom(createInput.text);
    }

    //Unirse a sala
    public void JoinRoom()
    {
        SetPlayerName();
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    //Cargar la escena del juego
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");                         //Carga la escena de la sala
    }
}
