using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour {
	public float m_JumpTime = 0.2f;
	public float m_JumpSpeed = 0.2f;
	public float m_IncY = 0.5f;

	private float m_JumpValue = 0.0f;
	private bool m_Jumping = false, m_IsGrounded = true, m_Falling = false;
	//private Rigidbody m_RigidBody;
	private Transform m_Transform;

	private Vector3 m_GroundedPosition;

	void Start () {
		//m_RigidBody = GetComponent<Rigidbody>();
		m_Transform = GetComponent<Transform>();

		m_GroundedPosition = m_Transform.position;
	}


	void Update () {

	}


	private void FixedUpdate(){
		if (game.gameStarted == true) {
			if (Input.GetButton ("Jump") && m_IsGrounded && !m_Jumping && !m_Falling) {
				m_IsGrounded = false;
				m_Jumping = true;
			}

			if (m_Jumping) {
				m_JumpValue += Time.deltaTime * m_JumpSpeed;

				if (m_JumpValue < m_JumpTime) {
					float ratioAngle = (m_JumpSpeed * Time.deltaTime * 100) / m_JumpTime;
					m_Transform.transform.Rotate (0f, 0f, (360 / 100) * ratioAngle);

					Debug.Log ("ra= " + ratioAngle);

					Vector3 newPosition = m_Transform.transform.position;
					newPosition.y += m_IncY;

					m_Transform.transform.position = newPosition;
				} else {
					m_Jumping = false;
					m_Falling = true;
					m_JumpValue = 0.0f;
				}
			}

			if (m_Falling) {
				m_JumpValue += Time.deltaTime * m_JumpSpeed;

				if (m_JumpValue * 0.5f < m_JumpTime) {
					float ratioAngle = (m_JumpSpeed * Time.deltaTime * 100) / m_JumpTime;
					m_Transform.transform.Rotate (0f, 0f, (360 / 100) * ratioAngle);

					Debug.Log ("ra= " + ratioAngle);

					Vector3 newPosition = m_Transform.transform.position;
					newPosition.y -= m_IncY * 0.5f;

					m_Transform.transform.position = newPosition;
				} else {
					m_Jumping = false;
					m_Falling = false;
					m_JumpValue = 0.0f;
					m_Transform.rotation = Quaternion.identity;
					m_Transform.position = m_GroundedPosition;
					m_IsGrounded = true;
				}
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.tag == "obstacle") {
			m_Jumping = false;
			m_Falling = false;
			m_JumpValue = 0.0f;
			m_Transform.rotation = Quaternion.identity;
			m_Transform.position = m_GroundedPosition;
			m_IsGrounded = true;
			game.gameStarted = false;
		}
	}
}