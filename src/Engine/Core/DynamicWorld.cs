﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Fusee.Math;


namespace Fusee.Engine
{
    public class DynamicWorld
    {
        internal /*readonly*/ IDynamicWorldImp _dwi;


        public DynamicWorld()
        {
            _dwi = ImpFactory.CreateIDynamicWorldImp();
        }

        public RigidBody AddRigidBody(float mass, float3 worldTransform, Mesh mesh,/* shape,*/ float3 inertia)
        {

            var meshTrianglesCount = mesh.Triangles.Length;
            int [] meshTrianglesArray = new int[meshTrianglesCount];
            for (int c = 0; c < meshTrianglesCount; c++)
            {
                meshTrianglesArray[c] = Convert.ToInt32(mesh.Triangles[c]);
            }

            int meshVerteciesCount = mesh.Vertices.Length;
            float3[] meshVerteciesArray = new float3[meshVerteciesCount];
            meshVerteciesArray = mesh.Vertices;

            IRigidBodyImp rbi = _dwi.AddRigidBody(mass, worldTransform, meshTrianglesArray, meshVerteciesArray, /* shape, */inertia);
       
            var retval = new RigidBody();
            retval.Mesh = mesh;
            retval._iRigidBodyImp = rbi;
            rbi.UserObject = retval;

            return retval;
        }

        public int StepSimulation(float timeSteps, int maxSubSteps, float fixedTimeSteps)
        {
            return _dwi.StepSimulation(timeSteps, maxSubSteps, fixedTimeSteps);
        }

        public RigidBody GetRigidBody(int i)
        {
            var rbi = _dwi.GetRigidBody(i);
            var retval = (RigidBody) rbi.UserObject;
            return retval;
        }

        public int NumberRigidBodies()
        {
            var number = _dwi.NumberRigidBodies();
            return number;
        }

        public Point2PointConstraint AddPoint2PointConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, float3 pivotInA, float3 pivotInB)
        {
            IPoint2PointConstraintImp ip2pci = _dwi.AddPoint2PointConstraint(rigidBodyA._iRigidBodyImp, rigidBodyB._iRigidBodyImp, pivotInA, pivotInB);
            var retval = new Point2PointConstraint();
            retval._iP2PConstraintImp = ip2pci;
            ip2pci.UserObject = retval;
            return retval;
        }

        public Point2PointConstraint GetConstraint(int i)
        {
            //Point2PointConstraint tp2pci = _dwi.GetConstraint(i).UserObject;
            var retval = (Point2PointConstraint)_dwi.GetConstraint(i).UserObject;
            return retval;
        }

        public HingeConstraint AddHingeConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, float3 pivotInA, float3 pivotInB, float3 axisInA, float3 axisInB, bool useReferenceFrameA = false)
        {
            IHingeConstraintImp ip2pci = _dwi.AddHingeConstraint(rigidBodyA._iRigidBodyImp, rigidBodyB._iRigidBodyImp, pivotInA, pivotInB, axisInA, axisInB, useReferenceFrameA);
            var retval = new HingeConstraint();
            retval._iHConstraintImp = ip2pci;
            ip2pci.UserObject = retval;
            return retval;
        }


        public int NumberConstraints()
        {
            var number = _dwi.NumberConstraints();
            return number;
        }
    }
}
