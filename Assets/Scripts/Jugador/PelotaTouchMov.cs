using UnityEngine;
using System.Collections;

public class PelotaTouchMov : MonoBehaviour {

	public float fReboteT0 = 5f;
	public float fReboteT1 = 25f;
	public float fReboteT2 = 40f;
	public float fReboteT3 = 60f;
	public float PlataformaTouch = 25f;
	public ForceMode2D modoFuerza;
	Animator anim;
	Animator animMoneda;
	Rigidbody2D rb2D;   
//	GameObject gameControl;
	GameObject Moneda;

	void Awake()
	{
//		gameControl = GameObject.FindGameObjectWithTag("GameControl");
		anim = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
		Moneda = GameObject.FindGameObjectWithTag("MonedaFinal");
		animMoneda = Moneda.GetComponent<Animator>();
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "PlataformaTouch")
		{
			Vector3 contact = other.contacts[0].point;
			Vector3 dif = transform.position - contact;
			rb2D.AddForce(new Vector2 (dif.x * PlataformaTouch, dif.y * PlataformaTouch),modoFuerza);
		}
		else 
		{
			if (other.gameObject.tag == "Terreno/1" || other.gameObject.tag == "Terreno/red")
			{
				Vector3 contact = other.contacts[0].point;
				Vector3 dif = transform.position - contact;
				rb2D.AddForce(new Vector2 (dif.x * fReboteT1, dif.y * fReboteT1),modoFuerza);
			}
			else if (other.gameObject.tag == "Terreno/2")
			{
				Vector3 contact = other.contacts[0].point;
				Vector3 dif = transform.position - contact;
				rb2D.AddForce(new Vector2 (dif.x * fReboteT2, dif.y * fReboteT2),modoFuerza);
			}
			else if (other.gameObject.tag == "Terreno/3")
			{
				Vector3 contact = other.contacts[0].point;
				Vector3 dif = transform.position - contact;
				rb2D.AddForce(new Vector2 (dif.x * fReboteT3, dif.y * fReboteT3),modoFuerza);
			}
			else if (other.gameObject.tag == "Terreno/0")
			{
				Vector3 contact = other.contacts[0].point;
				Vector3 dif = transform.position - contact;
				rb2D.AddForce(new Vector2 (dif.x * fReboteT0, dif.y * fReboteT0),modoFuerza);
			}
		}
		anim.SetTrigger("Rebote");
		animMoneda.SetTrigger("Rebote");
	}

//	public void YouWin ()
//	{
//		gameControl.GetComponent<GameControl>().YouWinScreen();
//	}
}
