# Pong Remake

First Attempt at recreating the popular 1972 classic Pong, built from scratch without tutorials. Purpose was to educate myself on Unity Game Development while also exploring new features
such as AI implementation.

## Description

The Project consists of a Start Screen, Difficulty selection screen, gameplay screen and end game screen. Entire game was created using one scene. Difficulty is determined by adjusting parameters of the AI,
for example, easy -> slows down AI movement speed below player reference speed, Medium -> increases AI movement speed above player reference speed and increases paddle height,
and Hard -> further increases movement speed, increases paddle height and also minimizes "deadzone" between AI tracking the ball's y-position and the paddles y-position.
At the start of each point, ball is launched in random direction with same speed, the direction is determined using a unit circle and constrainted from shooting in upper quadrants 
of the screen by recursively calling method to randomize ball's initial direction until it meets the constraints.
Game is currently played until either player or opponent reach 5 points.
Overall the project was a great chance for me to explore implementing a simple AI aswell as learning how to change game states, pausing the game, reseting the entire scene and updating UI.


## Photos

![image](https://github.com/PaulKokhanov1/Pong-Remake/assets/69466838/6c27bee8-f921-45e6-97f8-a316e9b417b6)
![image](https://github.com/PaulKokhanov1/Pong-Remake/assets/69466838/8899b0d3-f193-471c-a2d7-45528bbf1750)
![image](https://github.com/PaulKokhanov1/Pong-Remake/assets/69466838/9d2203de-a646-4859-9b16-24deeeec6f60)


## Challenges

### Ball glitching through walls
I noticed at very high speeds the ball would often glitch through the box collider's, I found that I was using a discrete collision detection on my box collider which would mean
that if the object is moving fast enough, the ball would travel a certain distance between two frames, meaning it could "phase" through the wall.
A solution could have been to decrease the fixed time step, however this is not optimal because it affects preformance. Another solution was to use a continous collision detection, which is more computationally heavy and only works with 
static objects with no rigidbody, which in my case was all I needed.

### AI Implementation tracking ball too closely
When first implementing the AI, I attempted a very simple solution to track the ball position, however, as expected the AI was unbeatable. My next solution was to decrease the AI's speed
which was a possible solution, however I wanted to explore further. I ended up adding a deadzone between the ball's y-position and the AI's y-position to allow a type of "offset"
between the two objects. With this implementation I noticed that the AI would often continously move up and down as it was continously adjusting its velocity in the y-direction to
"catch" up to the ball. I did think the deadzone was still a good idea so to minimize the amount of AI movement, I would only allow it to move once the ball appeared in its half of the playing field.
