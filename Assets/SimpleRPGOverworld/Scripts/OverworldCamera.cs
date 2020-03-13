using UnityEngine;

namespace SimpleRPGOverworld.Scripts
{
    public class OverworldCamera : MonoBehaviour, IComponentData
    {
        public new Camera camera;
        public float targetZoom = 5;
        public float zoomSpeed = 0.1f;
    }
}
