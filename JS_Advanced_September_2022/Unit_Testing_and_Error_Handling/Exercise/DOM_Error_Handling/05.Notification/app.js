function notify(message) {
  let notification = document.getElementById("notification");
  notification.textContent = message;
  notification.style.display = "block";

  notification.addEventListener('click', hideNotification);

  function hideNotification() {
    notification.style.display = "none";
  }
}