function deleteCheckedContacts() {

    var objectsToDelete = document.getElementsByClassName("user-checkbox")

    var contactIds = [];

    for (var i = 0; i < objectsToDelete.length; i++) {
        var objectToDelete = objectsToDelete[i];
        if (objectToDelete.checked)
            contactIds.push(objectsToDelete[i].getAttribute("name"))
    }

    if (contactIds.length > 0) {

        var RequestString = "";

        for (var i = 0; i < contactIds.length - 1; i++) {
            RequestString += "Ids[" + i + "]=" + contactIds[i] + "&"
        }

        RequestString += "Ids[" + (contactIds.length - 1) + "]=" + contactIds[contactIds.length - 1] + ""
        console.log(RequestString)

        window.location.replace("/home/delete?" + RequestString);

    }

}

function blockCheckedContacts() {

    var objectsToDelete = document.getElementsByClassName("user-checkbox")

    var contactIds = [];

    for (var i = 0; i < objectsToDelete.length; i++) {
        var objectToDelete = objectsToDelete[i];
        if (objectToDelete.checked)
            contactIds.push(objectsToDelete[i].getAttribute("name"))
    }

    if (contactIds.length > 0) {

        var RequestString = "";

        for (var i = 0; i < contactIds.length - 1; i++) {
            RequestString += "Ids[" + i + "]=" + contactIds[i] + "&"
        }

        RequestString += "Ids[" + (contactIds.length - 1) + "]=" + contactIds[contactIds.length - 1] + ""
        console.log(RequestString)

        window.location.replace("/home/block?" + RequestString);

    }

}

function unblockCheckedContacts() {

    var objectsToDelete = document.getElementsByClassName("user-checkbox")

    var contactIds = [];

    for (var i = 0; i < objectsToDelete.length; i++) {
        var objectToDelete = objectsToDelete[i];
        if (objectToDelete.checked)
            contactIds.push(objectsToDelete[i].getAttribute("name"))
    }

    if (contactIds.length > 0) {

        var RequestString = "";

        for (var i = 0; i < contactIds.length - 1; i++) {
            RequestString += "Ids[" + i + "]=" + contactIds[i] + "&"
        }

        RequestString += "Ids[" + (contactIds.length - 1) + "]=" + contactIds[contactIds.length - 1] + ""
        console.log(RequestString)

        window.location.replace("/home/unblock?" + RequestString);

    }

}