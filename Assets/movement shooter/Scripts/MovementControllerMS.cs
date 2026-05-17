using UnityEngine;

public class MovementControllerMS : MonoBehaviour
{
    
  public Rigidbody rb;
  public float baseSpeed=100f;
  public float maxSpeed = 30f;
  public float maxslope=30f;
  public float maxWallSlopeVarience=15f;
  public float jumpForce=10f;
  public int jumpsMax=2;
  private int jumpsLeft;
  public bool isGrounded;
  public bool isWalled;
  Vector3 wallNormal;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    rb=GetComponent<Rigidbody>();
    jumpsLeft=jumpsMax;
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
  void Update()
  {
    float speed;
    
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

    if (Input.GetKey(KeyCode.LeftShift)){
      speed=baseSpeed*2;
    } else {
      speed =baseSpeed;
    }
    if (Input.GetKey(KeyCode.W)){
      rb.AddRelativeForce(Vector3.forward * speed*Time.deltaTime, ForceMode.VelocityChange);
    }
    if (Input.GetKey(KeyCode.A)){
      rb.AddRelativeForce(Vector3.left*speed*Time.deltaTime, ForceMode.VelocityChange);
    }
    if (Input.GetKey(KeyCode.S)){
      rb.AddRelativeForce(Vector3.back*speed*Time.deltaTime, ForceMode.VelocityChange);
    }
    if (Input.GetKey(KeyCode.D)){
      rb.AddRelativeForce(Vector3.right*speed*Time.deltaTime, ForceMode.VelocityChange);
    }


    Vector3 horizontal = Vector3.ClampMagnitude(new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z),maxSpeed);
    rb.linearVelocity=new Vector3(horizontal.x,rb.linearVelocity.y,horizontal.z);
  }

  void FixedUpdate(){
    rb.AddForce(Physics.gravity*0.5f*rb.mass);
    if (isWalled&& !isGrounded){
      rb.AddForce(Physics.gravity*-1.5f*rb.mass);
      rb.AddForce(-wallNormal*50f);
    }
  }
}

