const uri = '/api/Person';
let persons = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function getGenderIdx(gender) {
    var x = document.getElementById("genders");
    var txt = "";
    var i;
    for (i = 0; i < x.options.length; i++) {
        if (x.options[i].value == gender.trim()) {
            return i;
        }
    }
    return null;
}

function getGenderByIdx(genderIdx) {
    var x = document.getElementById("genders");
    return genderIdx == null ? '' : x.options[genderIdx].value
}

function addItem() {
    const addCitizenIdTextbox = document.getElementById('add-citizen-id');
    const addNameTextbox = document.getElementById('add-name');
    const addEmailTextbox = document.getElementById('add-email');
    const addDateOfBirthTextbox = document.getElementById('add-date-of-birth');
    const addGenderTextbox = document.getElementById('add-gender');
    const addPhoneTextbox = document.getElementById('add-phone');

    const item = {
        CitizenId: addCitizenIdTextbox.value.trim(),
        Name: addNameTextbox.value.trim(),
        Email: addEmailTextbox.value.trim(),
        DateOfBirth: addDateOfBirthTextbox.value.trim(),
        Gender: null,
        Phone: addPhoneTextbox.value.trim()
    };

    item.Gender = getGenderIdx(addGenderTextbox.value.trim());

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            addCitizenIdTextbox.value = '';
            addNameTextbox.value = '';
            addEmailTextbox.value = ''
            addDateOfBirthTextbox.value = ''
            addGenderTextbox.value = ''
            addPhoneTextbox.value = ''
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = persons.find(item => item.id === id);

    document.getElementById('edit-citizen-id').value = item.citizenId;
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-email').value = item.email;
    document.getElementById('edit-date-of-birth').value = item.dateOfBirth.split('T')[0];
    document.getElementById('edit-gender').value = getGenderByIdx(item.gender);
    document.getElementById('edit-phone').value = item.phone;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        citizenId: document.getElementById('edit-citizen-id').value.trim(),
        name: document.getElementById('edit-name').value.trim(),
        email: document.getElementById('edit-email').value.trim(),
        dateOfBirth: document.getElementById('edit-date-of-birth').value,
        gender: getGenderIdx(document.getElementById('edit-gender').value),
        phone: document.getElementById('edit-phone').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'person' : 'persons';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('persons');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode1 = document.createTextNode(item.citizenId);
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.name);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(item.email);
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        let textNode4 = document.createTextNode(item.dateOfBirth.toString().split('T')[0]);
        td4.appendChild(textNode4);

        var x = document.getElementById("genders");

        let td5 = tr.insertCell(4);
        let textNode5 = document.createTextNode(item.gender == null ? '' : x.options[item.gender].value);
        td5.appendChild(textNode5);

        let td6 = tr.insertCell(5);
        let textNode6 = document.createTextNode(item.phone);
        td6.appendChild(textNode6);

        let td7 = tr.insertCell(6);
        td7.appendChild(editButton);

        let td8 = tr.insertCell(7);
        td8.appendChild(deleteButton);
    });

    persons = data;
}