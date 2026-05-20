using UnityEngine;

public class MovementControllerMS : MonoBehaviour
{
    
  public Rigidbody rb;
  public float baseSpeed=100f;
  public Transform camera;
  public Transform player;
  public Collider physicsColider;
  public float maxSpeed = 30f;
  public float maxslope=30f;
  public float maxWallSlopeVarience=15f;
  public float jumpForce=10f;
  public int jumpsMax=2;
  private int jumpsLeft;
  public bool isGrounded;
  public bool isWalled;
  public int dashes;
  public float breakingStrength;
  public float dashStrength;
  public int dashesLeft;
  public float cooldown;
  public float dashCoolDown;
  Vector3 wallNormal;
  float defaultMaxSpeed;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    rb=GetComponent<Rigidbody>();
    jumpsLeft=jumpsMax;
    defaultMaxSpeed=maxSpeed;
    if (!physicsColider){
      physicsColider = GetComponent<Collider>();
    }
  }

  void OnCollisionStay(Collision other){
    for (int i = 0;i<other.contactCount;i++){
      if (Vector3.Angle(Vector3.up,other.contacts[i].normal)<=maxslope){
        jumpsLeft =jumpsMax;
        isGrounded = true;
        if (isGrounded&&isWalled){
          return;
        }
      } else if (Vector3.Angle(Vector3.up,other.contacts[i].normal)<=90+maxWallSlopeVarience&&Vector3.Angle(Vector3.up,other.contacts[i].normal)>=90-maxWallSlopeVarience){
        jumpsLeft =jumpsMax;
        isWalled = true;
        wallNormal=other.contacts[i].normal;
        if (isGrounded&&isWalled){
          return;
        }
      }
    }
  }

  void OnCollisionExit(Collision Other){
    isGrounded=false;
    isWalled=false;
  }

  // Update is called once per frame
  void Update(){
    float speed;
    float speeds=(new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z)).magnitude;
    
    if (dashesLeft<dashes){
      cooldown -= Time.deltaTime;
    } else {
      cooldown=dashCoolDown;
    }
    if (cooldown<=0){
      cooldown=dashCoolDown;
      dashesLeft++;
    }
    bool crouching = false;
    if (Input.GetKey(KeyCode.LeftControl)){
      float ply=0.5f;
      float newPlayer=Mathf.MoveTowards(player.localScale.y,ply,ply*Time.deltaTime*2);
      player.localScale = new Vector3(player.localScale.x, newPlayer, player.localScale.z);
      maxSpeed=1000000000f;
      physicsColider.material.dynamicFriction=0.1f;
      crouching=true;
    } else {
      float ply=1f;
      float newPlayer=Mathf.MoveTowards(player.localScale.y,ply,ply*Time.deltaTime*2);
      player.localScale = new Vector3(player.localScale.x, newPlayer, player.localScale.z);
      maxSpeed=defaultMaxSpeed;
      physicsColider.material.dynamicFriction=0.8f;
    }

    if (jumpsLeft>0&&Input.GetKeyDown(KeyCode.Space)){
      if (isWalled&& !isGrounded){
        rb.AddForce(wallNormal*60);
        rb.AddRelativeForce(Vector3.up*jumpForce, ForceMode.Impulse);
        isWalled=false;
      }else{
        rb.AddRelativeForce(Vector3.up*jumpForce, ForceMode.Impulse);
      }
      jumpsLeft--;
    }

    if (!crouching){

      if (Input.GetKeyDown(KeyCode.LeftAlt)&&Input.GetKey(KeyCode.W)&&dashesLeft>0){
        rb.AddRelativeForce(isGrounded ? (Vector3.forward*dashStrength) : (Vector3.forward*dashStrength/2.5f), ForceMode.Impulse);
        dashesLeft--;
      }
      if (Input.GetKeyDown(KeyCode.LeftAlt)&&Input.GetKey(KeyCode.A)&&dashesLeft>0){
        rb.AddRelativeForce(isGrounded ? (Vector3.left*dashStrength) : (Vector3.forward*dashStrength/2.5f), ForceMode.Impulse);
        dashesLeft--;
      }
      if (Input.GetKeyDown(KeyCode.LeftAlt)&&Input.GetKey(KeyCode.S)&&dashesLeft>0){
        rb.AddRelativeForce(isGrounded ? (Vector3.back*dashStrength) : (Vector3.forward*dashStrength/2.5f), ForceMode.Impulse);
        dashesLeft--;
      }
      if (Input.GetKeyDown(KeyCode.LeftAlt)&&Input.GetKey(KeyCode.D)&&dashesLeft>0){
        rb.AddRelativeForce(isGrounded ? (Vector3.right*dashStrength) : (Vector3.forward*dashStrength/2.5f), ForceMode.Impulse);
        dashesLeft--;
      }

      if (Input.GetKey(KeyCode.LeftShift)){
        speed=baseSpeed*2;
      } else {
        speed =baseSpeed;
      }

      if (Input.GetKey(KeyCode.W)&&(isGrounded||isWalled)){
        rb.AddRelativeForce(Vector3.forward * speed*Time.deltaTime, ForceMode.VelocityChange);
      } else if (Input.GetKey(KeyCode.W)&&!(isGrounded||isWalled)&&(speeds<maxSpeed*3f)){
        rb.AddRelativeForce(Vector3.forward * speed/2f*Time.deltaTime, ForceMode.VelocityChange);
      }
      if (Input.GetKey(KeyCode.A)&&(isGrounded||isWalled)){
        rb.AddRelativeForce(Vector3.left*speed*Time.deltaTime, ForceMode.VelocityChange);
      } else if (Input.GetKey(KeyCode.A)&&!(isGrounded||isWalled)&&(speeds<maxSpeed*3f)){
        rb.AddRelativeForce(Vector3.left * speed/2f*Time.deltaTime, ForceMode.VelocityChange);
      }
      if (Input.GetKey(KeyCode.S)&&(isGrounded||isWalled)){
        rb.AddRelativeForce(Vector3.back*speed*Time.deltaTime, ForceMode.VelocityChange);
      } else if (Input.GetKey(KeyCode.S)&&!(isGrounded||isWalled)&&(speeds<maxSpeed*3f)){
        rb.AddRelativeForce(Vector3.back * speed/2f*Time.deltaTime, ForceMode.VelocityChange);
      }
      if (Input.GetKey(KeyCode.D)&&(isGrounded||isWalled)){
        rb.AddRelativeForce(Vector3.right*speed*Time.deltaTime, ForceMode.VelocityChange);
      } else if (Input.GetKey(KeyCode.D)&&!(isGrounded||isWalled)&&(speeds<maxSpeed*3f)){
        rb.AddRelativeForce(Vector3.right * speed/2f*Time.deltaTime, ForceMode.VelocityChange);
      }
    }


    if (isGrounded){
      if (speeds >maxSpeed) {
        rb.AddForce(-(new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z)).normalized * ((speeds - maxSpeed)/maxSpeed)*breakingStrength, ForceMode.Acceleration);
      }
    } else if (isWalled){
      speeds = (new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z)).magnitude;
      if (speeds >maxSpeed) {
        rb.AddForce(-(new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z)).normalized * ((speeds - maxSpeed)/maxSpeed)*breakingStrength/2f, ForceMode.Acceleration);
      }
    }
  }

  void FixedUpdate(){
    rb.AddForce(Physics.gravity*0.5f*rb.mass);
    if (isWalled&&!isGrounded){
      rb.AddForce(Physics.gravity*-1.25f*rb.mass);
      rb.AddForce(-wallNormal*50f);
    }
  }
}

