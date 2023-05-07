using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using Boxfight2.Utilities;
using Boxfight2.Client;
using Boxfight2.Client.Input;

namespace Boxfight2.Server {
    public class Instancer : MonoBehaviour, INetworkRunnerCallbacks
    {
        [Header("Player Management")]
        [SerializeField] NetworkedPlayer player;

        InputCatcher localInput;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef refer)
        {
            if (runner.IsServer)
            {
                Debug.Log("[DEBUG] As Server: A Player has joined the server. Instantiating a new player.");
                runner.Spawn(player, Utilities.Utilities.GetRandomInstantiatorPoint().position, Quaternion.identity, refer);
            }
            else Debug.Log("[DEBUG] As Client: A Player has joined the match! Haha.");
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (localInput == null && NetworkedPlayer.Local != null)
                localInput = NetworkedPlayer.Local.GetComponent<InputCatcher>();

            if (localInput != null)
                input.Set(localInput.GetNetworkedInput());
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            Debug.Log("[DEBUG] Connected to the server. Have fun in the game! And don't die.");
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            Debug.Log("[ERROR] Failed to connect to the server. What the heck? How rude.");
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
            Debug.Log("[DEBUG] Requested to connect to the server. Someone needs to open the front door fast.");
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
            Debug.Log("[DEBUG] Custom Authentication response recieved. Now get in here!");
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
            Debug.Log("[DEBUG] Disconnected from the server. See you next time!");
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            Debug.Log("[DEBUG] Server host has been migrated to someone else! Maybe it's you?");
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            Debug.Log("[DEBUG] Connected To The Server.");
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            Debug.Log("[DEBUG] Connected To The Server.");
        }


        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("[DEBUG] Connected To The Server.");
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
            Debug.Log("[DEBUG] Connected To The Server.");
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            Debug.Log("[DEBUG] Connected To The Server.");
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
            Debug.Log("[DEBUG] Connected To The Server.");
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            Debug.Log("[DEBUG] Connected To The Server.");
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            Debug.Log("[DEBUG] Connected To The Server.");
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
            Debug.Log("[DEBUG] Connected To The Server.");
        }

    }
}
