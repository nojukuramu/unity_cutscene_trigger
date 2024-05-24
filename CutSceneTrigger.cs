using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    [Header("Cutscene Settings")]
    [SerializeField]
    [Tooltip("Choose the type of Trigger: \n 1. RadialDistanceDetection allows you to put this on any object. It detects an object with Player Tag in certain range. \n 2. Interact wait for a function call. call it using 'StartCutscene()' \n 3. Collider3D uses the collider of the this object. Make sure the collider is using IsTrigger on.")]
    private TypeOfTrigger typeOfTrigger;

    [Tooltip("If not using playable director, playableDirector will be forced to null and will use cscript instead by default.")]
    public bool usePlayableDirector; 
    public PlayableDirector playableDirector;
    private CutsceneList cutsceneScript;
    public string sceneName;

    public enum TypeOfTrigger {
        RadialDistanceDetection,
        Interact,
        Collider3D,
        Collider2D
    }

    [Header("Radial Distance Distance Settings")]
    //Choosing This Option will allow you to attach it where ever you want.
    public float detectionRadius;
    [Tooltip("Default: Cutscenes can only be triggered once. \n Create handler for this to allow multiple trigger.")]
    public bool isTriggered = false;

    [Header("Mark Down Scripting")]
    [Tooltip("Leave this unchecked if you don't know this")]
    public bool useMarkDownScripting;
    public string textFilename;
    private TextAsset textAsset;
    void Start()
    {
        if (useMarkDownScripting) {
            textAsset = Resources.Load<TextAsset>(textFilename);
        }

        if (!usePlayableDirector) {
            cutsceneScript = GetComponent<CutsceneList>();
        }
    }

    void Update()
    {
        if (!usePlayableDirector) {
            playableDirector = null;
        }

        if (!useMarkDownScripting) {
            textFilename = "Mark Down Scripting is disabled.";
        }

        if (typeOfTrigger == TypeOfTrigger.RadialDistanceDetection) {
            RadialTrigger();
        } 
        else {
            detectionRadius = 0;  //cleans up the Gizmos on play mode if type of trigger is not RDD
        }
    }

    void RadialTrigger() {
        if (isTriggered == false) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    Debug.Log("Player Detected. Playing Cutscene");
                    privateCutsceneStart();
                }
            }
        }
        
    }

    void OnTriggerEnter(Collider collision) {
        if (typeOfTrigger == TypeOfTrigger.Collider2D && collision.gameObject.tag == "Player") {
            privateCutsceneStart();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (typeOfTrigger == TypeOfTrigger.Collider3D && collision.gameObject.tag == "Player") {
            privateCutsceneStart();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void privateCutsceneStart() {
        if (usePlayableDirector) {
            if (playableDirector != null) {
                playableDirector.Play();
            }
            else {
                Debug.LogError("Playable Director is missing");
            }
        }
        else {
            if (cutsceneScript != null) {
                cutsceneScript.StartScene(sceneName);
            }
            else {
                Debug.LogWarning("CutSceneScript is missing.");
            }
        }
        

        if (useMarkDownScripting) {
            if (textAsset != null) {
                Interpreter();
            }
            else {
                Debug.LogWarning("Text File is missing.");
            }
        }
    }
    
    public void StartCutscene() {
        if (typeOfTrigger == TypeOfTrigger.Interact) {
            privateCutsceneStart();
        }
        else {
            Debug.LogError("Called StartCutscene() but is not allowed. Can only be called in Type Of Trigger: Interact.");
        }
    }

    void Interpreter() {
        //This Part Is Unfinished. Forgive me :<
    }

    string ReadTextFile(float lineNumberToRead)
    {
        // Split the text asset into lines
        string[] lines = textAsset.text.Split('\n');
        int lineBeingRead = 1;

        // Iterate through each line
        foreach (string line in lines)
        {
            if (lineNumberToRead == lineBeingRead) {
                return line;
            }

            lineBeingRead += 1;
        }

        return "<end>";

    }
}
