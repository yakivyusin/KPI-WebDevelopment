'use strict';

var stdin = process.stdin;
stdin.setRawMode(true);

function fetchUrl(url) {
    return fetch(url);
}

async function fetchUrlStatus(url) {
    let response = await fetch(url);

    return response.status;
}

function custom() {
    return {
        then: (onfulfilled, onrejected) => onfulfilled(1000)
    };
}

(async () => {
    let response = await fetchUrl('https://learn.microsoft.com');
    console.log(response);

    let status = await fetchUrlStatus('https://learn.microsoft.com/maui');
    console.log(status);

    let number = await custom();
    console.log(number);
})();

stdin.resume();
stdin.on('data', () => process.exit());
