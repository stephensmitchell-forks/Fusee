﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

namespace Fusee.Engine
{
    /// <summary>
    /// Use this class as a base class for implementing connectivity to whatever windows system you intend to support.
    /// Inherit from this class, make sure to call the constructor with the window handle to render on, implement the
    /// Run method and call the DoInit, DoUnload, DoRender and DoResize methods at appropriate incidences. Make sure
    /// that _width and _height are set to the new window size before calling DoResize.
    /// In addition you might have your connectivity class as well implement the <see cref="IInputImp"/> interface because
    /// often mouse and keyboard input are tied to the windows output.
    /// </summary>
    public abstract class RenderCanvasWindowImp : RenderCanvasImpBase, IRenderCanvasImp, IDisposable
    {
        internal IWindowInfo _wi;
        internal IGraphicsContext _context;
        internal GraphicsMode _mode;
        internal int _major, _minor;
        internal GraphicsContextFlags _flags;
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        /// <exception cref="System.NotImplementedException">Cannot (yet) set width on RenderContextWindowImp</exception>
        public int Width
        {
            get { return _width; }
            set { throw new NotImplementedException("Cannot (yet) set width on RenderContextWindowImp");}
        }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        /// <exception cref="System.NotImplementedException">Cannot (yet) set height on RenderContextWindowImp</exception>
        public int Height        
        {
            get { return _height; }
            set { throw new NotImplementedException("Cannot (yet) set height on RenderContextWindowImp");}
        }

        /// <summary>
        /// Gets or sets the caption(title of the window).
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public string Caption { get; set; }
        private double _lastTimeTick;
        private double _deltaFrameTime;
        private static Stopwatch _daWatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderCanvasWindowImp"/> class.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        public RenderCanvasWindowImp(IntPtr windowHandle)
        {
            // _mode = GraphicsMode.Default;
            bool antiAliasing = true;
            _mode = new GraphicsMode(32, 24, 0, (antiAliasing) ? 8 : 0);
            _major = 1;
            _minor = 0;
            _flags = GraphicsContextFlags.Default;
            _wi = Utilities.CreateWindowsWindowInfo(windowHandle);

            try
            {
                _context = new GraphicsContext(_mode, _wi, _major, _minor, _flags);
            }
            catch
            {
                antiAliasing = false;
                _mode = new GraphicsMode(32, 24, 0, (antiAliasing) ? 8 : 0);
                _context = new GraphicsContext(_mode, _wi, _major, _minor, _flags);
            }

            _context.MakeCurrent(_wi);
            ((IGraphicsContextInternal)_context).LoadAll();

            GL.ClearColor(Color.MidnightBlue);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            // Use VSync!
            _context.SwapInterval = 1;
            _lastTimeTick = Timer;
        }

