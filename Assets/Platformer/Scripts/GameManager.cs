using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
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

    public float animationSpeed;
    
    private (int, int) _level;
    private int _score;
    private int _coins;
    private float _time; 
    // Start is called before the first frame update
    void Start()
    {
        _level = (1, 1);
        _time = 400;
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
        brickeffect.Play();
        brickSound.Play();
    }

    public void CoinPlay()
    {
        coinSound.Play();
    }
}