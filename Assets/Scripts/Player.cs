using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gameoverPanel;
    public GameObject lifeText;
    public GameObject scoreText;
    static public int score = 0;
    public int life = 3;
    public Bullet bulletPrefab;
    float turnSpeed = 1.0f;
    float thrustSpeed = 3.5f;
    public bool isDead = false;
    Rigidbody2D _rigidbody;
    bool _thrusting;
    bool _reversing;
    float _turnDirection;
    public bool testing = false;
    

    private void Awake()
    {
        Time.timeScale = 1;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isDead = false;
        if (!testing)
        {
            gameoverPanel.SetActive(false);
        }
        Time.timeScale = 1;
    }

    void Update()
    {
        // moves forward
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        _reversing = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        // rotatates player
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else 
        {
            _turnDirection = 0.0f;
        }

        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        if (!testing)
        {
            scoreText.GetComponent<Text>().text = "Score : " + score;
            lifeText.GetComponent<Text>().text = "Life : " + life;
        }

        if (life <= 0)
        {
            isDead = true;
            gameover();
        }
    }

    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
        if (_reversing)
        {
            _rigidbody.AddForce(this.transform.up * -this.thrustSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    void gameover()
    {
        Time.timeScale = 0;
        
        if (!testing)
        {
            gameoverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        if (!testing)
        {
            gameoverPanel.SetActive(false);
        }
        Time.timeScale = 1;
        isDead = false;
        life = 3;
        score = 0;
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }




}
