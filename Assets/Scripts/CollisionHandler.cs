using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] ParticleSystem Boom;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RespondToDebugKys();
    }
    void OnTriggerEnter(Collider other) 
    {
        Debug.Log($"{this.name} **Trigfered by** {other.gameObject.name}");
        if(isTransitioning || collisionDisabled) { return; }
        StarCrashSequence();
    }
    void RespondToDebugKys()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //togle collision on off
        }
    }
    void StarCrashSequence()
    {
        isTransitioning = true;
        Boom.Play();
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        // PlayerShip enabled = false;
        Invoke("ReloadLevel", delayTime);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}