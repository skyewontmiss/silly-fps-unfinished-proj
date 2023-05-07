using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace Boxfight2.Client
{
    public class NetworkedPlayer : NetworkBehaviour, IPlayerLeft
    {
        // Start is called before the first frame update
        public static NetworkedPlayer Local { get; set; }
        void Start()
        {
            gameObject.name = "Networked Player";
        }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                Local = this;
                Debug.Log("[DEBUG] As InputAuthoritarian: New player has been instantiated locally.");
            }
            else
            {
                Camera[] cameras = GetComponentsInChildren<Camera>();
                AudioListener[] listeners = GetComponentsInChildren<AudioListener>();

                foreach (Camera cam in cameras)
                {
                    cam.enabled = false;
                }
                foreach (AudioListener listener in listeners)
                {
                    listener.enabled = false;
                }

                Debug.Log("[DEBUG] As Client: New player has been instantiated remotely.");
            }
        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.InputAuthority)
                Runner.Despawn(Object);
        }
    }
}
