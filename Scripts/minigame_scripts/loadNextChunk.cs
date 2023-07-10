using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadNextChunk : MonoBehaviour
{
    [SerializeField] GameObject nextChunk, currentChunk;
    [SerializeField] Transform playerObject, newChunkSpawnPoint;

    void LoadChunk()
    {
        nextChunk.SetActive(true);
        playerObject.position = newChunkSpawnPoint.position;
        currentChunk.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        LoadChunk();
    }
}//EndScript