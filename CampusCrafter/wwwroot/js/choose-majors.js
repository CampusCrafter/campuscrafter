function check() {
    var form = document.getElementById("myForm");
    var selects = form.getElementsByTagName("select");
    var i = 0;
    for (let j = 0; j < selects.length; j++) {
        if (selects[j].value !== "") {
            i++;
        }
    }
    if (i < 5) {
        form.submit();
        return;
    }
    alert("to many majors chosen \n maximum of 5 can be chosen");
}

function toggleTable(tableName) {
    const table = document.getElementById(tableName);
    if (table.style.display === "none") {
        table.style.display = "table"; // Show the table
    } else {
        table.style.display = "none"; // Hide the table
    }
}