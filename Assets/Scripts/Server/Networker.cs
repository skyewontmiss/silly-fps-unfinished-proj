using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using Boxfight2.Client;

namespace Boxfight2.Server
{
    public class Networker : MonoBehaviour
    {
        [Header("Runner")]
        [SerializeField] NetworkRunner RunnerPrefab;
        NetworkRunner Runner;
        // Start is called before the first frame update
        void Start()
        {
            Runner = Instantiate(RunnerPrefab);
            Runner.name = "Boxfight 2 Multiplayer NetworkRunner";
            var ClientTask = InitializeNetworkRunner(Runner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
            Debug.Log("[DEBUG] BX2 Runner started hosting a GAME.");
        }

        protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode mode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
        {
            var SceneManaged = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
            if (SceneManaged == null)
                SceneManaged = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();

            runner.ProvideInput = true;

            return runner.StartGame(new StartGameArgs
            {
                GameMode = mode,
                Address = address,
                SessionName = "Fuck Around And Find Out",
                Initialized = initialized,
                SceneManager = SceneManaged

            });

        }

    }
}
