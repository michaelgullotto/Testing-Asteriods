using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : MonoBehaviour
{
   
    Rigidbody2D _rididbody;
    public float size = 0.03f;
    public float minSize = 0.02f;
    public float maxSize = 0.05f;
    public float speed = 50.0f;
    public float maxlifeTime = 30.0f;
    public GameObject SpawnedAsteriod;
    private void Awake()
    {
       
        _rididbody = GetComponent<Rigidbody2D>();
        SpawnedAsteriod = this.gameObject;
      
    }

    

    private void Start()
    {
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size  ;


        _rididbody.mass = this.size * 300;
    }

    public void SetTrajectort(Vector2 direction)
    {
        _rididbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxlifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if ((this.size* 0.5f) > minSize)
            {
                CreateSplit();
                CreateSplit();

            }
            Destroy(this.gameObject);
            Player.score++;
        }
        
        Player player = collision.gameObject.GetComponent<Player>();
        if (player!= null)
        {
            player.life = player.life - 1;
            Destroy(this.gameObject);
        }
    }
    public void CreateSplit()
    {
        Vector2 postion = this.transform.position;
        postion += Random.insideUnitCircle * 0.5f;
        Asteroid half = Instantiate(this, postion, this.transform.rotation);
        half.size = this.size * 0.5f;
        SetTrajectort(Random.insideUnitCircle.normalized * this.speed);
    }

  
    public GameObject SpawnAsteriodRef()
    {
        return SpawnedAsteriod;
    }

    
}
