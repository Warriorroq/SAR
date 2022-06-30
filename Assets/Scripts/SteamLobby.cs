using UnityEngine;
using Mirror;
using Steamworks;


public class SteamLobby : MonoBehaviour
{
    public static SteamLobby instance;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> joinRequest;
    protected Callback<LobbyEnter_t> lobbyEntered;

    public ulong currentLobbyID;
    private const string hostAddressKey = "HostAddress";
    private CustomNetworkManager manager;

    private void Start()
    {
        if (!SteamManager.Initialized)
        {
            return;
        }

        if (instance == null)
        {
            instance = this;
        }

        manager = GetComponent<CustomNetworkManager>();

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, manager.maxConnections);
    }

    private void OnLobbyCreated(LobbyCreated_t callBack)
    {
        if (callBack.m_eResult != EResult.k_EResultOK)
        {
            return;
        }

        Debug.Log("Lobby Created Succesfully");

        manager.StartHost();

        CSteamID cSteamID = new CSteamID(callBack.m_ulSteamIDLobby);
        string steamID = SteamUser.GetSteamID().ToString();

        SteamMatchmaking.SetLobbyData(cSteamID, hostAddressKey, steamID);
        SteamMatchmaking.SetLobbyData(cSteamID, "name", SteamFriends.GetPersonaName().ToString() + "'s lobby");
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callBack)
    {
        Debug.Log("Request To Join Lobby");
        SteamMatchmaking.JoinLobby(callBack.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callBack)
    {
        currentLobbyID = callBack.m_ulSteamIDLobby;

        if (NetworkServer.active)
        {
            return;
        }

        manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callBack.m_ulSteamIDLobby), hostAddressKey);

        manager.StartClient();
    }
}
