using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class GameManager : MonoBehaviour
{
    public Material questionMaterial;
    private bool _isRunning;
    public TMP_Text scoreText;
    public TMP_Text coinsText;
    public TMP_Text levelText;
    public TMP_Text timeText;
    public AudioSource coinSound;
    public AudioSource brickSound;
    public ParticleSystem brickeffect;
    public GameObject GameOverPanel;
    public Transform spawn;
    public GameObject mario;
    public GameObject lvl1;
    public GameObject lvl2;
    public GameObject background;

    public float animationSpeed;
    
    private (int, int) _level;
    private int _score;
    private int _coins;
    [SerializeField]private float _time; 
    // Start is called before the first frame update
    void Start()
    {
        mario.transform.position = spawn.position;
        GameOverPanel.SetActive(false);
        _level = (1, 1);
        _time = 100;
        _coins = 0;
        _score = 0;
        _isRunning = true;
        SetWorld();
        SetCoins();
        SetScore();
        questionMaterial.mainTextureOffset = Vector2.zero;
        StartCoroutine(QuestionBlockAnimation());
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator QuestionBlockAnimation()
    {
        while (_isRunning)
        {
            yield return new WaitForSeconds(animationSpeed);
            questionMaterial.mainTextureOffset += new Vector2(0, 1/5f);
        }
    }

    private IEnumerator Timer()
    {
        while (_isRunning && _time > 0)
        {
            yield return new WaitForSeconds(1f);
            _time -= 1f;
            timeText.text = "Time\n" + _time;
        }
        if (_time <= 0)
        {
            GameOverPanel.SetActive(true);
        }
    }

    private void SetWorld()
    {
        levelText.text = $"WORLD\n{_level.Item1}-{_level.Item2}";
    }
    
    private void SetCoins()
    {
        coinsText.text = $"x{_coins.ToString("D2")}";
    }
    
    private void SetScore()
    {
        scoreText.text = $"MARIO\n{_score.ToString("D6")}";
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
        SetCoins();
    }
    
    public void AddScore(int score)
    {
        _score += score;
        SetScore();
    }

    public void BrickPlay()
    {
        if (!brickeffect.IsDestroyed() && !brickSound.IsDestroyed())
        {
            brickeffect.Play();
            brickSound.Play();
        }
    }

    public void CoinPlay()
    {
        if (!coinSound.IsDestroyed())
        {
            coinSound.Play();
        }
    }

    public void NextLevel()
    {
        _level = (_level.Item1 + 1, _level.Item2);
        _time = 100;
        SetWorld();
        mario.transform.position = spawn.position;
        lvl1.SetActive(false);
        lvl2.SetActive(true);
        background.SetActive(false);
    }
}
