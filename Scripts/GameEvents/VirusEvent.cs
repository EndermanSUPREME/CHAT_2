using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEvent : MonoBehaviour
{
    [SerializeField] GameObject[] VirusPopUpWindowPrefs;
    [SerializeField] Transform VirusPopUpParent;
    [SerializeField] RectTransform SpawningArea;
    [SerializeField] AudioSource[] virusSounds;
    bool minigame_started = false, virusStopped = false;
    List<GameObject> virusWindowsSpawnedInScene = new List<GameObject>();

    public void startVirusTimer()
    {
        if (!minigame_started)
        {
            Invoke("LaunchAttack", Random.Range(2, 4));
            minigame_started = true;
        }
    }

    void LaunchAttack()
    {
        StartCoroutine(SpawnSpamWindow());
    }

    public void AntiVirusDefence()
    {
        virusStopped = true;
        StopCoroutine(SpawnSpamWindow());

        foreach (GameObject viWind in virusWindowsSpawnedInScene.ToArray())
        {
            if (viWind == null)
            {
                virusWindowsSpawnedInScene.Remove(viWind);
            }
        }

        foreach (GameObject viWind in virusWindowsSpawnedInScene.ToArray())
        {
            Destroy(viWind);
        }

        virusWindowsSpawnedInScene.Clear();

        foreach (AudioSource sound in virusSounds)
        {
            sound.Stop();
            sound.mute = true;
        }
    }

    public AudioSource[] GetVirusSoundList()
    {
        return virusSounds;
    }

    Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }

    IEnumerator SpawnSpamWindow()
    {
        if (!virusStopped)
        {
            if (VirusPopUpParent.childCount < 15)
            {
                Vector3 spawnPosition = GetBottomLeftCorner(SpawningArea) - new Vector3(Random.Range(0, SpawningArea.rect.x), Random.Range(0, SpawningArea.rect.y), 0);
                GameObject newPopUp = Instantiate(VirusPopUpWindowPrefs[Random.Range(0, VirusPopUpWindowPrefs.Length)], spawnPosition, VirusPopUpWindowPrefs[Random.Range(0, VirusPopUpWindowPrefs.Length)].transform.rotation);
                virusWindowsSpawnedInScene.Add(newPopUp);
                newPopUp.transform.SetParent(VirusPopUpParent);
                newPopUp.transform.SetSiblingIndex(VirusPopUpParent.childCount - 1);

                foreach (GameObject viWind in virusWindowsSpawnedInScene.ToArray())
                {
                    if (viWind == null)
                    {
                        virusWindowsSpawnedInScene.Remove(viWind);
                    }
                }
            }
    
            yield return new WaitForSeconds(Random.Range(1, 3));
            StartCoroutine(SpawnSpamWindow());
        }
    }
}//EndScript