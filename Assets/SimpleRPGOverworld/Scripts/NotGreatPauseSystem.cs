using UnityEngine;

public class NotGreatPauseSystem : MonoBehaviour, ISystem
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            if (Time.timeScale > 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
    }
}
