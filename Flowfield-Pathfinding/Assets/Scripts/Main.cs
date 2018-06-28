﻿using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Main : MonoBehaviour
{
	public int m_activeSteerData = 0;
	public InitializationData m_InitData;
	public AgentSpawnData m_AgentSpawnData;
	public AgentSteerData[] m_AgentSteerData;
	public static Main Instance;

	public static AgentSteerParams ActiveSteeringParams
	{
		get { return Instance.m_AgentSteerData[Instance.m_activeSteerData].m_Data; }
	}

	public static AgentSpawnData ActiveSpawnParams
	{
		get { return Instance.m_AgentSpawnData; }
	}

	public static InitializationData ActiveInitParams
	{
		get { return Instance.m_InitData; }
	}

	public static NativeArray<float3> TerrainFlow
	{
		get { return Instance.m_InitData.m_terrainFlow; }
	}

	public static NativeArray<float> TerrainHeight
	{
		get { return Instance.m_InitData.m_heightmap; }
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Initialize()
	{
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        Manager.Archetype.Initialize(entityManager);

        var debugEntity = entityManager.CreateEntity(Manager.Archetype.DebugHeatmapType);
        entityManager.SetSharedComponentData(debugEntity, new DebugHeatmap.Component());

		Manager.Archetype.CreateInputSystem(entityManager);
		
		Instance = FindObjectOfType<Main>();
		Instance.m_InitData.Initalize();
	}

	private void OnDisable()
	{
		m_InitData.Shutdown();
	}

	private void LateUpdate()
	{
		//m_InitData.LateUpdate();
	}

}
