using System.Collections;
using UnityEngine;
using Boxfight2.Server;

namespace Boxfight2.Client.Input
{
    public class InputCatcher : MonoBehaviour
    {
        Vector2 move = Vector2.zero;

        private void Update()
        {
            move.x = UnityEngine.Input.GetAxis("Horizontal");
            move.x = UnityEngine.Input.GetAxis("Vertical");
        }

        public NetworkInputData GetNetworkedInput()
        {
            NetworkInputData data = new NetworkInputData();
            data.XYInput = move;

            return data;
        }
    }
}