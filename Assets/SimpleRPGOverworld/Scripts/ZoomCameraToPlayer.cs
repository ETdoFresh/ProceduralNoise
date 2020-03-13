using UnityEngine;

namespace SimpleRPGOverworld.Scripts
{
    public class ZoomCameraToPlayer : MonoBehaviour, ISystem
    {
        private void Update()
        {
            foreach(var cameraEntity in Ecs.GetEntities<OverworldCamera>())
            foreach(var playerEntity in Ecs.GetEntities<OverworldPlayer>())
            {
                var zoomSpeed = cameraEntity.Item1.zoomSpeed;
                var cameraPosition = cameraEntity.Item1.transform.position;
                var playerPosition = playerEntity.Item1.transform.position;
                var targetPosition = playerPosition;
                targetPosition.z = cameraPosition.z;
                var cameraZoom = cameraEntity.Item1.camera.orthographicSize;
                var targetZoom = cameraEntity.Item1.targetZoom;

                cameraEntity.Item1.transform.position = Vector3.Lerp(cameraPosition, targetPosition, Time.deltaTime * zoomSpeed);
                cameraEntity.Item1.camera.orthographicSize = Mathf.Lerp(cameraZoom, targetZoom, Time.deltaTime * zoomSpeed);
            }
        }
    }
}
