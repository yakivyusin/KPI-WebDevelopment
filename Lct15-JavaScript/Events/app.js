function attributeDoubleClick() {
    confirm('Чи є життя на Марсі?');
}

let dragEventButton = document.getElementById('drag-event-button');
dragEventButton.ondragend = () => {
    dragEventButton.textContent = ':-(';
    setTimeout(() => dragEventButton.textContent = 'Драг-с', 2000);
};

window.onresize = () => {
    document.body.style = `background-color: rgb(${Math.random() * 255}, ${Math.random() * 255}, ${Math.random() * 255});`;
};

function mouseOverHandler() {
    mouseOverEventButton.textContent = ';-(';
    mouseOverEventButton.removeEventListener('mouseover', mouseOverHandler);
    setTimeout(() => mouseOverEventButton.textContent = 'Овер-с', 2000);
}

let mouseOverEventButton = document.getElementById('mouseover-event-button');
mouseOverEventButton.addEventListener('mouseover', mouseOverHandler);

document.getElementById('google-link').addEventListener('click', (e) => {
    console.dir(e);
    console.log(e.defaultPrevented);

    e.preventDefault();

    console.log(e.defaultPrevented);
});

function bubblingHandler(e) {
    let log = document.getElementById('bubble-output');
    log.textContent += `Click event | target: ${e.target.tagName}, currentTarget: ${e.currentTarget.tagName}\n`;
}

document.getElementById('bubble-row').addEventListener('click', bubblingHandler);
document.getElementById('bubble-cell').addEventListener('click', bubblingHandler);
document.getElementById('bubble-button').addEventListener('click', bubblingHandler);
