using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float dano = 25f;
    public float tempoDeVida = 5f;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    void OnCollisionEnter(Collision colisao)
    {
        Inimigo inimigo = colisao.gameObject.GetComponent<Inimigo>();

        if (inimigo != null)
        {
            inimigo.ReceberDano(dano);
        }

        Destroy(gameObject);
    }
}