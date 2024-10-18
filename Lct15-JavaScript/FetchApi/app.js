async function loadData() {
    let weatherResponse = await fetch('http://localhost:5066/weatherforecast', {
        method: 'get'
    });

    if (!weatherResponse.ok) {
        return;
    }

    let weatherData = await weatherResponse.json();

    for (let i = 0; i < weatherData.length; i++) {
        createElement(weatherData[i]);
    }
}

function createElement(elementData) {
    let date = document.createElement('td');
    date.textContent = elementData.date;

    let temperature = document.createElement('td');
    temperature.textContent = elementData.temperatureC;

    let summary = document.createElement('td');
    summary.textContent = elementData.summary;

    let tr = document.createElement('tr');
    tr.appendChild(date);
    tr.appendChild(temperature);
    tr.appendChild(summary);

    tableBody.appendChild(tr);
}

let tableBody = document.getElementById('weather-table').getElementsByTagName('tbody')[0];
document.getElementById('load-button').addEventListener('click', loadData);
