using UnityEngine;

public class paddleController : MonoBehaviour
{
    public float kecepatan;
    public string axis;
    public float batasAtas;
    public float batasBawah;
   
    void Start()
    {
        
    }

  
    void Update()
    {
        //gerakin pemukul
        float gerak = Input.GetAxis(axis) * kecepatan * Time.deltaTime;

        //biar nabrak batas atas sama bawah
        float nextPos = transform.position.y + gerak;
        if (nextPos > batasAtas)
        {
            gerak = 0;
        }
        if (nextPos < batasBawah)
        {
            gerak = 0;
        }
        transform.Translate(0, gerak, 0);

       
    

    }
}
