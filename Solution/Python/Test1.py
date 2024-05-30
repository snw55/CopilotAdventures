# Define the dance moves and their effects
dance_moves_effects = {
    ("Twirl", "Twirl"): "Fireflies light up the forest.",
    ("Leap", "Spin"): "Gentle rain starts falling.",
    ("Spin", "Leap"): "A rainbow appears in the sky.",
}

# Define the dance moves for each creature
lox_moves = ["Twirl", "Leap", "Spin", "Twirl", "Leap"]
drako_moves = ["Spin", "Twirl", "Leap", "Leap", "Spin"]

# Loop through each sequence
for i in range(5):
    # Get the dance moves of each creature
    lox_move = lox_moves[i]
    drako_move = drako_moves[i]

    # Find the corresponding effect
    effect = dance_moves_effects.get((lox_move, drako_move), "Other effect")

    # Print the effect
    print(f"After sequence {i+1}, {effect}")