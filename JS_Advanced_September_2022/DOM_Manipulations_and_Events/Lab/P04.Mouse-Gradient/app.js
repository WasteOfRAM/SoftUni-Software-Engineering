function attachGradientEvents() {
    let result = document.getElementById("result");
    let gradient = document.getElementById("gradient");
    gradient.addEventListener('mousemove', gradientMove);
    gradient.addEventListener('mouseout', gradientOut);

    function gradientMove(event) {
        let position = event.offsetX;
        let gradientWidth = event.target.clientWidth;
        let power = Math.trunc(position / gradientWidth * 100);
        result.textContent = power + '%';
    }

    function gradientOut() {
        result.textContent = '';
    }
}