// notification.js
const vscode = require('vscode');

async function showNotification() {
  const response = await vscode.window.showInformationMessage(
    "Workshop Reminder: 30 minutes have passed.",
    "Dismiss"
  );

  if (response === "Dismiss") {
    console.log("Notification dismissed.");
  }
}

showNotification();
