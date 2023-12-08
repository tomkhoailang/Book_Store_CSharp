const updateFavoriteBookAmount = async () => {
	const respone = await fetch("/FavoriteBooks/getFavoriteBookAmount");
	const data = await respone.json();
	$("#user-favorite-books-amount").textContent = data.amount;
}

function nodeScriptReplace(node) {
    if (nodeScriptIs(node) === true) {
        node.parentNode.replaceChild(nodeScriptClone(node), node);
    }
    else {
        var i = -1, children = node.childNodes;
        while (++i < children.length) {
            nodeScriptReplace(children[i]);
        }
    }

    return node;
}
function nodeScriptClone(node) {
    var script = document.createElement("script");
    script.text = node.innerHTML;

    var i = -1, attrs = node.attributes, attr;
    while (++i < attrs.length) {
        script.setAttribute((attr = attrs[i]).name, attr.value);
    }
    return script;
}

function nodeScriptIs(node) {
    return node.tagName === 'SCRIPT';
}

nodeScriptIs(document.getElementsByTagName("body")[0])
console.log(document.getElementsByTagName("body")[0])