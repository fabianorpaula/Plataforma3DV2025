 using UnityEngine;

public class Arma : MonoBehaviour
{
    //Objeto que vou Atirar
    public GameObject projetil;
    public GameObject pontoSaida;
    public int maxMunicao = 40;
    public int municao;

    private void Start()
    {
        municao = maxMunicao;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)|| Input.GetButtonDown("Fire1")){
           if(municao > 0)
            {
                Disparo();
            }
            
        }
    }
    void Disparo()
    {
        municao--;
        //Crio o Projetil
        GameObject Tiro = Instantiate(projetil, 
            pontoSaida.transform.position, 
            Quaternion.identity);
        //Dou Velociado ao Projetil
        Tiro.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        Destroy(Tiro, 1f);
        
    }


}
