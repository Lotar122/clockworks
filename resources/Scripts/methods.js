function setup()
{
    bgl1.Use();
    bgl2.Use();
    bgl3.Use();
    console.log("Setup");
}
var increase = true;
function loop()
{
    bgl1.render();
    bgl2.render();
    bgl3.render();
    if(increase)
    {
        bgl2.position = Vector.Vector2(bgl2.position.X + 0.3, bgl2.position.Y);
        bgl2.refreshVertices();
    }
    else
    {
        bgl2.position = Vector.Vector2(bgl2.position.X - 0.3, bgl2.position.Y);
        bgl2.refreshVertices();
    }
    if(bgl2.position.X > 0)
    {
        increase = false;
    }
    if(bgl2.position.X < -100)
    {
        increase = true;
    }
    if(KeyboardRegister.Key.A)
    {
        sprite1.phys.setForce(Vector.Vector2(-5, sprite1.phys.force.Y));
    }
    else if(KeyboardRegister.Key.D)
    {
        sprite1.phys.setForce(Vector.Vector2(5, sprite1.phys.force.Y));
    }
    else if(KeyboardRegister.Key.Space)
    {
        sprite1.phys.setForce(Vector.Vector2(sprite1.phys.force.X, -10));
    }
    else
    {
        sprite1.phys.setForce(Vector.Vector2(0, 0));
    }
}
function onResize()
{
    program.SetMatrix4x4("projection", camera.GetProjectionMatrix());
    //programl.SetMatrix4x4("projection", camera.GetProjectionMatrix());
}