using UnityEngine;

public class PlayerMove : MonoBehaviour, ISystem
{
    private void Update()
    {
        foreach (var entity in ECS.GetEntities<OverworldPlayer, Movement>())
        {
            var player = entity.Item1;
            var movement = entity.Item2;
            var input = new Vector3();
            input.x = Input.GetAxis("Horizontal") * movement.speed * Time.deltaTime;
            input.y = Input.GetAxis("Vertical") * movement.speed * Time.deltaTime;
            player.transform.position += input;
        }
    }
}