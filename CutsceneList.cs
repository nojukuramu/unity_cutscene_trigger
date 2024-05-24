using UnityEngine;

public class CutsceneList : MonoBehaviour
{
    public void  StartScene(string scene) {
        Debug.Log(scene);
        switch (scene) {
            case "sample":
                Sample();
                break;
        }
    }

    public void Sample() {
        Debug.Log("Scene Was Called");
        //Your Code here like calling a IEnumerator funtion... etc.
    }
}
