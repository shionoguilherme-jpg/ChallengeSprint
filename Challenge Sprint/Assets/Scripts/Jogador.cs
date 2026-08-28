using UnityEngine;

public class Jogador : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;

    [Header("Vida")]
    public float vida = 100f;

    [Header("Tiro")]
    public GameObject projetil;
    public Transform pontoDeDisparo;
    public float forcaDoTiro = 20f;
    public float intervaloEntreTiros = 0.2f;

    private float proximoTiro;

    void Update()
    {
        Mover();
        MirarComMouse();

        if (Input.GetMouseButton(0) && Time.time >= proximoTiro)
        {
            Atirar();
            proximoTiro = Time.time + intervaloEntreTiros;
        }
    }

    void Mover()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(x, 0f, z);

        if (movimento.magnitude > 1f)
            movimento.Normalize();

        transform.position += movimento * velocidade * Time.deltaTime;
    }

    void MirarComMouse()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plano = new Plane(Vector3.up, transform.position);

        float distancia;

        if (plano.Raycast(raio, out distancia))
        {
            Vector3 ponto = raio.GetPoint(distancia);

            Vector3 direcao = ponto - transform.position;

            direcao.y = 0f;

            if (direcao != Vector3.zero)
            {
                transform.rotation =
                    Quaternion.LookRotation(direcao);
            }
        }
    }

    void Atirar()
    {
        if (projetil == null || pontoDeDisparo == null)
            return;

        GameObject bala = Instantiate(
            projetil,
            pontoDeDisparo.position,
            pontoDeDisparo.rotation
        );

        Rigidbody rb = bala.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity =
                pontoDeDisparo.forward * forcaDoTiro;
        }
    }

    public void ReceberDano(float dano)
    {
        vida -= dano;

        Debug.Log("Jogador recebeu " + dano + " de dano.");

        if (vida <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("JOGADOR MORREU!");

        Destroy(gameObject);
    }
}