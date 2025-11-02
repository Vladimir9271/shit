using UnityEngine;

public class TPdoors : MonoBehaviour
{
    [SerializeField]public Transform TPpositionPlayer;
    [SerializeField]public Transform TPpositionCamera;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            player.transform.position = TPpositionPlayer.position;
            Camera mainCam = Camera.main;
            Vector3 cameraPos = TPpositionCamera.position;
            cameraPos.z = -5;
            mainCam.transform.position = cameraPos;
        }
    }
}
