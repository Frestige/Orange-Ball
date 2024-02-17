using UnityEngine;

public class FPSTarget : MonoBehaviour
{
    public int targetFPS = 60;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
