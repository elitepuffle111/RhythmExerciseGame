using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject center;
    [SerializeField] private GameObject start;
    [SerializeField] private float maxHeight = 0.5f;
    [SerializeField] private int rotationSpeed = 30;
    [SerializeField] private float floatSpeed = 0.0005f;
    [SerializeField] private float spawnDepth = 5;
    [SerializeField] private float radius = 45;
    [SerializeField] private AudioSource song;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float bps = 1.5f;
    [SerializeField] private GameObject scoreUI;
    private int score = 0;
    

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool startGame;
    private List<GameObject> centers;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = start.transform.position;
        startingRotation = start.transform.rotation;
        centers = new List<GameObject>();
        startGame = false;
        center.transform.GetChild(0).localPosition = new Vector3(radius, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!startGame)
            return;
        Vector3 pos = start.transform.position;
        pos.y -= spawnDepth;
        startingPosition = pos;
        startingRotation = start.transform.rotation; 
        
        time += Time.deltaTime;
        if (time > bps)
        {
            SpawnBeats();
            time = 0;
        }
        Rotate();
        MoveUp();
        CheckDestroyedStatus();
        DestroyBeat(); 
    }

    public void StartGame()
    {
        startGame = true;
    }

    public void PlaySong()
    {
        song.PlayOneShot(clip);
    }
    private void Rotate()
    {
        foreach (GameObject cent in centers) {
            cent.transform.RotateAround(cent.transform.position, Vector3.up, (rotationSpeed) * Time.deltaTime);
        }
    }

    private void CheckDestroyedStatus()
    {
        if (centers.Count > 0)
        {
            if (centers[0].GetComponentInChildren<Beats>().GetIsDestroyed())
            {
                score++;
                scoreUI.GetComponent<TextMeshPro>().text = score.ToString();
            }
        }
    }

    private void SpawnBeats()
    {
        GameObject centerNew = Instantiate(center, startingPosition, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));
        centerNew.name = "New Center";
        centers.Add(centerNew);
    }

    private void MoveUp()
    {
        foreach (GameObject cent in centers)
        {
            Vector3 pos = cent.transform.position;
            pos.y += floatSpeed;
            cent.transform.position = pos;
        }
    }

    private void DestroyBeat()
    {
        if (centers.Count > 0)
        {
            if (centers[0].transform.position.y > maxHeight + start.transform.position.y)
            {
                Destroy(centers[0]);
                centers.Remove(centers[0]);
            }
        } 
    }

}
