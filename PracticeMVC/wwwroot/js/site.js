let selectedTr;

var table = document.getElementsByTagName("table")[0];
table.addEventListener("click", (event) => {
    let tr = event.target.closest('tr');

    if (!tr) return;

    if (!table.contains(tr)) return;

    if (tr.classList.contains('head')) return;

    document.getElementById("edit").removeAttribute("disabled");
    document.getElementById("delete").removeAttribute("disabled");

    highlight(tr);
})

function highlight(tr) {
    if (selectedTr) {
        selectedTr.classList.remove('active');
    }
    selectedTr = tr;
    selectedTr.classList.add('active');
}

function reset() {
    var elements = document.getElementsByTagName("input");
    for (let i = 0; i < elements.length; i++) {
        elements[i].value = "";
    }

    document.getElementById("edit").setAttribute("disabled", true);
    document.getElementById("delete").setAttribute("disabled", true);
}

document.getElementById("add").addEventListener("click", () => {
    reset();
    if (document.getElementsByClassName('active')[0] != null) {
        document.getElementsByClassName('active')[0].classList.remove('active');
    }
});

function row(model) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", model[0]);

    for (let i = 0; i < model.length; i++) {
        const td = document.createElement("td");
        td.append(model[i]);
        tr.append(td);
    }

    return tr;
}

async function createModel(modelsName, newModel) {

    const response = await fetch(`/api/${modelsName}`, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: newModel
    });
    if (response.ok === true) {
        const model = await response.json();
        document.querySelector("tbody").append(row(Object.values(model)));
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}

async function editModel(modelsName, oldModel) {
    const response = await fetch(`/api/${modelsName}`, {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: oldModel
    });
    if (response.ok === true) {
        const model = await response.json();
        document.querySelector(`tr[data-rowid='${model.id}']`).replaceWith(row(Object.values(model)));
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}