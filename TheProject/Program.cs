using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Text.Json;
using System.IO;
using System;
using TheProject.Core;

namespace TheProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Create(1920, 1080, "The Project");

            Application.Get().Run();
        }
    }
}
