console.log("Welcome to the Magical Forest of Algora!");
// Define the creatures and their dance moves
const creatures = ['Lox', 'Drako'];
const danceMoves = ['Twirl', 'Leap', 'Spin'];

// Function to get a random dance move
function getRandomDanceMove() {
    return danceMoves[Math.floor(Math.random() * danceMoves.length)];
}

// Function to perform the sacred dance
function performSacredDance(creature1, danceMove1, creature2, danceMove2) {
    console.log(`${creature1} starts to dance.`);
    console.log(`${creature1} performs a ${danceMove1}.`);
    console.log(`${creature2} starts to dance.`);
    console.log(`${creature2} performs a ${danceMove2}.`);

    if (danceMove1 === 'Twirl' && danceMove2 === 'Twirl') {
        console.log('Fireflies light up the forest.');
    } else if ((danceMove1 === 'Leap' && danceMove2 === 'Spin') || (danceMove1 === 'Spin' && danceMove2 === 'Leap')) {
        console.log('A rainbow appears in the sky.');
    } else if (danceMove1 === 'Leap' && danceMove2 === 'Leap') {
        console.log('Gentle rain starts falling.');
    } else {
        console.log('The forest is filled with a magical aura.');
    }
}

// Start the sacred dance
performSacredDance();