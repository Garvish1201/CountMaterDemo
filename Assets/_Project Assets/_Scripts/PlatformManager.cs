using System;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static Action LevelGenerationComplete;

    [Header(" Elements ")]
    [SerializeField] private Platform[] platformForRandomGen;
    [SerializeField] private Platform[] platformForFixedGen;
    [SerializeField] private LevelChuncks[] levelChuncks;

    [Header(" Settings ")]
    [SerializeField] private float levelLength = 1;
    [SerializeField] private int levelToSpawn = 0;
    public float chunkWidth;

    private void Start()
    {
        int getLevelInfo = GameManager.instance.currentLevel;
        levelToSpawn = getLevelInfo;
        GenerateFixedLevel();
    }

    private void GenerateFixedLevel()
    {
        Vector3 chunkPosition = Vector3.zero;

        int levelLengh = levelChuncks[levelToSpawn].platforms.Length;

        for (int i = 0; i < levelLengh; i++)
        {
            Platform chunkToCreate = levelChuncks[levelToSpawn].platforms[i];

            // skip for the first time 
            if (i > 0)
                chunkPosition.z += chunkToCreate.GetLenght() / 2;

            Platform chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            // for the spawing of the next platform
            chunkPosition.z += chunkInstance.GetLenght() / 2;
        }

        // on level generation is completed
        LevelGenerationComplete?.Invoke();
    }    

    // TESTING------------------------------------
    private void GenerateRandomLevel ()
    {
        Vector3 chunkPosition = Vector3.zero;

        for (int i = 0; i < levelLength; i++)
        {
            Platform chunkToCreate = platformForRandomGen[UnityEngine.Random.Range(0, platformForRandomGen.Length)];

            // after first platform is spawned
            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLenght() / 2;
            }

            Platform chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstance.GetLenght() / 2;
        }
    }
}

[System.Serializable]
public class LevelChuncks
{
    public string levelName;
    public Platform[] platforms;
}
