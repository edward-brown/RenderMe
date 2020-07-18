# RenderMe
RenderMe engine and a collection of projects made with it.
##### Please note that some projects might not compile / work. Changes to the core engine might not be reflected in older projects.

## Usage

Create a new instance of RenderEngine in Program.cs
Call UseShaders and then ShaderManager.OnLoad, pointed at a directory containing Shaders in the following format...
```
    Shaders
        Basic
            Basic.Fragment
            Basic.Vertex
        NotBasic
            NotBasic.fragment.glsl
            NotBasic.vertex.glsl
```

#### Example
```C#
RenderEngine engine = new RenderEngine(200, 200, "Blobs");

engine.UseShaders(@"./Shaders");
engine.ShaderManager.OnLoad();
```

Renderable objects should inherit from Entity2D, Or Entity3D.
They can be added to the engine like so...

```C#
var triangle = new Triangle(engine.ShaderManager.Shaders.FirstOrDefault(x => x.Name.ToLower() == "basic"));
triangle.OnLoad();
engine.Entities.Add(triangle);
```

#### Example Renderable Object
```C#
public class Triangle : Entity2D
{
    public Triangle(Shader shader) : base()
    {
        Vertices = new float[]
        {
            -0.5f, 0.0f, 0.0f, 1.0f,
            0.5f, 0.0f, 0.0f, 1.0f,
            0.0f, 0.5f, 0.0f, 1.0f
        };

        Shader = shader ?? throw new ArgumentNullException(nameof(shader));
    }

    public new void OnLoad()
    {
        base.OnLoad();

        var positionLocation = GL.GetAttribLocation(Shader.Program, "position");
        GL.VertexAttribPointer(positionLocation, 4, VertexAttribPointerType.Float, false, 0, 0);
        IsLoaded = true;
    }

    public override void Render()
    {
        // Bind
        base.Bind();

        // Draw
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }
}
```
