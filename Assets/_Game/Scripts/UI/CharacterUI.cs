using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CharacterUI : MonoBehaviour
{
    //public Text text;
    public Image enemyIcon;
    public Character target;
    public TextMeshProUGUI targetName;

    private Vector3 offset = new Vector3(0, 4, 0);
    private Transform nameTf;
    private Transform enemyTf;
    public CinemachineBrain CMcam => VcamManager.Ins.cinemachineBrain;

    private void Awake()
    {
        nameTf = targetName.transform;
        enemyTf = enemyIcon.transform;
    }

    public void Start()
    {
        enemyIcon.enabled = false;
    }
    // Update is called once per frame
    private void Update()
    {
        if (GameManager.IsState(GameState.GamePlay) && target != null)
        {
            enemyIcon.enabled = true;
            float minX = 40;
            float maxX = Screen.width - minX;

            float minY = 20;
            float maxY = Screen.height - minY;

            Vector3 EnemyPos = target.transform.position + offset;
            Vector3 EnemyName = target.transform.position + offset;

            Vector2 enemyPos = CMcam.OutputCamera.WorldToScreenPoint(EnemyPos);
            Vector2 enemyName = CMcam.OutputCamera.WorldToScreenPoint(EnemyName);
            Rect screenRect = new Rect(minX, minY, maxX - minX, maxY - minY);

            if (screenRect.Contains(enemyPos))
            {
                enemyIcon.enabled = false;
                targetName.text = target.characterName;
            }
            else
            {
                
            }

            enemyPos.x = Mathf.Clamp(enemyPos.x, minX, maxX);
            enemyPos.y = Mathf.Clamp(enemyPos.y, minY, maxY);
            enemyTf.position = enemyPos;
            nameTf.position = enemyName;
        }
 
    }

    public void Setlevel()
    {
        Debug.Log(target.levelCharacter);
        //targetlv.text = target.levelCharacter.ToString();
    }
}