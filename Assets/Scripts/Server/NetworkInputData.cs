using Fusion;
using System.Collections;
using UnityEngine;

namespace Boxfight2.Server
{
    public struct NetworkInputData : INetworkInput
    {
        public Vector2 XYInput;
        public float RotationInput;
        public NetworkBool isJump;
    }
}