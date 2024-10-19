console.log(`Title: ${document.title}, charset: ${document.characterSet}`);

console.log(document.getElementsByTagName('body')[0]);

let p = document.createElement('p');
let attr = document.createAttribute('attr1');
attr.value = 'value';
p.setAttributeNode(attr);
p.setAttribute('attr2', 'value');
p.textContent = 'Lorem ipsum...';

document.body.prepend(p);

console.log(document.body.innerHTML);

p.after(document.createComment('Oops...'));

console.log(document.body.innerHTML);
