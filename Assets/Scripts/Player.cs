using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;


// MonoBehaviour is getting extended by the Player class, and because of Inheritance the Start() and the Update() functions can be used inside the Player Class
public class Player : MonoBehaviour
{


    // Let us define a variable _speed of type float
    [SerializeField] 
    private float _speed = 3.2f;
    //Using the laser gameobject inside the Player object
    [SerializeField]
    private GameObject _laserPrefab;
    //To cause a delay in the firing of the laser
    [SerializeField]
    private float _fireRate= 0.15f;
    private float _canFire = -1f;
    // Lives of the player
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            UnityEngine.Debug.LogError("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Let's call the methods here
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

    }

    void CalculateMovement()
    {
        // Gets the input from Users - W,S,A,D or Up,Down,Left,Right
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Moves the Player Object
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);


        // Putting boundaries to the player object - Vertical
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        // Putting boundaries to the player object - Horizontal
        if (transform.position.x > 9.2f)
        {
            transform.position = new Vector3(-9.2f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.2f)
        {
            transform.position = new Vector3(9.2f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        _lives--;
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }

    }

    
}
