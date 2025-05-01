using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnterShop() {
        Debug.Log("Entering");
        SceneManager.LoadScene("ShopInside");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
