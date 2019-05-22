using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour {

    bool Joom = false;
    float maxzoom = -1.5f;
    float minzoom = -1f;
    Camera cr;
    private void Start()
    {
        cr = GetComponent<Camera>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Joom = true;
        Joomin();
    }
    private void OnTriggerExit(Collider other)
    {
        Joom = false;
        Joomout();
    }

    void Joomin()
    {
        if (!Joom) return;
        Vector3 pos = this.transform.localPosition;
        if (pos.z > minzoom) return;
        pos.z = pos.z + 0.01f;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, pos, 5f);
        cr.fieldOfView = cr.fieldOfView - 0.1f;
        Invoke("Joomin", 0.01f);
    }

    void Joomout()
    {
        if (Joom) return;
        Vector3 pos = this.transform.localPosition;
        if (pos.z < maxzoom) return;
        pos.z = pos.z - 0.01f;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, pos, 2f);
        cr.fieldOfView = cr.fieldOfView + 0.1f;
        Invoke("Joomout", 0.01f);
    }
}