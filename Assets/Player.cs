using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public static Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    Vector3 cameraPosition;

    [SerializeField]private TextMeshProUGUI textMeshProUGUI;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraPosition = Camera.main.transform.position;
    }
    private void Update()
    {
        CamLogic();
        ScoreLogic();
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(
                transform.position.x - speed * Time.deltaTime,
                transform.position.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(
                transform.position.x + speed * Time.deltaTime,
                transform.position.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && rb.velocity.y <= 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);

            PlatformSpawner spawner = PlatformSpawner.Instance;
            spawner.ReloadPlatforms();
        }
    }
    void CamLogic()
    {
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (transform.position.y > cameraPosition.y - 1f)
        {
            cameraPosition.y = Mathf.Max(cameraPosition.y, transform.position.y);

            Camera.main.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z);
        }
        
        if (screenPosition.x > 1)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, screenPosition.y, screenPosition.z));
        }
        else if (screenPosition.x < 0)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1, screenPosition.y, screenPosition.z));
        }
    }
    void ScoreLogic()
    {
        float score = Camera.main.transform.position.y;

        // ‘орматуЇмо число з одним дес€тковим знаком
        string formattedScore = score.ToString("F1");

        // ¬идал€Їмо кому з р€дка
        string result = formattedScore.Replace(",", "").Replace(".", "");

        // якщо результат починаЇтьс€ з нул€, то видал€Їмо його
        if (result.Length > 1 && result.StartsWith("0"))
        {
            result = result.Substring(1);
        }
        textMeshProUGUI.text = result;

    }
}
