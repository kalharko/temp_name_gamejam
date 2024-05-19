using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class BeltBehavior : MonoBehaviour
{
    // Prefab des sushis
    [SerializeField] private GameObject sushiPrefab;
    // Interval de temps entre chaque accélération de la belt
    [SerializeField] private float accelerationInterval = 10.0f;
    // Interval de temps entre chaque accélération du spawn de sushi
    [SerializeField] private float spawnAccelerationInterval = 10.0f;
    // Interval de temps entre spawn de sushi
    [SerializeField] private float spawnInterval = 1.0f;
    // Valeur de l'accélération
    [SerializeField] private float accelerationValue = 0.1f;
    // Valeur de l'accélération du spawn de sushi
    [SerializeField] private float spawnAccelerationValue = 0.1f;
    // Ref vers l'enfant spline
    [SerializeField] private Transform spline_gameObject;
    // Vitesse de la belt
    [SerializeField] private float beltSpeed = 0.02f;
    // Punish speed augmentation
    [SerializeField] private float punishSpeedUp = 0.1f;
    // Punish duration
    [SerializeField] private float punishDuration = 5.0f;

    // Temps écoulé depuis la dernière accélération
    private float timeSinceLastAcceleration = 0.0f;
    // Temps écoulé depuis la dernière accélération du spawn de sushi
    private float timeSinceLastSpawnAcceleration = 0.0f;
    // Temps écoulé depuis le dernier spawn de sushi
    private float timeSinceLastSpawn = 0.0f;

    // Reference vers l'enfant spline
    private SplineContainer spline;

    // Variables pour l'animation 

    [SerializeField] private GameObject belt;
     public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;        
    // Intervalle entre les changements de sprites
    public float interval = 1.0f;          
    
    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        // On récupère la référence vers l'enfant spline
        spline = spline_gameObject.GetComponent<SplineContainer>();
        // print(spline);

        // On initialise les temps écoulés
        timeSinceLastAcceleration = 0.0f;
        timeSinceLastSpawnAcceleration = 0.0f;
        timeSinceLastSpawn = 0.0f;

        // On récupère le sprite renderer de la belt
        
        spriteRenderer = belt.GetComponent<SpriteRenderer>();
        StartCoroutine(SwitchSprites());
    }

    // Update is called once per frame
    void Update()
    {
        // On incrémente les temps écoulés
        timeSinceLastAcceleration += Time.deltaTime;
        timeSinceLastSpawnAcceleration += Time.deltaTime;
        timeSinceLastSpawn += Time.deltaTime;

        // Si le temps écoulé depuis la dernière accélération est supérieur à l'intervalle d'accélération
        if (timeSinceLastAcceleration > accelerationInterval)
        {
            // On accélère la belt
            beltSpeed += accelerationValue;
            SetSushiSpeed(beltSpeed);
            timeSinceLastAcceleration = 0.0f;
        }

        // Si le temps écoulé depuis la dernière accélération du spawn de sushi est supérieur à l'intervalle d'accélération
        if (timeSinceLastSpawnAcceleration > spawnAccelerationInterval)
        {
            // On diminue l'intervalle de spawn de sushi
            spawnInterval -= spawnAccelerationValue;
            timeSinceLastSpawnAcceleration = 0.0f;
        }

        // Si le temps écoulé depuis le dernier spawn de sushi est supérieur à l'intervalle de spawn
        if (timeSinceLastSpawn > spawnInterval)
        {
            // On crée un sushi
            GameObject sushi = Instantiate(sushiPrefab, spline_gameObject);
            // On le place au début du chemin de l'enfant spline
            sushi.transform.position = spline.EvaluatePosition(0.0f);
            // On lui donne la vitesse de la belt
            sushi.GetComponent<SushiBehavior>().speed = beltSpeed;
            timeSinceLastSpawn = 0.0f;
        }
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.HasFailed)
        {
            StartPunishSpeedUp();
            GameManager.Instance.UpdateGameState(GameState.IsPlaying);
        }
    }

    private void SetSushiSpeed(float new_speed)
    {
        // get all childrens of spline_gameObject
        foreach (SushiBehavior sushi in spline_gameObject.GetComponentsInChildren<SushiBehavior>())
        {
            // set speed
            sushi.speed = new_speed;
            
        }
    }

    public void StartPunishSpeedUp()
    {
        StartCoroutine(PunishSpeedUp());
    }

    private IEnumerator PunishSpeedUp()
    {
        // get all childrens of spline_gameObject
        SetSushiSpeed(beltSpeed + punishSpeedUp);

        // wait for punishDuration
        yield return new WaitForSeconds(punishDuration);

        // call EndPunishSpeedUp
        SetSushiSpeed(beltSpeed);
    }

    IEnumerator SwitchSprites()
    {
        while (true)
        {
            spriteRenderer.sprite = sprite1;
            yield return new WaitForSeconds(interval);
            spriteRenderer.sprite = sprite2;
            yield return new WaitForSeconds(interval);
        }
    }

}
