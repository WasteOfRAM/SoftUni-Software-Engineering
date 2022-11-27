export function errorNotifications(errorMsg) {
    const errorElement = document.querySelector(".notification");
    document.querySelector(".notification span").textContent = errorMsg;
    errorElement.style.display = "block";
    setTimeout(() => {
        errorElement.style.display = "none";
    }, 3000);
}