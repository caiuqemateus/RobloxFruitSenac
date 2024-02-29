using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int vida = 0;
    public bool podeAtacar = false;

    bool chegouAoDestino = true;
    public float velocidade = 0.5f;

    Vector3 destino;
    Vector3 areaOriginal;
    GameObject player;

    bool rondarArea = true;
    bool seguirJogador = true;

    Rigidbody rb;
    private Animator animator;
    private bool estaOlhandoParaDireita = true;

    void Start()
    {
        destino = Vector3.zero;
        areaOriginal = transform.Find("ariaDetecao").transform.localScale;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (podeAtacar && PlayerEstaNaAreaDeAtaque())
        {
            animator.SetBool("esta atacando", true);
            return;
        }

        // Se não puder atacar ou o jogador estiver fora da área de ataque, retorna à lógica de movimento

        if (seguirJogador)
        {
            Vector3 posicaoJogador = player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, posicaoJogador, Time.deltaTime * velocidade);
        }

        if (rondarArea)
        {
            if (chegouAoDestino)
            {
                float posicaoX = Random.Range(-areaOriginal.x / 2, areaOriginal.x / 2);
                float posicaoZ = Random.Range(-areaOriginal.z / 2, areaOriginal.z / 2);
                destino = new Vector3(posicaoX, transform.position.y, posicaoZ);
                Invoke("Esperar", 1.0f);
            }

            if (!chegouAoDestino)
            {
                transform.position = Vector3.MoveTowards(transform.position, destino, Time.deltaTime * velocidade);
            }

            if (Vector3.Distance(transform.position, destino) < 0.1f)
            {
                chegouAoDestino = true;
            }
        }

        animator.SetBool("vem", !chegouAoDestino);

        Vector3 direction = (destino - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            estaOlhandoParaDireita = direction.x > 0;
        }
    }

    bool PlayerEstaNaAreaDeAtaque()
    {
        RaycastHit hit;
        Vector3 direction = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    void Esperar()
    {
        chegouAoDestino = false;
    }

    void verificarMorte()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void receberDano(int valor)
    {
        vida -= valor;
        verificarMorte();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude < 0.1f)
        {
            chegouAoDestino = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            seguirJogador = false;
            rondarArea = true;
        }
    }
}
