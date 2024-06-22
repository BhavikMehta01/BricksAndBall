using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;
    //[SerializeField]
    //private Slider withdrawAmountSlider;

    //[SerializeField]
    //private Text withdrawAmountText;

    [SerializeField]
    private GameObject scorePanel, highScorePanel;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    withdrawAmountSlider.onValueChanged.AddListener((v) =>{

    //        withdrawAmountText.text = v.ToString("$00.00");
    //    });
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShareGame() {
        StartCoroutine(LoadImageAndShare());
    }

    IEnumerator LoadImageAndShare() 
    {
        Texture2D image = Resources.Load("image",typeof(Texture2D)) as Texture2D;
        yield return null;

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, image.EncodeToPNG());

        new NativeShare().AddFile(filePath).SetText("<b>Ball and Bricks Cash </b></n>I scored 1000 points in my game.").SetUrl("https://www.google.com").Share();
    }

    public void NextButton(GameObject nextPannel) {
        nextPannel.SetActive(true);
        SoundManagerGame.instance.allSounds[0].Play();
    }

    public void LastButtonOk() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GotoMenu() {

       
        SceneManager.LoadScene("MenuScene");
    }
}
