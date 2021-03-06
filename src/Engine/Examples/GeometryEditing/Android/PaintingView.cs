using System;
using System.Runtime.InteropServices;
using System.Text;
using Android.Content;
using Android.Util;
using Fusee.Base.Core;
using Fusee.Math.Core;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using OpenTK.Platform.Android;

// Render a triangle using OpenGLES 3.0

namespace Fusee.Engine.Examples.GeometryEditing.Android {

	class PaintingView : AndroidGameView
	{
		int viewportWidth, viewportHeight;
		int program;
		float [] vertices;

		public PaintingView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Init ();
		}

		public PaintingView (IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
			: base (handle, transfer)
		{
			Init ();
		}

        [DllImport("shalib", EntryPoint = "NativeAdd")]
        public static extern int NativeAdd(int a, int b);

		void Init ()
		{
            var a = new float3(1, 2, 0);
            var b = new float3(3, 2, 1);
		    var c = a + b;
            Diagnostics.Log(c);

            var d = NativeAdd(2, 3);
            Diagnostics.Log(d);
        }

        // This method is called everytime the context needs
        // to be recreated. Use it to set any egl-specific settings
        // prior to context creation
        protected override void CreateFrameBuffer ()
		{
			GLContextVersion = GLContextVersion.Gles3_0;

			// the default GraphicsMode that is set consists of (16, 16, 0, 0, 2, false)
			try {
				Log.Verbose ("GLTriangle", "Loading with default settings");

				// if you don't call this, the context won't be created
				base.CreateFrameBuffer ();
				return;
			} catch (Exception ex) {
				Log.Verbose ("GLTriangle", "{0}", ex);
			}

			// this is a graphics setting that sets everything to the lowest mode possible so
			// the device returns a reliable graphics setting.
			try {
				Log.Verbose ("GLTriangle", "Loading with custom Android settings (low mode)");
				GraphicsMode = new AndroidGraphicsMode ();//0, 0, 0, 0, 0, false);

				// if you don't call this, the context won't be created
				base.CreateFrameBuffer ();
				return;
			} catch (Exception ex) {
				Log.Verbose ("GLTriangle", "{0}", ex);
			}
			throw new Exception ("Can't load egl, aborting");
		}

		// This gets called when the drawing surface has been created
		// There is already a GraphicsContext and Surface at this point,
		// following the standard OpenTK/GameWindow logic
		//
		// Android will only render when it refreshes the surface for
		// the first time, so if you don't call Run, you need to hook
		// up the Resize delegate or override the OnResize event to
		// get the updated bounds and re-call your rendering code.
		// This will also allow non-Run-loop code to update the screen
		// when the device is rotated.
		protected override void OnLoad (EventArgs e)
		{
			// This is completely optional and only needed
			// if you've registered delegates for OnLoad
			base.OnLoad (e);

			viewportHeight = Height; viewportWidth = Width;

			// Vertex and fragment shaders
			string vertexShaderSrc =  "attribute vec4 vPosition;    \n" + 
							  "void main()                  \n" +
							  "{                            \n" +
							  "   gl_Position = vPosition;  \n" +
							  "}                            \n";

			string fragmentShaderSrc = "precision mediump float;\n" +
		      					   "void main()                                  \n" +
		      					   "{                                            \n" +
		      					   "  gl_FragColor = vec4 (1.0, 0.0, 0.0, 1.0);  \n" +
		      					   "}                                            \n";

			int vertexShader = LoadShader (All.VertexShader, vertexShaderSrc );
			int fragmentShader = LoadShader (All.FragmentShader, fragmentShaderSrc );
			program = GL.CreateProgram();
			if (program == 0)
				throw new InvalidOperationException ("Unable to create program");

			GL.AttachShader (program, vertexShader);
			GL.AttachShader (program, fragmentShader);

			GL.BindAttribLocation (program, 0, "vPosition");
			GL.LinkProgram (program);

			int linked;
			GL.GetProgram (program, All.LinkStatus, out linked);
			if (linked == 0) {
				// link failed
				int length = 0;
				GL.GetProgram (program, All.InfoLogLength, out length);
				if (length > 0) {
					var log = new StringBuilder (length);
					GL.GetProgramInfoLog (program, length, out length, log);
					Log.Debug ("GL2", "Couldn't link program: " + log.ToString ());
				}

				GL.DeleteProgram (program);
				throw new InvalidOperationException ("Unable to link program");
			}

			RenderTriangle ();
		}

		int LoadShader (All type, string source)
		{
			int shader = GL.CreateShader (type);
			if (shader == 0)
				throw new InvalidOperationException ("Unable to create shader");

			int length = 0;
			GL.ShaderSource (shader, 1, new string [] {source}, (int[])null);
			GL.CompileShader (shader);

			int compiled = 0;
			GL.GetShader (shader, All.CompileStatus, out compiled);
			if (compiled == 0) {
				length = 0;
				GL.GetShader (shader, All.InfoLogLength, out length);
				if (length > 0) {
					var log = new StringBuilder (length);
					GL.GetShaderInfoLog (shader, length, out length, log);
					Log.Debug ("GL2", "Couldn't compile shader: " + log.ToString ());
				}

				GL.DeleteShader (shader);
				throw new InvalidOperationException ("Unable to compile shader of type : " + type.ToString ());
			}

			return shader;
		}

		void RenderTriangle ()
        {
            //vertices = new float[] {
            //        0.0f, 0.5f, 0.0f,
            //        -0.5f, -0.5f, 0.0f,
            //        0.5f, -0.5f, 0.0f
            //    };

            vertices = new float[] {
                    0.0f, 0.5f, 0.0f,
                    -0.5f, 0f, 0.0f,
                    0.5f, 0f, 0.0f,

                    0.0f, -0.5f, 0.0f,
                    0.5f, 0f, 0.0f,
                    -0.5f, 0f, 0.0f
            };

            GL.ClearColor (0.2f, 0.7f, 0.2f, 1);
			GL.Clear (ClearBufferMask.ColorBufferBit);

			GL.Viewport (0, 0, viewportWidth, viewportHeight);
			GL.UseProgram (program);

			// pin the data, so that GC doesn't move them, while used
			// by native code
			unsafe {
				fixed (float* pvertices = vertices) {
					GL.VertexAttribPointer (0, 3, All.Float, false, 0, new IntPtr (pvertices));
					GL.EnableVertexAttribArray (0);
					GL.DrawArrays (All.Triangles, 0, 6);
					GL.Finish ();
				}
			}

			SwapBuffers ();
		}

		// this is called whenever android raises the SurfaceChanged event
		protected override void OnResize (EventArgs e)
		{
			viewportHeight = Height;
			viewportWidth = Width;

			// the surface change event makes your context
			// not be current, so be sure to make it current again
			MakeCurrent ();
			RenderTriangle ();
		}
	}
}
