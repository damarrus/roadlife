using MadaoEcs;
using RaidHealer.UI;
using System;
using System.Collections;
using UnityEngine;

namespace RaidHealer.Logic
{
    public class EcsInitializer : MonoBehaviour
    {

        [SerializeField] private EcsTestBehaviour1 configLoader;

        public event Action OnInitialized = delegate { };
        public event Action<int> OnLoadProcessing = delegate { };

        IEnumerator Start()
        {

            NodesCollector.RegisterAllComponents();
            yield return new WaitForSeconds(0.05f);
            OnLoadProcessing.Invoke(20);
            NodesCollector.RegisterAllNodes();
            yield return new WaitForSeconds(0.05f);
            OnLoadProcessing.Invoke(30);
            yield return StartCoroutine(RegisterSystems());
            Ecs.RegisterAllEvents();
            configLoader.LoadConfigs();
            yield return new WaitForSeconds(0.05f);
            OnLoadProcessing.Invoke(80);

            Ecs.SortEventHandlers();
            configLoader.CreateTestInitialEntities();
            Ecs.InitStubEntity();
            yield return new WaitForSeconds(0.05f);
            OnLoadProcessing.Invoke(90);

            yield return new WaitForSeconds(0.05f);
            OnLoadProcessing.Invoke(100);
            yield return new WaitForSeconds(0.05f);
        }

        private IEnumerator RegisterSystems()
        {
            yield return new WaitForSeconds(0.05f);
            OnLoadProcessing.Invoke(40);       
            yield return new WaitForSeconds(0.05f);
            OnLoadProcessing.Invoke(50);
        }
    }
}
