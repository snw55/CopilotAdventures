print('Welcome to the Magical Forest of Algora!')

import matplotlib.pyplot as plt

dance_moves_effects = {
    ('Twirl', 'Twirl'): 'Fireflies light up the forest.',
    ('Leap', 'Spin'): 'Gentle rain starts falling.',
    ('Spin', 'Leap'): 'A rainbow appears in the sky.',
}


def dance(lox_moves, drako_moves):
    # Initialize the state of the forest
    forest_state = []

    # Simulate the dance
    for lox_move, drako_move in zip(lox_moves, drako_moves):
        # Determine the combined dance move
        combined_move = (lox_move, drako_move)

        # Determine the effect of the combined dance move
        effect = dance_moves_effects.get(combined_move, 'No effect')
        # Update the state of the forest
        forest_state.append(effect)

    # Display the final state of the forest
    for i, state in enumerate(forest_state, 1):
        print(f'After sequence {i}, the state of the forest is: {state}')

    plt.bar(range(1, len(forest_state) + 1), [effect for effect in forest_state])
    plt.xlabel('Sequence Number')
    plt.ylabel('Forest State')
    plt.title('Visualization of the Forest State')
    plt.show()


if __name__ == '__main__':
    # Define the dance sequences for Lox and Drako
    lox_moves = ['Twirl', 'Leap', 'Spin', 'Twirl', 'Leap']
    drako_moves = ['Spin', 'Twirl', 'Leap', 'Leap', 'Spin']

    dance(lox_moves, drako_moves)
