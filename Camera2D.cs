using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten
{
    public class Camera2D
    {
        private static GraphicsDevice _graphics;

        public Vector3 Position = new Vector3(0, 0, 0);
        public Matrix Projection { get; private set; }
        public Matrix View { get; private set; }

        public float Width { get; } = 30f;
        public float Height { get; private set; }

        public Camera2D(GraphicsDevice graphics)
        {
            _graphics = graphics;
            Height = Width / _graphics.Viewport.AspectRatio;

            Update();
        }

        public void Update()
        {
            var vp = _graphics.Viewport;
            Projection = Matrix.CreateOrthographic(Width, Width/vp.AspectRatio, 0f, -1f);
            View = Matrix.CreateLookAt(Position, Position + Vector3.Forward, Vector3.Up);
        }

        private void UpdateProjection()
        {
            var vp = _graphics.Viewport;
            Projection = Matrix.CreateOrthographic(Width, Width/vp.AspectRatio, 0f, -1f);
        }

        private void UpdateView()
        {
            View = Matrix.CreateLookAt(Position, Position + Vector3.Forward, Vector3.Up);
        }

        public Vector2 ConvertScreenToWorld(Vector2 location)
        {
            Vector3 t = new Vector3(location, 0);
            t = _graphics.Viewport.Unproject(t, Projection, View, Matrix.Identity);
            return new Vector2(t.X, t.Y);
        }

        public Vector2 ConvertScreenToWorld(Point location)
        {
            Vector3 t = new Vector3(location.X, location.Y, 0);
            t = _graphics.Viewport.Unproject(t, Projection, View, Matrix.Identity);
            return new Vector2(t.X, t.Y);
        }

        public Vector2 ConvertWorldToScreen(Vector2 location)
        {
            Vector3 t = new Vector3(location, 0);
            t = _graphics.Viewport.Project(t, Projection, View, Matrix.Identity);
            return new Vector2(t.X, t.Y);
        }
    }
}