        /// <summary>
        /// Gets the delta time.
        /// The delta time is the time that was required to render the last frame in milliseconds.
        /// This value can be used to determine the frames per second of the application.
        /// </summary>
        /// <value>
        /// The delta time in milliseconds.
        /// </value>
        public double DeltaTime
        {
            get
            {
                return _deltaFrameTime;
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [vertical synchronize].
        /// This option is used to reduce "Glitches" during rendering.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [vertical synchronize]; otherwise, <c>false</c>.
        /// </value>
        public bool VerticalSync
        {
            get { return _context.SwapInterval == 1; }
            set { _context.SwapInterval = (value) ? 1 : 0; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable blending].
        /// Blending is used to display transparent graphics.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable blending]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableBlending { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [fullscreen] is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fullscreen]; otherwise, <c>false</c>.
        /// </value>
        public bool Fullscreen { get; set; }

        /// <summary>
        /// Gets the timer.
        /// The timer value can be used to measure time that passed since the first call of this property.
        /// </summary>
        /// <value>
        /// The timer.
        /// </value>
        public static double Timer
        {
            get
            {
                if (_daWatch == null)
                {
                    _daWatch = new Stopwatch();
                    _daWatch.Start();
                }
                return ((double)_daWatch.ElapsedTicks) / ((double)Stopwatch.Frequency);
            }
        }

        /// <summary>
        /// Presents the rendered result of this instance. The rendering buffers are flushed and the deltatime is recalulated.
        /// Call this function after rendering.
        /// </summary>
        public void Present()
        {
            // Recalculate time tick.
            double newTick = Timer;
            _deltaFrameTime = newTick - _lastTimeTick;
            _lastTimeTick = newTick;

            // _context.MakeCurrent(_wi);
            _context.SwapBuffers();
        }

        /// <summary>
        /// Runs this application instance.
        /// </summary>
        public abstract void Run();

        private bool _disposed = false;

        //Implement IDisposable.
        /// <summary>
        /// Releases this instance for garbage collection. Do not call this method in frequent updates because of performance reasons.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                _context.Dispose();
                _context = null;
                _wi.Dispose();
                _wi = null;
                _disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~RenderCanvasWindowImp()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

    }

    /// <summary>
    /// This is a default render canvas implementation creating its own rendering window.
    /// </summary>
    public class RenderCanvasImp : RenderCanvasImpBase, IRenderCanvasImp
    {
        public int Width
        {
            get { return _width; }
            set
            {
                _gameWindow.Width = value;
                _width = value;
                ResizeWindow();
            }
        }

        /// <summary>
        /// Gets or sets the height in pixel units.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get { return _height; }
            set
            {
                _gameWindow.Height = value;
                _height = value;
                ResizeWindow();
            }
        }

        private void ResizeWindow()
        {
            var width2 = _width / 2;
            var height2 = _height / 2;

            var scHeight2 = Screen.PrimaryScreen.Bounds.Height / 2;
            var scWidth2 = Screen.PrimaryScreen.Bounds.Width / 2;

            _gameWindow.Bounds = new Rectangle(scWidth2 - width2, scHeight2 - height2, _width, _height);
        }

        /// <summary>
        /// Gets or sets the caption(title of the window).
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public string Caption
        {
            get { return (_gameWindow == null) ? "" : _gameWindow.Title; }
            set { if (_gameWindow != null) _gameWindow.Title = value; }
        }

        /// <summary>
        /// Gets the delta time.
        /// The delta time is the time that was required to render the last frame in milliseconds.
        /// This value can be used to determine the frames per second of the application.
        /// </summary>
        /// <value>
        /// The delta time in milliseconds.
        /// </value>
        public double DeltaTime
        {
            get
            {
                if (_gameWindow != null)
                    return _gameWindow.DeltaTime;
                return 0.01f;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [vertical synchronize].
        /// This option is used to reduce "Glitches" during rendering.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [vertical synchronize]; otherwise, <c>false</c>.
        /// </value>
        public bool VerticalSync
        {
            get { return (_gameWindow != null) && _gameWindow.Context.SwapInterval == 1; }
            set { if (_gameWindow != null) _gameWindow.Context.SwapInterval = (value) ? 1 : 0; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable blending].
        /// Blending is used to render transparent objects.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable blending]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableBlending
        {
            get { return _gameWindow.Blending; }
            set { _gameWindow.Blending = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [fullscreen] is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fullscreen]; otherwise, <c>false</c>.
        /// </value>
        public bool Fullscreen
        {
            get { return (_gameWindow.WindowState == WindowState.Fullscreen); }
            set { _gameWindow.WindowState = (value) ? WindowState.Fullscreen : WindowState.Normal; }
        }

        /// <summary>
        /// Gets a value indicating whether [focused].
        /// This property is used to identify if this application is the active window of the user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [focused]; otherwise, <c>false</c>.
        /// </value>
        public bool Focused
        {
            get { return _gameWindow.Focused; }
        }

        internal RenderCanvasGameWindow _gameWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderCanvasImp"/> class.
        /// </summary>
        public RenderCanvasImp()
        {
            const int width = 1280;
            var height = System.Math.Min(Screen.PrimaryScreen.Bounds.Height - 100, 720);

            try
            {
                _gameWindow = new RenderCanvasGameWindow(this, width, height, true);
            }
            catch
            {
                _gameWindow = new RenderCanvasGameWindow(this, width, height, false);
            }
        }

        /// <summary>
        /// Presents this application instance. Call this function after rendering to show the final image. 
        /// After Present is called the render buffers get flushed.
        /// </summary>
        public void Present()
        {
            if (_gameWindow != null)
                _gameWindow.SwapBuffers();
        }

        /// <summary>
        /// Runs this application instance.
        /// </summary>
        public void Run()
        {
            if (_gameWindow != null)
                _gameWindow.Run(30.0, 0.0);
        }
    }

    public class RenderCanvasImpBase
    {
        protected internal int _width;
        protected internal int _height;

        /// <summary>
        /// Occurs when [initialize].
        /// </summary>
        public event EventHandler<InitEventArgs> Init;
        /// <summary>
        /// Occurs when [un load].
        /// </summary>
        public event EventHandler<InitEventArgs> UnLoad;
        /// <summary>
        /// Occurs when [render].
        /// </summary>
        public event EventHandler<RenderEventArgs> Render;
        /// <summary>
        /// Occurs when [resize].
        /// </summary>
        public event EventHandler<ResizeEventArgs> Resize;

        internal protected void DoInit()
        {
            if (Init != null)
                Init(this, new InitEventArgs());
        }

        internal protected void DoUnLoad()
        {
            if (UnLoad != null)
                UnLoad(this, new InitEventArgs());
        }

        internal protected void DoRender()
        {
            if (Render != null)
                Render(this, new RenderEventArgs());
        }

        internal protected void DoResize()
        {
            if (Resize != null)
                Resize(this, new ResizeEventArgs());
        }
    }

    class RenderCanvasGameWindow : GameWindow
    {
        private RenderCanvasImp _renderCanvasImp;
        private double _deltaTime;

        /// <summary>
        /// Gets the delta time.
        /// The delta time is the time that was required to render the last frame in milliseconds.
        /// This value can be used to determine the frames per second of the application.
        /// </summary>
        /// <value>
        /// The delta time in milliseconds.
        /// </value>
        public double DeltaTime
        {
            get { return _deltaTime; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [blending].
        /// Blending is used to render transparent objects.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [blending]; otherwise, <c>false</c>.
        /// </value>
        public bool Blending
        {
            get { return GL.IsEnabled(EnableCap.Blend); }
            set
            {
                if (value)
                {
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                }
                else
                {
                    GL.Disable(EnableCap.Blend);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderCanvasGameWindow"/> class.
        /// </summary>
        /// <param name="renderCanvasImp">The render canvas implementation.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="antiAliasing">if set to <c>true</c> [anti aliasing] is on.</param>
        public RenderCanvasGameWindow(RenderCanvasImp renderCanvasImp, int width, int height, bool antiAliasing)
            : base(width, height, new GraphicsMode(32, 24, 0, (antiAliasing) ? 8 : 0) /*GraphicsMode.Default*/, "Fusee Engine")
        {
            _renderCanvasImp = renderCanvasImp;
        }

        protected override void OnLoad(EventArgs e)
        {
            // Check for necessary capabilities:
            string version = GL.GetString(StringName.Version);

            int major = (int)version[0];
            // int minor = (int)version[2];

            if (major < 2)
            {
                MessageBox.Show("You need at least OpenGL 2.0 to run this example. Aborting.", "GLSL not supported",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Exit();
            }

            GL.ClearColor(Color.MidnightBlue);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            // Use VSync!
            Context.SwapInterval = 1;

            _renderCanvasImp.DoInit();
        }

        protected override void OnUnload(EventArgs e)
        {
            _renderCanvasImp.DoUnLoad();
            // if (_renderCanvasImp != null)
            //     _renderCanvasImp.Dispose();      
        }


        protected override void OnResize(EventArgs e)
        {
            if (_renderCanvasImp != null)
            {
                _renderCanvasImp._width = Width;
                _renderCanvasImp._height = Height;
                _renderCanvasImp.DoResize();
            }

            /*
            GL.Viewport(0, 0, Width, Height);

            float aspect_ratio = Width / (float)Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
             * */
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[OpenTK.Input.Key.Escape])
                this.Exit();

            if (Keyboard[OpenTK.Input.Key.F11])
                if (WindowState != WindowState.Fullscreen)
                    WindowState = WindowState.Fullscreen;
                else
                    WindowState = WindowState.Normal;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            _deltaTime = e.Time;
            if (_renderCanvasImp != null)
            {
                _renderCanvasImp.DoRender();
            }
        }
    }
}