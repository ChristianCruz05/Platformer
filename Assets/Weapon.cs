using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   //gun
    public Camera cam;
    public float speed;
    public Transform PlayerPosition;
    public float IncreaseGunHeight;
    
    //bullet
    public GameObject bullet;
    public float launchForce;
    public Transform ShotPoint;

    

    //range
    [SerializeField] Transform player;
    public float speedUpRange;
    public float extraSpeed;
    public float originalSpeed;
    
    void Start()
    {
        transform.position = PlayerPosition.position;
        originalSpeed = speed;

    }

    void Update()
    {
        //speed up when out of range
        float distanceToPlayer =  Vector2.Distance(transform.position, player.position);
        if(distanceToPlayer > speedUpRange)
        {
            speed = speed + extraSpeed;
        }
        else{
            speed = originalSpeed;
        }


        Vector2 gunPosition = transform.position;
        Vector2 mousePosition =cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - gunPosition;
        transform.right = direction;
        Vector3 NewPosition = new Vector3(PlayerPosition.position.x,PlayerPosition.position.y + IncreaseGunHeight, 0);
        transform.position  = Vector3.MoveTowards(transform.position,NewPosition, speed *Time.deltaTime);     
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
            AudioManager.Instance.PlaySFX("gun");
            CinemachineShake.Instance.ShakeCamera(2f, 0.1f);
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, ShotPoint.position, ShotPoint.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity =transform.right * launchForce;
    }

}
