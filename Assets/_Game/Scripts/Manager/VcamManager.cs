using UnityEngine;
using Cinemachine;

public class VcamManager : Singleton<VcamManager>
{
    public CinemachineVirtualCamera vcam;
    public Transform targetObject; // Đối tượng bạn muốn theo dõi
    public float scaleFactor = 1.0f; // Tỉ lệ thay đổi offset theo trục Y
    public float zScaleFactor = 1.0f; // Tỉ lệ thay đổi offset theo trục Z

    private CinemachineTransposer transposer;
    private Vector3 initialOffset;
    public CinemachineBrain cinemachineBrain;
    private void Start()
    {
        // Lấy thành phần CinemachineTransposer của máy ảnh
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        // Lưu giá trị offset ban đầu
        initialOffset = transposer.m_FollowOffset;
    }

    private void LateUpdate()
    {
        if (GameManager.IsState(GameState.MainMenu))
        {
            Vector3 targetRotation = new Vector3(0f, gameObject.transform.rotation.y, gameObject.transform.rotation.z);

            transform.rotation = Quaternion.Euler(targetRotation);
            initialOffset = new Vector3(0.01f, 1.85f, -8.85f);
        }
        else
        {
            Vector3 targetRotation = new Vector3(50f, gameObject.transform.rotation.y, gameObject.transform.rotation.z);

            transform.rotation = Quaternion.Euler(targetRotation);
            initialOffset = new Vector3(0, 15.7f, -12.7f);
        }
        if (targetObject != null)
        {
            // Lấy tỉ lệ thay đổi theo trục Y và Z dựa trên tỉ lệ thay đổi kích thước
            float yScale = targetObject.localScale.y;
            float zScale = targetObject.localScale.z;

            // Cập nhật giá trị offset theo trục Y và Z dựa trên tỉ lệ thay đổi
            Vector3 currentOffset = transposer.m_FollowOffset;
            currentOffset.y = initialOffset.y * scaleFactor * yScale;
            currentOffset.z = initialOffset.z * zScaleFactor * zScale;
            transposer.m_FollowOffset = currentOffset;
        }
    
    }
}