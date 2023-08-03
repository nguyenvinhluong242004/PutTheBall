using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sq : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Lấy ra điểm va chạm đầu tiên giữa hình vuông và quả bóng
            ContactPoint2D contactPoint = collision.GetContact(0);

            // Tính toán và xuất vector pháp tuyến của mặt va chạm
            Vector2 normal = - contactPoint.normal;
            player.setVelocity(normal);
            Debug.Log("Vector pháp tuyến của mặt va chạm: " + normal);
        }
    }
}
