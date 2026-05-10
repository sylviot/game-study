using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    Pause,
    NoInitialized,
    Initializing,
    Running,
    GameOver,
    Restarting,
}

public class Game : MonoBehaviour
{
    public delegate EventArgs PersonagemDelegate();
    public static event Action PersosnagemHit;

    public GameStatus status = GameStatus.NoInitialized;

    public GameObject Emissor;
    public GameObject AguaPrefab;
    public GameObject MargemPrefab;
    public GameObject PeixePrefab;
    public GameObject TroncoPrefab;

    private float aguaTime = 0f;
    private float aguaSpeed = 0.5f;
    private float aguaSpeedMin = 0.05f;
    private float aguaSpeedMax = 6f;
    private int peixe = 0;
    private float distance = 0f;
    private List<GameObject> Aguas = new List<GameObject>();

    private float troncoTime = 0f;
    public float troncoEmitTime = 5f;
    private List<GameObject> Troncos = new List<GameObject>();

    public Text MarcadorPeixe;
    public Text MarcadorDistancia;
    public GameObject Logo;
    public GameObject Base;
    public GameObject Personagem;

    public GameObject Warning0;
    public GameObject Warning1;
    public GameObject Warning2;

    public int laneAtual = 0;
    public float laneDistancia = 7f;

    public float MaxVelocity;
    public float Velocity;
    public float Accelaration = 0.1f;

    private void Start()
    {
        //StartCoroutine(nameof(FakeStart));

        PersosnagemHit += Game_PersosnagemHit;
    }

    public static void PersonagemHitHandle()
    {
        Debug.Log("GameOver");
        Game.PersosnagemHit.Invoke();
    }

    private void Game_PersosnagemHit()
    {
        status = GameStatus.GameOver;
    }

    private void Update()
    {
        Vector3 startPos = transform.position;
        if (status == GameStatus.NoInitialized)
        {
            laneAtual = 0;
            distance = 0;
            peixe = 0;
            troncoEmitTime = 0;
            aguaSpeed = aguaSpeedMin;

            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
            {
                status = GameStatus.Initializing;
            }
        }
        else if (status == GameStatus.Initializing)
        {
            StartCoroutine(nameof(InitializingGameScene));
        }
        else if (status == GameStatus.Running)
        {
            troncoEmitTime = 5f;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                MoveEntreLane(-1);

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                MoveEntreLane(1);

            // Controle por celular (touch swipe)
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Vector2 swipe = Input.GetTouch(0).deltaPosition;

                if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y)) // swipe horizontal
                {
                    if (swipe.x > 50f) MoveEntreLane(1);   // direita
                    if (swipe.x < -50f) MoveEntreLane(-1); // esquerda
                }
            }

            Personagem.transform.position = new Vector3(Mathf.Lerp(Personagem.transform.position.x, (laneAtual * laneDistancia), 0.2f), Personagem.transform.position.y, Personagem.transform.position.z);

            aguaSpeed = Mathf.Clamp(aguaSpeed + Time.deltaTime, aguaSpeedMin, aguaSpeedMax);
            distance += Time.deltaTime;
            MarcadorDistancia.text = $"{distance.ToString("0")} m";
        }
        else if (status == GameStatus.GameOver)
        {
            status = GameStatus.Restarting;
        }
        else if (status == GameStatus.Restarting)
        {
            aguaSpeed = aguaSpeedMin;
            StartCoroutine(nameof(RestartingGameScene));
        }

        GerenciaAgua();
        GerenciarTronco();
    }

    public void GerenciaAgua()
    {
        if (aguaTime > 0.02f)
        {
            var p = new Vector3(Emissor.transform.position.x + UnityEngine.Random.Range(-10, 10), Emissor.transform.position.y);
            AguaPrefab.transform.localScale = new Vector3(UnityEngine.Random.Range(0.1f, 0.3f), UnityEngine.Random.Range(3, 7));
            Aguas.Add(Instantiate(AguaPrefab, p, Quaternion.identity));
            aguaTime = 0f;
        }

        if (Aguas.Count > 0)
        {
            foreach (var a in Aguas)
            {
                a.transform.position = new Vector3(a.transform.position.x, a.transform.position.y - 0.3f);
            }
        }

        var aDestroy = Aguas.Where(x => x.transform.position.y < -30f);
        foreach (var a in aDestroy)
        {
            Destroy(a);
        }

        Aguas.RemoveAll(x => x.transform.position.y < -30f);

        aguaTime += Time.deltaTime * aguaSpeed;
    }

    private void GerenciarTronco()
    {
        if (troncoEmitTime == 0) return;

        if (troncoTime > troncoEmitTime)
        {
            troncoTime= 0;
            StartCoroutine(nameof(LancarTronco));
        }

        if (Troncos.Count > 0)
        {
            foreach (var t in Troncos)
            {
                t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y - 0.3f);
            }
        }

        var aDestroy = Troncos.Where(x => x.transform.position.y < -30f);
        foreach (var a in aDestroy)
        {
            Destroy(a);
        }

        Troncos.RemoveAll(x => x.transform.position.y < -30f);
        troncoTime += Time.deltaTime;
    }

    IEnumerator LancarTronco()
    {
        int lane = UnityEngine.Random.Range(-1, 2);

        if (lane == -1)
        {
            Warning0.SetActive(true);
        }
        if (lane == 0)
        {
            Warning1.SetActive(true);
        }
        if (lane == 1)
        {
            Warning2.SetActive(true);
        }


        yield return new WaitForSeconds(3f);
        Warning0.SetActive(false);
        Warning1.SetActive(false);
        Warning2.SetActive(false);
        var p = new Vector3(Emissor.transform.position.x + (lane * laneDistancia), Emissor.transform.position.y);
        Troncos.Add(Instantiate(TroncoPrefab, p, Quaternion.identity));
    }

    private void MoveEntreLane(int direcao)
    {
        int lane = Mathf.Clamp(laneAtual + direcao, -1, 1);
        if (lane != laneAtual)
        {
            laneAtual = lane;
        }
    }

    IEnumerator FakeStart()
    {
        yield return new WaitForSeconds(5f);
        status = GameStatus.Initializing;
    }

    IEnumerator InitializingGameScene()
    {
        Logo.transform.DOMoveY(-40f, 3f);
        Base.transform.DOMoveY(-35, 3f);
        Personagem.transform.DOMove(new Vector3(0, -14.5f, Personagem.transform.position.z), 5f);

        yield return new WaitForSeconds(3);
        status = GameStatus.Running;

        //yield return new WaitForSeconds(15f);
        //status = GameStatus.GameOver;
    }

    IEnumerator RestartingGameScene()
    {
        Base.transform.position = new Vector3(-9.2f, 30f, Base.transform.position.z);

        Logo.transform.DOMove(new Vector3(0, -17.2f, Logo.transform.position.z), 3f);
        Base.transform.DOMove(new Vector3(-9.2f, -2f, Base.transform.position.z), 3f);
        Personagem.transform.DOMove(new Vector3(-2.5f, -1.25f, Personagem.transform.position.z), 2f);

        yield return new WaitForSeconds(5f);
        status = GameStatus.NoInitialized;
    }
}
