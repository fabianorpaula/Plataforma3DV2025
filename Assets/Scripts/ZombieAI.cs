using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ZombieAI : MonoBehaviour
{
    private Animator animator;
    private Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public ZombieState currentState;
    public GameObject personagem;
    public GameObject meuAtaque;
    public int hp = 25;
    public bool zumbiVivo = true;

    public List<AudioClip> sonsZumbi;


    void Start()
    {
        animator = GetComponent<Animator>();
        personagem = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (zumbiVivo == true)
        {   Presenca();
            if (currentState == ZombieState.Walking)
            {
                Seguir();
            }
            if (currentState == ZombieState.Attacking)
            {
                animator.SetTrigger("atk");
                OlharPersonagem();
            }
        }
    }

    void Presenca()
    {
        //Calcula a Distancia Entre Zumbi e Personagem
        float distanciaPersonagem = Vector3.Distance(
            personagem.transform.position,
            transform.position);
        if(distanciaPersonagem < 2)
        {
            currentState = ZombieState.Attacking;
        }
        else if (distanciaPersonagem < 10)
        {
            currentState = ZombieState.Walking;
            animator.SetBool("walk", true);

        }
        else if(distanciaPersonagem > 20)
        {
            currentState = ZombieState.Idle;
            animator.SetBool("walk", false);
        }
    }


    void Seguir()
    {

        Vector3 vetorCorrigido = new Vector3(
            personagem.transform.position.x,
            transform.position.y,
            personagem.transform.position.z);

        transform.position = Vector3.MoveTowards(
            transform.position,
            vetorCorrigido,
            0.01f);

        transform.LookAt(vetorCorrigido);
    }

    void OlharPersonagem()
    {
        Vector3 vetorCorrigido = new Vector3(
            personagem.transform.position.x,
            transform.position.y,
            personagem.transform.position.z);

        transform.LookAt(vetorCorrigido);

    }

    public void IniciarAtaque()
    {
        //Ativando o meu ataque;
        meuAtaque.SetActive(true);
    }

    public void EncerrarAtaque()
    {
        //Desativando o meu ataque;
        meuAtaque.SetActive(false);
    }

    public void TomeiDano(int tipoDano)
    {
        hp = hp - tipoDano;
        meuAtaque.SetActive(false);
        if (hp < 0)
        {
            animator.SetBool("die", true);
        }
        animator.SetTrigger("dmg");
    }
    public void Morreu()
    {
        zumbiVivo = false;
        meuAtaque.SetActive(false);
        GetComponent<CapsuleCollider>().enabled = false;
        //Destroy(this.gameObject);
        //Inativado
    }


    private void OnTriggerEnter(Collider colidir)
    {
        //ELE TOCOU NA BALA?
        if(colidir.gameObject.tag == "Bullet")
        {
            if (zumbiVivo == true)
            {
                Destroy(colidir.gameObject);
                TomeiDano(10);
            }
        }
    }


    ///Cuidando do Som
    ///som 0 - Idle
    ///Som 1 - Walk
    ///Som 2 - Atack
    ///Som 3 - Dano

    public void SomZumbi(int tipoSom) {

        GetComponent<AudioSource>().clip = sonsZumbi[tipoSom];
        GetComponent<AudioSource>().Play();
    }
   
}
