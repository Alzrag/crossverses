using UnityEngine;


public class CameraControllerFPS : MonoBehaviour
{

    public Transform player;
    public Transform Camera;
    public float sensitivity = 20f;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible=false;
    }

    // Update is called once per frame
    void Update()
    {
        player.Rotate(0,Input.GetAxisRaw("Mouse X")*sensitivity*Time.deltaTime, 0);
        Camera.Rotate(-1*Input.GetAxisRaw("Mouse Y")*sensitivity*Time.deltaTime,0,0);
    }
}
