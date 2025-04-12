using UnityEngine;

// initializes Cameras for a split-screen view
public class CamManager : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] Camera mapCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCam.rect = new Rect(0, 0, 0.5f, 1f);
        mapCam.rect = new Rect(0.5f, 0, 0.5f, 1f);
    }


}
