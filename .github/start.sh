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