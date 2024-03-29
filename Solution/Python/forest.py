import pygame
import sys
import math

# Initialize Pygame and create the screen
pygame.init()
screen_width, screen_height = 800, 600
screen = pygame.display.set_mode((screen_width, screen_height))
pygame.display.set_caption("The Magical Forest of Algora")

# Load images
background_img = pygame.image.load(
    "Solution/Python/assets/forest_background.png"
).convert_alpha()
harmony_end_img = pygame.image.load(
    "Solution/Python/assets/harmony_end.png"
).convert_alpha()
ascent_end_img = pygame.image.load(
    "Solution/Python/assets/ascent_end.png"
).convert_alpha()
resilience_end_img = pygame.image.load(
    "Solution/Python/assets/resilience_end.png"
).convert_alpha()

lox_img = pygame.image.load("Solution/Python/assets/lox.png").convert_alpha()
drako_img = pygame.image.load("Solution/Python/assets/drako.png").convert_alpha()
sparkle_trail_img = pygame.image.load(
    "Solution/Python/assets/magical_sparkle_trail.png"
).convert_alpha()

# Scale images
background_img = pygame.transform.scale(background_img, (screen_width, screen_height))
lox_img = pygame.transform.scale(lox_img, (100, 100))
drako_img = pygame.transform.scale(drako_img, (100, 100))
sparkle_trail_img = pygame.transform.scale(sparkle_trail_img, (20, 20))

# Initial positions and states
lox_pos = [screen_width * 0.2, screen_height - 150]
drako_pos = [screen_width * 0.7, screen_height - 150]
angle = 0  # For rotating during twirl

# Movement and animation parameters
jump_height = 50
animation_speed = 5


# Animation functions
def twirl(image, angle):
    """Rotate the image while maintaining its center."""
    rotated_image = pygame.transform.rotate(image, angle)
    new_rect = rotated_image.get_rect(center=image.get_rect(topleft=lox_pos).center)
    return rotated_image, new_rect.topleft


def leap(position, frame, total_frames):
    """Animate a leap, moving the character up then down."""
    # Calculate progress as a percentage
    progress = frame / total_frames
    # Adjust the height by a sine wave pattern
    height = jump_height * abs(math.sin(progress * math.pi))
    return [position[0], position[1] - height]


# Main loop
def main():
    running = True
    clock = pygame.time.Clock()
    frame = 0

    while running:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running = False

        screen.blit(background_img, (0, 0))

        # Animate Lox with a "Twirl"
        global angle
        angle += animation_speed
        rotated_lox, new_lox_pos = twirl(lox_img, angle)
        screen.blit(rotated_lox, new_lox_pos)

        # Animate Drako with a "Leap"
        if frame < 60:  # Simple condition to repeat the leap
            new_drako_pos = leap(drako_pos, frame, 60)
            screen.blit(drako_img, new_drako_pos)
        else:
            screen.blit(drako_img, drako_pos)

        frame = (frame + 1) % 61  # Reset the frame count for looping the animation

        pygame.display.flip()
        clock.tick(60)

    pygame.quit()
    sys.exit()


if __name__ == "__main__":
    main()
