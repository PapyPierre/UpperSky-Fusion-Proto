using Custom_UI;
using Entity.Buildings;
using Entity.Military_Units;
using Fusion;
using NaughtyAttributes;
using Network;
using UnityEngine;

namespace Player
{
    public class PlayerController : NetworkBehaviour
    {
        private UIManager _uiManager;
        private NetworkManager _networkManager;
        
        [HideInInspector] public PlayerRessources ressources;

        [Header("Cameras")]
        public Camera myCam;

        [SerializeField, Required()] private GameObject minimapIndicator;
        
        [Networked] public PlayerRef MyPlayerRef {get; set; }
        [Networked] private TickTimer Delay { get; set; }

        public override void Spawned()
        {
            _networkManager = NetworkManager.Instance;
            _uiManager = UIManager.Instance;

            ressources = GetComponent<PlayerRessources>();

            _uiManager.connectionInfoTMP.text = "Is connected - " + Runner.GameMode;

            if (Object.HasInputAuthority)
            {
                _networkManager.thisPlayer = this;
                minimapIndicator.SetActive(true);
            }
            else
            {
                myCam.gameObject.SetActive(false);
            }
            
            transform.Rotate(Vector3.up, 180);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _uiManager.HideOpenedUI();
            }
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_SpawnNetworkObj(NetworkPrefabRef prefab, Vector3 position, Quaternion rotation, 
            RpcInfo info = default)
        {
            Runner.Spawn(prefab, position, rotation, Object.InputAuthority);
        }
        
        
        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_MoveNetworkObj(NetworkObject obj, Vector3 newPosition, Quaternion newRotation, 
            RpcInfo info = default)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position,newPosition, 0.5f);
            obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, newRotation, 0.5f);
        }
        
        private void OnDrawGizmos()
        {
            Debug.DrawRay(myCam.transform.position, myCam.transform.forward, Color.cyan);
        }
    }
}

