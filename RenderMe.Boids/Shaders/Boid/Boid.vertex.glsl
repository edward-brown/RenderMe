#version 330
layout(location = 0) in vec2 aPos;

uniform vec2 positions[100];
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    vec2 Pos = positions[gl_InstanceID];
    gl_Position = vec4(aPos.x + Pos.x, aPos.y + Pos.y, 0.0, 1.0) * model * view * projection;
}