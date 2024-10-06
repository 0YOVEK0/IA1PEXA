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

### Enemigos
Se recomienda crear una clase base para los enemigos, que incluya las siguientes características comunes:
- **Vida/HP**: Cada enemigo tiene HP y muere si llega a 0 o menos.
- **Daño**: Los enemigos causan daño al jugador al colisionar con él.
- **Efectos**: (Puntos extra) Los enemigos pueden brillar de color rojo al recibir daño, y se pueden agregar animaciones de muerte.

# Mi Proyecto

## Videos de Demostración

### Video 1
[![Mira mi video](https://img.youtube.com/vi/yS6p_gJR_3c/0.jpg)](https://www.youtube.com/watch?v=yS6p_gJR_3c)

### Video 2
[![Mira mi video](https://img.youtube.com/vi/adZuMjNtcic/0.jpg)](https://www.youtube.com/watch?v=adZuMjNtcic)

### Video 3
[![Mira mi video](https://img.youtube.com/vi/41tlIUtn-5E/0.jpg)](https://www.youtube.com/watch?v=41tlIUtn-5E)

### Video 4
[![Mira mi video](https://img.youtube.com/vi/mRN4Eyt6SfU/0.jpg)](https://www.youtube.com/watch?v=mRN4Eyt6SfU)





#### Enemigo Tipo Torreta
- **Cono de Visión**: Tiene un cono de visión que rota y detecta al jugador.
- **Disparo**: Dispara al jugador al ser detectado, incluso si el jugador sale de su cono de visión.
- **Visualización**: (Puntos extra) Muestra su cono de visión en la pantalla, no solo en Gizmos.



#### Enemigo Pesado
- **Persecución**: Persigue al jugador sin parar al entrar al cuarto.
- **Movimiento Lento**: Tiene aceleración baja, pero puede alcanzar altas velocidades.
- **Interacción con Paredes**: Al chocar con una pared, su velocidad se reduce a 0.0 (opcional).
- **Daño Proporcional**: (Puntos extra) El daño causado aumenta según la velocidad del enemigo al chocar con el jugador.

#### Enemigo Escapista
- **Flee**: Huye del jugador durante un tiempo y luego se detiene.
- **Disparo Predictivo**: Dispara hacia la posición futura del jugador.
- **Movimiento Ligero**: Acelera rápidamente, pero no alcanza alta velocidad.
- **Obstacle Avoidance**: (Puntos extra) Realiza *obstacle avoidance* mientras huye.

## Requisitos Técnicos
- **Motor**: Unity
- **Lenguaje**: C#

## Instalación
1. Clona o descarga el repositorio del proyecto.
2. Abre el proyecto en Unity.
3. Ejecuta la escena principal para comenzar a jugar.

## Contribuciones
Cualquier contribución es bienvenida. Por favor, abre un *issue* o un *pull request* para sugerencias o mejoras.

## Licencia
Este proyecto está bajo la Licencia MIT. Para más detalles, consulta el archivo LICENSE.
