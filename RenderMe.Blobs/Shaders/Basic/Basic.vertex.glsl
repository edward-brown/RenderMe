#version 330
layout(location = 0) in vec4 position;

uniform vec2 offsets[100];

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    vec2 offset = offsets[gl_InstanceID];
    gl_Position = vec4(position.x + offset.x, position.y + offset.y, offset.x, 1.0) * model * view * projection;
}