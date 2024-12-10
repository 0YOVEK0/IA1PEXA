# Proyecto Primer Parcial

## Descripción del Proyecto
Es un mini juego inspirado en *Binding of Isaac*, desarrollado en Unity con vista 2D desde arriba. El objetivo es crear un entorno de combate dinámico donde un personaje jugable se enfrenta a diferentes enemigos, cada uno con comportamientos únicos, en un escenario lleno de obstáculos.

## Características del Juego

### Personaje Jugable
- **Movimiento**: El jugador puede moverse en todas las direcciones (arriba, abajo, izquierda, derecha y diagonales).
- **Rotación**: El personaje gira hacia la dirección en la que se mueve.
- **Disparo**: Puede disparar en la dirección de su rotación al presionar un botón.
- **Colisión**: Las balas colisionan adecuadamente con enemigos y obstáculos, destruyéndose al impactar (o desactivándose para reutilizar).
- **Vida**: El personaje tiene HP y muere si su HP llega a 0 o menos.

### Escenario
- **Muros**: El área de combate contiene muros que impiden el paso del jugador y los enemigos.
- **Obstáculos**: Se incluyen obstáculos que permiten que los enemigos como el escapista realicen *obstacle avoidance*.
- **Obstáculos Destructibles**: (Puntos extra) Algunos obstáculos pueden ser destruidos con balas tras recibir daño.
- **Estilo Visual**: Puede ser en 2D, 3D, o una mezcla de ambos, siempre manteniendo la estructura similar a *Binding of Isaac*.
- **Restricciones**: No se permite el uso de NavigationMesh en este parcial.
  
[![Obstáculos Destructibles](https://img.youtube.com/vi/mRN4Eyt6SfU/0.jpg)](https://www.youtube.com/watch?v=mRN4Eyt6SfU)

### Enemigos
Se recomienda crear una clase base para los enemigos, que incluya las siguientes características comunes:
- **Vida/HP**: Cada enemigo tiene HP y muere si llega a 0 o menos.
- **Daño**: Los enemigos causan daño al jugador al colisionar con él.
- **Efectos**: (Puntos extra) Los enemigos pueden brillar de color rojo al recibir daño, y se pueden agregar animaciones de muerte.


#### Enemigo Tipo Torreta
- **Cono de Visión**: Tiene un cono de visión que rota y detecta al jugador.
- **Disparo**: Dispara al jugador al ser detectado, incluso si el jugador sale de su cono de visión.
- **Visualización**: (Puntos extra) Muestra su cono de visión en la pantalla, no solo en Gizmos.
  
[![Enemigo Tipo Torreta](https://img.youtube.com/vi/yS6p_gJR_3c/0.jpg)](https://www.youtube.com/watch?v=yS6p_gJR_3c)


#### Enemigo Pesado
- **Persecución**: Persigue al jugador sin parar al entrar al cuarto.
- **Movimiento Lento**: Tiene aceleración baja, pero puede alcanzar altas velocidades.
- **Interacción con Paredes**: Al chocar con una pared, su velocidad se reduce a 0.0 (opcional).
- **Daño Proporcional**: (Puntos extra) El daño causado aumenta según la velocidad del enemigo al chocar con el jugador.
  
[![Enemigo Escapista](https://img.youtube.com/vi/41tlIUtn-5E/0.jpg)](https://www.youtube.com/watch?v=41tlIUtn-5E)

#### Enemigo Escapista
- **Flee**: Huye del jugador durante un tiempo y luego se detiene.
- **Disparo Predictivo**: Dispara hacia la posición futura del jugador.
- **Movimiento Ligero**: Acelera rápidamente, pero no alcanza alta velocidad.
- **Obstacle Avoidance**: (Puntos extra) Realiza *obstacle avoidance* mientras huye.
  
[![Enemigo Pesado](https://img.youtube.com/vi/adZuMjNtcic/0.jpg)](https://www.youtube.com/watch?v=adZuMjNtcic)

# 🛡️ Boss Enemy AI en Unity

Este script implementa un sistema de inteligencia artificial para un Boss en Unity que combina comportamientos de patrullaje, persecución, ataque y descanso. El enemigo utiliza **NavMeshAgent** para su navegación y cuenta con un sistema de estados (FSM) para gestionar su lógica de comportamiento.

## 🎮 Características principales:
- **Estados del Boss**:
  - **Patrullaje**: El Boss se mueve entre puntos predefinidos hasta detectar al jugador.
  - **Persecución**: Cuando el jugador está en el rango de detección, el Boss lo persigue.
  - **Ataque a distancia**: Dispara proyectiles hacia el jugador al entrar en rango.
  - **Descanso**: Toma un breve descanso después de atacar antes de reanudar la persecución.
- **Animaciones integradas**: Cambia entre animaciones de caminar, reposo y ataque según el estado.
- **Debug visual**: Muestra el rango de detección y los puntos de patrullaje en la escena.


## 🎥 Video demostrativo:
[![Ver en YouTube](https://www.youtube.com/watch?v=wx2xodpRv68.jpg)](https://www.youtube.com/watch?v=wx2xodpRv68)


## Tecnologías Utilizadas

- **Lenguajes de programación:** C#
- **Framework:** Unity 2023.2.18f1
- **Documetacion Auxiliar:** Chatgpt-4o

## Instalación
1. Clona o descarga el repositorio del proyecto.
2. Abre el proyecto en Unity.
3. Ejecuta la escena principal para comenzar a jugar.

## Contribuciones
Cualquier contribución es bienvenida. Por favor, abre un *issue* o un *pull request* para sugerencias o mejoras.

## Licencia
Este proyecto está bajo la Licencia MIT. Para más detalles, consulta el archivo LICENSE.
