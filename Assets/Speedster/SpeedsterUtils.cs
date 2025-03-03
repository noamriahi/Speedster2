using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Speedsters
{
    public static class SpeedsterUtils
    {
        static SpeedsterManager _speedsterManager;
        #region Transform translate
        public static void TranslateSpeedster(this Transform currentTransform, Vector3 translation)
        {
            currentTransform.TranslateSpeedster(translation, Space.Self);
        }       
        public static void TranslateSpeedster(this Transform currentTransform, float x, float y, float z)
        {
            currentTransform.TranslateSpeedster(x, y, z, Space.Self);
        }
        public static void TranslateSpeedster(this Transform currentTransform, float x, float y, float z, Space relativeTo)
        {
            var newVector = new Vector3(x, y, z);
            currentTransform.TranslateSpeedster(newVector, relativeTo);
        }
        public static void TranslateSpeedster(this Transform currentTransform, Vector3 translation, Space space)
        {

            GetSpeedsterManager();
            if (_speedsterManager == null) return;
            float speed = _speedsterManager.GetSpeedsterSpeed();
            currentTransform.Translate(translation * speed, space);
        }
        #endregion
        #region Physics AddForce
        public static void AddForceSpeedster(this Rigidbody rigidbody, Vector3 force)
        {
            rigidbody.AddForceSpeedster(force,ForceMode.Force);
        }
        public static void AddForceSpeedster(this Rigidbody rigidbody, Vector3 force, ForceMode mode)
        {
            GetSpeedsterManager();
            if (_speedsterManager == null) return;
            float speed = _speedsterManager.GetSpeedsterSpeed();
            rigidbody.AddForce(force * speed);
        }
        public static void AddForceSpeedster(this Rigidbody rigidbody, float x, float y, float z)
        {
            rigidbody.AddForceSpeedster(x, y, z, ForceMode.Force);
        }
        public static void AddForceSpeedster(this Rigidbody rigidbody, float x, float y, float z, ForceMode mode)
        {
            Vector3 force = new Vector3(x, y, z);
            rigidbody.AddForceSpeedster(force, mode);
        }
        #endregion
        #region NavmeshAgent
        public static void MoveToSpeedster(this NavMeshAgent agent,Vector3 offset)
        {
            GetSpeedsterManager();
            if (_speedsterManager == null)
                return;
            _speedsterManager.TryAddNewAgent(agent.GetInstanceID(), agent.speed);
            float speed = _speedsterManager.GetAgentSpeed(agent.GetInstanceID());

            agent.speed = speed;
            agent.destination = offset;
        }
        #endregion
        public static void ChangeAnimationSpeed(this Animator animator)
        {
            GetSpeedsterManager();
            if (_speedsterManager == null)
                return;
            float speed = _speedsterManager.GetSpeedsterSpeed();
            if (_speedsterManager.IsInSpeedMode())
            {
                animator.speed = 1f / speed;
                return;
            }
            animator.speed = 1;
        }
        static SpeedsterManager GetSpeedsterManager()
        {
            if(_speedsterManager == null || _speedsterManager.IsDestroyed())
            {
                _speedsterManager = GameObject.FindAnyObjectByType<SpeedsterManager>();
                if (_speedsterManager == null)
                {
                    Debug.LogError($"There is not speedsterManager in the scene, you must to create one to use a speedster method");
                    return null;
                }
            }
            return _speedsterManager;
        }
    }
}
