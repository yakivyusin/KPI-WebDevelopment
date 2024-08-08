'use strict';

let stdin = process.stdin;
stdin.setRawMode(true);

let fetchPromise = fetch('https://learn.microsoft.com').then(response => console.log(response));

let customPromise = new Promise((resolve, reject) => {
    setTimeout(() => resolve('Hello World'), 2000);
});

customPromise.then((v) => console.log(`value: ${v}`), (v) => console.log(`error: ${v}`));

Promise.resolve('Hello World 2').then((v) => console.log(`value: ${v}`), (v) => console.log(`error: ${v}`));

stdin.resume();
stdin.on('data', () => process.exit());
