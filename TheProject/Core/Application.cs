using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using ImGuiNET;

using TheProject.Render;

namespace TheProject.Core
{
    class Application : GameWindow
    {
        static Application mApplication;

        static public Application Get()
        {
            return mApplication;
        }

        public Application(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings) { }

        public static void Create(int width, int height, string title)
        {
            var nativeWindowSettings = new NativeWindowSettings();
            nativeWindowSettings.Size = new Vector2i(width, height);
            nativeWindowSettings.Title = title;

            nativeWindowSettings.Flags = ContextFlags.ForwardCompatible;

            mApplication = new Application(GameWindowSettings.Default, nativeWindowSettings);
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "../../../../");
            Console.WriteLine($"Current Directory : {Directory.GetCurrentDirectory()}");
        }

        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
             0.0f,  0.5f, 0.0f
        };

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        Shader _shader;

        ImGuiController mUiController;

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            _vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.EnableVertexAttribArray(0);

            _shader = new Shader("Assets/Shaders/vertex.glsl", "Assets/Shaders/fragment.glsl");

            _shader.Use();

            mUiController = new ImGuiController(ClientSize.X, ClientSize.Y);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);


            mUiController.Update(this, (float)e.Time);

            GL.ClearColor(new Color4(0, 32, 48, 255));
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();

            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            ImGuiNET.ImGui.ShowDemoWindow();

            mUiController.Render();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            mUiController.WindowResized(ClientSize.X, ClientSize.Y);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            GL.DeleteProgram(_shader.Handle);

            base.OnUnload();
        }

    }
}
