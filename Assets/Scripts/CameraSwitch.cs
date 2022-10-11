using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch {

    static List<CinemachineFreeLook> cameras = new List<CinemachineFreeLook>();
    public static CinemachineFreeLook ActiveCamera = null;

    public static bool IsActiveCamera (CinemachineFreeLook cam)
    {
        return cam == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineFreeLook cam)
    {
        cam.Priority = 10;
        ActiveCamera = cam;
        foreach (CinemachineFreeLook c in cameras)
        {
            if(c!=cam && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }
    
    public static void Register(CinemachineFreeLook cam)
    {
        cameras.Add(cam);
    }

    public static void Unregister(CinemachineFreeLook cam)
    {
        cameras.Remove(cam);
    }
}
