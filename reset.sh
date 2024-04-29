#!/bin/bash

# Commit all changes on the current branch
git add .
git commit -m "Submit new solution"

# Push changes to the current branch
git push origin HEAD

# Switch to the main branch and pull the latest changes
git checkout main
git pull origin main

# Revert all changes to match main
git reset --hard origin/main

# Generate a new branch name (e.g., based on date/time)
BRANCH_NAME="participant-$(date +"%Y%m%d%H%M%S")"

# Create a new branch from main
git checkout -b $BRANCH_NAME

# Push the new branch to the remote repository
git push --force origin $BRANCH_NAME

# Switch back to the new branch
git checkout $BRANCH_NAME

# Set execute permissions for start.sh and reset.sh
chmod +x start.sh reset.sh notification.sh

# Set the immutable attribute on start.sh and reset.sh
sudo chattr +i start.sh reset.sh notification.sh

# Set the countdown timer for 30 minutes
code --execute-command "countdown-timer.settimer 00:30:00"
