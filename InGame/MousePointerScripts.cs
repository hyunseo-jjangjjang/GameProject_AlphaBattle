using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MousePointerScripts : MonoBehaviour {
    public Image MousePointerImage;
    public Camera mainCamera;
    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        MousePointerImage.transform.position = Input.mousePosition;
    }
}
