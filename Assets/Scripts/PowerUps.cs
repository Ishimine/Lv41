using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PowerUps : MonoBehaviour {

    public IconoPower icono;
    public ColorControlPlayer colorPlayer;

    public delegate void gatillo();
    public event gatillo power;
    int activePower = -1;
    public float fuerzaSalto = 3;
    Vector2 dir = Vector2.down;

    Rigidbody2D rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TouchControl.instance.dobleTap = PowerTap;
        CambiarPowerUp(0);

    }
    


    public void PowerTap()
    {
        if (power != null)
            power();
        icono.AnimarIcono();
    }

  


    public void CambiarIcono(Sprite iconoN)
    {
        icono.render.sprite = iconoN;
    }

    public void CambiarPowerUp(int i)
    {
        //print(Time.time);
        if (activePower == i)
            return;
        activePower = i;
        switch (activePower)
        {
            case 0:
                power = null;
                if(colorPlayer != null) colorPlayer.CambiarColor(1, Color.white);
                break;
            case 1:
                power = PowerJumpRigido;
                if (colorPlayer != null) colorPlayer.CambiarColor(1, Color.gray);
                break;
            case 2:
                power = PowerGravityChange;
                if (colorPlayer != null) colorPlayer.CambiarColor(1, Color.blue);
                break;
            case 3:
                power = PowerJumpExtra;
                if (colorPlayer != null) colorPlayer.CambiarColor(1, Color.magenta);
                break;
            case 4:
                if (colorPlayer != null) colorPlayer.CambiarColor(1, Color.red);
                break;
            default:
                break;
        }
        icono.AnimarIcono();
    }

    public void CambiarDirGravedad(Vector2 nDir)
    {
        dir = nDir;
    }

    private void PowerJumpRigido()
    {
        rb.velocity = new Vector2(0, fuerzaSalto);
    }

    private void PowerGravityChange()
    {
        DirectorGravedad.CambiarGravedad(dir);
    }

    private void PowerJumpExtra()
    {
        rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
    }


}
