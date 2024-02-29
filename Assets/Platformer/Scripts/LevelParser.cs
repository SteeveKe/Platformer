using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject tubePrefab;
    public GameObject castlePrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        int row = 0;
        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                if (letter == 'x')
                {
                    Vector3 newPos = new Vector3(column, row, 0f);
                    GameObject gameObject = Instantiate(rockPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                else if (letter == 'b')
                {
                    Vector3 newPos = new Vector3(column, row, 0f);
                    GameObject gameObject = Instantiate(brickPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                else if (letter == '?')
                {
                    Vector3 newPos = new Vector3(column, row, 0f);
                    GameObject gameObject = Instantiate(questionBoxPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                else if (letter == 's')
                {
                    Vector3 newPos = new Vector3(column, row, 0f);
                    GameObject gameObject = Instantiate(stonePrefab, newPos, Quaternion.identity, environmentRoot);
                }
                else if (letter == 't')
                {
                    Vector3 newPos = new Vector3(column + 0.5f  , row - 0.5f, 0f);
                    GameObject gameObject = Instantiate(tubePrefab, newPos, Quaternion.identity, environmentRoot);
                }
                else if (letter == 'c')
                {
                    Vector3 newPos = new Vector3(column + 2.5f, row - 0.5f, 0f);
                    GameObject gameObject = Instantiate(castlePrefab, newPos, Quaternion.identity, environmentRoot);
                }
            }

            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
