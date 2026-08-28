using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 3.5f;
    public float distanciaParaParar = 2f;

    [Header("Vida")]
    public float vida = 100f;

    [Header("Ataque")]
    public float dano = 10f;
    public float intervaloDeAtaque = 1f;

    private float proximoAtaque;
    private NavMeshAgent agente;
    private Transform jogador;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        // Procura o jogador automaticamente
        GameObject objetoJogador = GameObject.FindGameObjectWithTag("Player");

        if (objetoJogador != null)
        {
            jogador = objetoJogador.transform;
        }
        else
        {
            Debug.LogError("Não encontrei o Jogador! Coloque a Tag 'Player' no jogador.");
        }

        agente.speed = velocidade;
        agente.stoppingDistance = distanciaParaParar;
    }

    void Update()
    {
        if (jogador == null)
            return;

        if (!agente.isOnNavMesh)
            return;

        float distancia = Vector3.Distance(
            transform.position,
            jogador.position
        );

        if (distancia > distanciaParaParar)
        {
            agente.isStopped = false;
            agente.SetDestination(jogador.position);
        }
        else
        {
            agente.isStopped = true;
            Atacar();
        }
    }

    public void ReceberDano(float dano)
    {
        vida -= dano;

        Debug.Log("Inimigo recebeu " + dano + " de dano.");

        if (vida <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Destroy(gameObject);
    }

    void Atacar()
    {
        if (Time.time < proximoAtaque)
            return;

        proximoAtaque = Time.time + intervaloDeAtaque;

        Jogador jogadorScript = jogador.GetComponent<Jogador>();

        if (jogadorScript != null)
        {
            jogadorScript.ReceberDano(dano);
        }
    }
}