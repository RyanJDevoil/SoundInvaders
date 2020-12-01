using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


public class Movement : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FreeTrackData
    {
        public int dataid;
        public int camwidth, camheight;
        public Single Yaw, Pitch, Roll, X, Y, Z;
        public Single RawYaw, RawPitch, RawRoll;
        public Single RawX, RawY, RawZ;
        public Single x1, y1, x2, y2, x3, y3, x4, y4;
    }

    [DllImport("FreeTrackClient")]
    public static extern bool FTGetData(ref FreeTrackData data);

    [DllImport("FreeTrackClient")]
    public static extern string FTGetDllVersion();

    [DllImport("FreeTrackClient")]
    public static extern void FTReportID(Int32 name);

    [DllImport("FreeTrackClient")]
    public static extern string FTProvider();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float sensitivity;
    public Transform cameraTransform;
    private float prevYaw = 0;
    void FixedUpdate()
    {
        FreeTrackClientDll.FreeTrackData FreeTrackData;
        FreeTrackData = new FreeTrackClientDll.FreeTrackData();
        if (!FreeTrackClientDll.FTGetData(ref FreeTrackData))
        {
            Debug.Log("FTGetData returned false. FreeTrack likely not working.");
            return;
        }
        FreeTrackClientDll.FTGetData(ref FreeTrackData);

        float Yaw = FreeTrackData.Yaw;
        float rotateHorizontal = Input.GetAxis("Mouse X");
        Vector3 yawVector = new Vector3(0, (Yaw-prevYaw), 0);
        prevYaw = Yaw;
        cameraTransform.Rotate((transform.up * rotateHorizontal * sensitivity)-yawVector*35);
        //cameraTransform.Rotate(-yawVector * 35);
    }
}
