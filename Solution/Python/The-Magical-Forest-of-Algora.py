print('Welcome to the Magical Forest of Algora!')

class Forest:
    def __init__(self):
        self.state = []
        self.allowed_moves = ['Twirl', 'Leap', 'Spin', 'Jump']  # Added 'Jump'

    def add_effect(self, effect):
        self.state.append(effect)

    def verify_move(self, move):
        return move in self.allowed_moves

from itertools import combinations
from collections import Counter

def dance(players_moves):
    MAX_PLAYERS = 5
    MAX_MOVES = 10

    if len(players_moves) > MAX_PLAYERS:
        print(f'Too many players. Maximum is {MAX_PLAYERS}.')
        return

    if any(len(moves) > MAX_MOVES for moves in players_moves.values()):
        print(f'Too many moves. Maximum is {MAX_MOVES}.')
        return

    forest = Forest()
    effects = {
        # Existing combinations
        ('Twirl', 'Twirl'): '✨',
        ('Leap', 'Spin'): '💧',
        ('Spin', 'Leap'): '🌈',
        ('Twirl', 'Leap', 'Spin'): '🌳',
        ('Spin', 'Twirl', 'Leap'): '🌊',
        # New combinations with 4 moves
        ('Twirl', 'Leap', 'Spin', 'Jump'): '💦',
        ('Spin', 'Twirl', 'Leap', 'Jump'): '⛰️',
        ('Jump', 'Twirl', 'Leap', 'Spin'): '🌼',
        # New combinations with 5 moves
        ('Twirl', 'Leap', 'Spin', 'Jump', 'Twirl'): '🏰',
        ('Spin', 'Twirl', 'Leap', 'Jump', 'Spin'): '🌉',
        ('Jump', 'Twirl', 'Leap', 'Spin', 'Jump'): '🍄',
    }

    header = " ".join(f'{name:<10}' for name in players_moves.keys())
    print(f'{"Sequence":<10} {header} {"Effect":<30}')
    print('-' * 90)

    for i, moves in enumerate(zip(*players_moves.values()), 1):
        if any(not forest.verify_move(move) for move in moves):
            print(f'{i:<10} {"Invalid move":<50}')
            continue

        matching_effects = []
        for r in range(1, len(moves) + 1):
            for sub_moves in combinations(moves, r):
                effect = effects.get(sub_moves)
                if effect:
                    forest.add_effect(effect)
                    matching_effects.append(effect)

        moves_str = " ".join(f'{move:<10}' for move in moves)
        effects_counter = Counter(matching_effects)
        effects_str = " ".join(f'{effect * count}' for effect, count in effects_counter.items()) if matching_effects else "No effect"
        print(f'{i:<10} {moves_str} {effects_str}')

# Example usage:
import random

possible_moves = ['Twirl', 'Leap', 'Spin', 'Jump']
player_names = ['Lox', 'Drako', 'Luna', 'Zara', 'Rex']
players_moves = {name: [random.choice(possible_moves) for _ in range(10)] for name in player_names}
dance(players_moves)