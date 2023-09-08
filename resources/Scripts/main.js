var shader = new Shader("Shaders/v.glsl", "Shaders/f.glsl");

var program = new Program();

var camera = new Camera(10, Vector.Vector2(1, 1));

var texture = new Texture("Textures/red.png", TextureUnit.Texture0, TextureWrapMode.Repeat, TextureMinFilter.Nearest, TextureMagFilter.Nearest);
var texture2 = new Texture("Textures/tileset/tile16.png", TextureUnit.Texture0, TextureWrapMode.Repeat, TextureMinFilter.Nearest, TextureMagFilter.Nearest);

var bgl1 = new Background("Textures/background.jpg", Vector.Vector2(0, 0), Vector.Vector2(Window.Size.X, Window.Size.Y), camera);
var bgl2 = new Background("Textures/tileset/clouds background.png", Vector.Vector2(0 - 100, 0), Vector.Vector2(Window.Size.X + 100, Window.Size.Y), camera);
var bgl3 = new Background("Textures/tileset/clouds foreground.png", Vector.Vector2(0, 0), Vector.Vector2(Window.Size.X, Window.Size.Y), camera);

var sprite1 = new Sprite(
    "spritegg",
    Vector.Vector2(400, 200),
    Vector.Vector2(200, 200),
    shader,
    program,
    texture,
    camera,
    true
);
var floor = new Sprite(
    "spritehh",
    Vector.Vector2(0, 600),
    Vector.Vector2(Window.Size.X, 400),
    shader,
    program,
    texture2,
    camera
);

sprite1.attachPhysics(new PhysicsObject(10));

sprite1.Use();
program.SetVector4("lightColor", Vector.Vector4(1, 1, 1, 1));
floor.Use();

floor.usePhysics(false);

SpriteRegister.addSprite(sprite1);
SpriteRegister.addSprite(floor);

SpriteRegister.setGlobalFriction(0.1);