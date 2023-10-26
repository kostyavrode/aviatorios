using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public AudioSource shootSound;
    public bool isGameStarted;
    public Gate gate;
    public Enemy enemyPrefab;
    public Joystick joystick;
    public AirplaneController player;
    public CameraFollower cameraFollower;
    public FuelTank fuelTank;
    public Transform playerStartPosition;
    public float timeBetweenSpawnTanks = 10f;
    public float timeBetweenSpawnEnemies = 10f;
    public float timeBetweenSpawnGates = 10f;
    private float spendedTime;
    private float spendedTimeEnemies;
    private float spendedTimeGates;
    private List<Enemy> enemies = new List<Enemy>();
    private List<FuelTank> fuelTanks = new List<FuelTank>();
    private List<Gate> gates = new List<Gate>();
    public void Awake()
    {
        instance = this;
        AirplaneController playerObject = Instantiate(player);
        playerObject.transform.position = playerStartPosition.position;
        playerObject.joystick = this.joystick;
        playerObject.isCanFly = true;
        playerObject.GetComponent<AirplaneShoot>().shootSound = this.shootSound;
        CameraFollower cameraObject = Instantiate(cameraFollower);
        cameraObject.transform.position = new Vector3(12.5f, -4.3f, -2f);
        cameraObject.target = playerObject.transform;
        SpawnFuelTanks();
        SpawnGates();
        SpawnEnemies();
        isGameStarted = true;
    }
    private void FixedUpdate()
    {
        if (spendedTime>=timeBetweenSpawnTanks)
        {
            SpawnFuelTanks();
            spendedTime = 0;
        }
        if (spendedTimeEnemies >= timeBetweenSpawnEnemies)
        {
            SpawnEnemies();
            spendedTimeEnemies = 0;
        }
        if (spendedTimeGates >= timeBetweenSpawnGates)
        {
            SpawnGates();
            spendedTimeGates = 0;
        }
        spendedTime += Time.fixedDeltaTime;
        spendedTimeEnemies += Time.fixedDeltaTime;
        spendedTimeGates += Time.fixedDeltaTime;
    }
    private void SpawnFuelTanks()
    {
        for(int i=0;i<10;i++)
        {
            FuelTank fuel = Instantiate(fuelTank);
            fuelTanks.Add(fuel);
            
            if (fuelTanks.Count > 1)
            {
                Debug.Log(fuelTanks[fuelTanks.Count - 1].transform.position.z);
                fuel.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(10, 20), Random.Range(fuelTanks[fuelTanks.Count-2].gameObject.transform.position.z + 5, fuelTanks[fuelTanks.Count-2].gameObject.transform.position.z + 20));
            }
            else
            {
                fuel.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(10, 20), Random.Range(20,50));
            }
        }
    }
    private void SpawnGates()
    {
        for (int i = 0; i < 4; i++)
        {
            Gate newGate = Instantiate(gate);
            gates.Add(newGate);

            if (gates.Count > 1)
            {
                newGate.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(10, 20), Random.Range(gates[gates.Count - 2].gameObject.transform.position.z + 5, gates[gates.Count - 2].gameObject.transform.position.z + 20));
            }
            else
            {
                newGate.transform.position = new Vector3(13.08f, Random.Range(10, 20), Random.Range(30, 50));
            }
        }
    }
    private void SpawnEnemies()
    {
        for (int i = 0; i < 1; i++)
        {
            Enemy newEnemy = Instantiate(enemyPrefab);
            newEnemy.target = AirplaneController.instance.GetPlayerTransform();
            enemies.Add(newEnemy);

            if (enemies.Count > 1)
            {
                newEnemy.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(10, 20), Random.Range(enemies[enemies.Count - 2].gameObject.transform.position.z + 50, enemies[enemies.Count - 2].gameObject.transform.position.z + 80));
            }
            else
            {
                newEnemy.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(10, 20), Random.Range(40, 50));
            }

        }
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }
}
