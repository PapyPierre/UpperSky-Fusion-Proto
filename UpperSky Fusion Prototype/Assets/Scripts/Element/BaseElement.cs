using System.Collections.Generic;
using Element.Entity.Buildings;
using Element.Entity.Military_Units;
using Element.Island;
using Entity.Military_Units;
using Fusion;
using Player;
using UnityEngine;
using UnityEngine.UI;
using UserInterface;

namespace Element
{
    /// <summary>
    /// Elements include Islands, Buildings and Units
    /// </summary>

    public class BaseElement : NetworkBehaviour
    {
        protected UnitsManager UnitsManager;
        protected GameManager GameManager;
        protected UIManager UIManager;

        #region Ownership
        [field: SerializeField, Header("Ownership")]
        [Networked(OnChanged = nameof(OwnerChanged))] public PlayerController Owner { get; set; }
        [SerializeField] private  List<MeshRenderer> meshToColor;
        #endregion
        
        public enum ElementType
        {
            None,
            Island,
            Building,
            Unit
        }
        
        [SerializeField, Space] protected GameObject graphObject;
        [SerializeField, Space] protected GameObject canvas;
        [SerializeField, Space] protected Image minimapIcon;

        private static void OwnerChanged(Changed<BaseElement> changed)
        {
            if (changed.Behaviour.Owner == null) return;

            foreach (var meshRenderer in changed.Behaviour.meshToColor)
            {
                meshRenderer.material.color *= changed.Behaviour.Owner.myColor;
            }

            if (changed.Behaviour.minimapIcon != null)
            {
                changed.Behaviour.minimapIcon.color = changed.Behaviour.Owner.myColor;
            }
        }
        
        public override void Spawned()
        {
            GameManager = GameManager.Instance;
            UnitsManager = UnitsManager.Instance;
            UIManager = UIManager.Instance;
        }
        
        #region Selection
        protected virtual void OnMouseEnter()
        {
         
        }
        
        protected virtual void OnMouseExit()
        {
        
        }
        
        #endregion
    }
}
