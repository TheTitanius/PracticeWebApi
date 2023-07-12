﻿const apiName = "manufacturers";

document.getElementById("saveBtn").addEventListener("click", async () => {
    let id = "";
    var tr = document.getElementsByClassName('active')[0];
    if (tr) {
        id = tr.childNodes[0].innerHTML;
    }

    const name = document.getElementById("name").value;
    const director = document.getElementById("director").value;
    const chiefAccountant = document.getElementById("chiefAccountant").value;
    const bankDetails = document.getElementById("bankDetails").value;

    if (id === "")
        await createModel(apiName, JSON.stringify({
            name: name,
            director: director,
            chiefAccountant: chiefAccountant,
            bankDetails: bankDetails
        }));
    else
        await editModel(apiName, JSON.stringify({
            id: id,
            name: name,
            director: director,
            chiefAccountant: chiefAccountant,
            bankDetails: bankDetails
        }));
    reset();
});

document.getElementById("edit").addEventListener("click", async () => {
    let manufacturerId = "";
    var tr = document.getElementsByClassName('active')[0];
    if (tr) {
        manufacturerId = tr.childNodes[0].innerHTML;
    }

    const response = await fetch(`/api/${apiName}/${manufacturerId}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const manufacturer = await response.json();
        document.getElementById("name").value = manufacturer.name;
        document.getElementById("director").value = manufacturer.director;
        document.getElementById("chiefAccountant").value = manufacturer.chiefAccountant;
        document.getElementById("bankDetails").value = manufacturer.bankDetails;
    }
});

async function getManufacturer() {
    const response = await fetch(`/api/${apiName}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const manufacturers = await response.json();
        const rows = document.querySelector("tbody");
        manufacturers.forEach(manufacturer => rows.append(row(Object.values(manufacturer))));
    }
}

document.getElementById("deleteBtn").addEventListener("click", async () => {
    let clientId = "";
    var tr = document.getElementsByClassName('active')[0];
    if (tr) {
        clientId = tr.childNodes[0].innerHTML;
    }

    const response = await fetch(`/api/${apiName}/${clientId}`, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const manufacturer = await response.json();
        document.querySelector(`tr[data-rowid='${manufacturer.id}']`).remove();
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
})

getManufacturer();