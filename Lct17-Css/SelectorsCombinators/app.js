
function getColorLuminance(r, g, b) {
    function gammaExpanding(value) {
        return value / 255 <= 0.04045 ? (value / 255) / 12.92 : Math.pow(((value / 255) + 0.055) / 1.055, 2.4);
    }

    return 0.2126 * gammaExpanding(r) + 0.7152 * gammaExpanding(g) + 0.0722 * gammaExpanding(b);
}

document.getElementById("stylesheet-enter").addEventListener("change", (ev) => {
    let style = document.getElementById("dynamic-stylesheet");
    style.textContent = ev.target.value;
});

[...document.getElementsByTagName("pre")].forEach((element) => {
    function getRandomInt(max) {
        return Math.floor(Math.random() * max);
    }

    let r = getRandomInt(255);
    let g = getRandomInt(255);
    let b = getRandomInt(255);

    element.style = `background-color: rgb(${r},${g},${b}); color: ${(getColorLuminance(r, g, b) > 0.5 ? 'black' : 'white')}`;
});
