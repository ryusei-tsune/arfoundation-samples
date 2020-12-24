using System.Collections;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("Assets/Scenes/DataVisualize/image.png");
            Debug.Log("screenshot");
        }
    }
}
