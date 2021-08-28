using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BilliboardViewer : MonoBehaviour
{
    #region VARIABLE_REG
    private Canvas m_Canvas;

    private Camera m_Cam;
    #endregion

    #region UNITY_REG
    private void Awake()
    {
        m_Canvas = this.GetComponent<Canvas>();
        m_Cam = Camera.main;

        m_Canvas.worldCamera = m_Cam;
    }

    private void Update()
    {
        FaceCamera();
    }
    #endregion

    #region CLASS_REG
    [ContextMenu("Face Camera")]
    private void FaceCamera()
    {
        this.transform.LookAt(this.transform.position + m_Cam.transform.rotation * Vector3.back, m_Cam.transform.rotation * Vector3.down);
    }
    #endregion
}
