#!/bin/bash

# Function to display browser notification
function show_notification() {
  if [ "$(uname)" == "Darwin" ]; then
    osascript -e 'display notification "Workshop Reminder: 30 minutes have passed." with title "Workshop"'
  elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    if command -v notify-send > /dev/null; then
      notify-send "Workshop Reminder" "30 minutes have passed."
    else
      echo "Desktop notification not supported on this Linux system."
    fi
  else
    echo "Browser notification not supported on this operating system."
  fi
}

show_notification
