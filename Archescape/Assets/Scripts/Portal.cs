using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals(TagRepository.player))
        {
            SceneManager.LoadScene(1);
        }
    }
}
