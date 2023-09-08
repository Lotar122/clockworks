#version 330

out vec4 outputColor;

in vec2 texCoord;
uniform vec4 lightColor;

uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord);
    if(lightColor.w == 0)
    {
        outputColor = vec4(1, 0, 0, 1);
    }
}