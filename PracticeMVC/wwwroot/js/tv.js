const apiName = "tvs";

document.getElementById("saveBtn").addEventListener("click", async () => {
    let id = "";
    var tr = document.getElementsByClassName('active')[0];
    if (tr) {
        id = tr.childNodes[0].innerHTML;
    }

    const name = document.getElementById("name").value;
    const price = document.getElementById("price").value;
    const tvType = document.getElementById("tvType").value;
    const manufacturer = document.getElementById("manufacturer").value;

    if (id === "")
        await createModel(apiName, JSON.stringify({
            name: name,
            price: price,
            tvType: tvType,
            manufacturer: manufacturer
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

async function getTv() {
    const response = await fetch(`/api/${apiName}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const tvs = await response.json();
        const rows = document.querySelector("tbody");
        tvs.forEach(tv => {
            rows.append(row([
                tv.id,
                tv.name,
                tv.price,
                tv.tvInStock,
                tv.soldNumber,
                tv.deliveredNumber,
                tv.tvType.type,
                tv.manufacturer.name
            ]));
        });
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
        document.querySelector(`tr[data-rowid='${manufacturer.manufacturerId}']`).remove();
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
})

getTv();