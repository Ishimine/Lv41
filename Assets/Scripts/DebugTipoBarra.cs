using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTipoBarra : MonoBehaviour
{

    Dropdown drop;
    TouchControl tc;

    private void Start()
    {
        tc = FindObjectOfType<TouchControl>();
        drop = GetComponent<Dropdown>();
    }

    public void SetTipoBarra()
    {
        tc.SetTipoBarra(drop.value);
    }

    public void SetTipoCreacion()
    {
        tc.SetTipoCreacion(drop.value);
    }
}
