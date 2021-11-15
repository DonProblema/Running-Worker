using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;
    public Button restartButton;
    public Button startButton;
    private SpawnManager spawnManagerScript;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool isGameActive = false;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver && isGameActive)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            RestartGame();
        }
    }

    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, 0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        titleText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        spawnManagerScript.StartGame();
    }

    private void GameOver()
    {
        gameOver = true;
        explosionParticle.Play();
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        dirtParticle.Stop();
        playerAudio.PlayOneShot(crashSound, 0.6f);
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("Scenes/" + SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("Prototype 3");
    }
}
