#version 330

out vec4 outputColor;

uniform vec4 lightColor;

void main()
{
    outputColor = lightColor;
    if(lightColor.w == 0)
    {
        outputColor = vec4(1, 0, 0, 1);
    }
}