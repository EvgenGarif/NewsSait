


document.querySelector('a#qw').onclick = function () {
    sortList('data-searsh');
}

document.querySelector('a#qe').onclick = function () {
    sortListDesc('data-searsh');
}

function sortList(sortType) {
   
    let items = document.querySelector('.date-wrap');   
    for (let i = 0; i < items.children.length - 1; i++) {
            for (let j = i; j < items.children.length; j++) {
                if (Date.parse(items.children[i].getAttribute(sortType)) > Date.parse(items.children[j].getAttribute(sortType))) {
                   
                    let replacedNode = items.replaceChild(items.children[j], items.children[i]);
                    insertAfter(replacedNode, items.children[i]);
                }
            }
        }
    }

function sortListDesc(sortType) {
    let items = document.querySelector('.date-wrap');
    for (let i = 0; i < items.children.length - 1; i++) {
        for (let j = i; j < items.children.length; j++) {
            if (Date.parse(items.children[i].getAttribute(sortType)) < Date.parse(items.children[j].getAttribute(sortType))) {
                
                let replacedNode = items.replaceChild(items.children[j], items.children[i]);
                insertAfter(replacedNode, items.children[i]);
            }
        }
    }
}

function insertAfter(elem, refElem) {
    return refElem.parentNode.insertBefore(elem, refElem.nextSibling);
}
  
