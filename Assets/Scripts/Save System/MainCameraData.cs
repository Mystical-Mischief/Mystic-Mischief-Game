using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveCamera()
    {
        SaveSystem.SaveCamera(this);
    }
        public void LoadCamera ()
    {
        CameraData data = SaveSystem.LoadCamera();
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        Vector3 rotation;
        rotation.x = data.rotation[0];
        rotation.y = data.rotation[1];
        rotation.z = data.rotation[2];
        Quaternion quaternion = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        float smooth = 5.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion,  Time.deltaTime * smooth);
        //staminaBar.GetComponent<StaminaBar>().UpdateStamina(Stamina);
        //healthBar?.GetComponent<HealthBar>().SetHealth(currentHealth);
    }
}
