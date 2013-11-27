﻿using System;
using Fusee.Math;
using Fusee.Engine;


namespace Fusee.Engine
{
    public interface IDynamicWorldImp
    {

        IRigidBodyImp AddRigidBody(float mass, float3 worldTransform, int[] meshTriangles, float3[] meshVertices, /* shape, */ float3 intertia);
        IRigidBodyImp GetRigidBody(int i);
        int StepSimulation(float timeSteps, int maxSubSteps, float fixedTimeSteps);
        int NumberRigidBodies();

        IPoint2PointConstraintImp AddPoint2PointConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float3 pivotInA, float3 pivotInB);
        IPoint2PointConstraintImp GetConstraint(int i);

        IHingeConstraintImp AddHingeConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float3 pivotInA, float3 pivotInB, float3 axisInA, float3 AxisInB, bool useReferenceFrameA);

        ISliderConstraintImp AddSliderConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float4x4 frameInA, float4x4 frameInB, bool useLinearReferenceFrameA);

        int NumberConstraints();
    }
}
