﻿using Fusee.Math;

namespace Fusee.SceneManagement
{
    /// <summary>
    /// This class is derived from Light and set's a Spotlight in the scene.
    /// </summary>
    public class SpotLight : Light
    {

        #region Fields

        private float3 _direction;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotLight"/> class. Position, direction, color and the channel are needed.
        /// </summary>
        /// <param name="position">The position of the spotlight.</param>
        /// <param name="direction">The direction of the spotlight.</param>
        /// <param name="color">The color of the spotlight(Red, Green, Blue, Alpha).</param>
        /// <param name="channel">The memory space of the light(0 - 7).</param>
        public SpotLight(float3 position, float3 direction, float4 color, int channel) 
        {
            _type = LightType.Spot;
            _position = position;
            _direction = direction;
            _color = color;
            _channel = channel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotLight"/> class. Only the channel is needed.
        /// </summary>
        /// <param name="channel">The memory space of the light(0 - 7).</param>
        public SpotLight( int channel) 
        {
            _type = LightType.Spot;
            _position = new float3(0,0,0);
            _direction = new float3(0,-1,0);
            _color = new float4(0.5f, 0.5f, 0.5f, 0.5f);
            _channel = channel;
        }
        #endregion

        #region Members

        /// <summary>
        /// Add's a Spotlight to the lightqueue.
        /// </summary>
        /// <param name="sceneVisitorRendering">The SceneVisitorRendering instance.</param>
        public void TraverseForRendering(SceneVisitorRendering sceneVisitorRendering)
        {
            if (SceneEntity != null)
            {
                _position = SceneEntity.transform.GlobalPosition;
            }
            sceneVisitorRendering.AddLightSpot(_position, _direction, _color, _type, _channel );
        }

        #endregion

        #region Overrides
        /// <summary>
        /// Passes the Component to the SceneVisitor which decides what to do with that Component.
        /// </summary>
        /// <param name="sv">The SceneVisitor.</param>
        public override void Accept(SceneVisitor sv)
        {
            sv.Visit((SpotLight)this);
        }
        #endregion
    }
}
