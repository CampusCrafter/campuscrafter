function toConfirm() {
    var admission_form = document.getElementById("admission-form");
    var selects = admission_form.getElementsByTagName("select");
    for (let i = 0; i < selects.length; i++) {
        selects[i].disabled = true;
    }

    var btns = document.getElementsByName("btn-add");
    for (let i = 0; i < btns.length; i++) {
        btns[i].style.display = "none";
    }
    
    var inputs = admission_form.getElementsByTagName("input");
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].disabled = true;
    }
    
    var post_btn = document.getElementById("post-btn");
    post_btn.style.display = "";
    inputs = post_btn.getElementsByTagName("input")
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].disabled = false;
    }
    
    var cont_btn = document.getElementById("cont-btn");
    cont_btn.style.display = "none";

    document.getElementById("majors_table").style.display = "table";
}

function toFill() {
    var admission_form = document.getElementById("admission-form");
    var selects = admission_form.getElementsByTagName("select");
    for (let i = 0; i < selects.length; i++) {
        selects[i].disabled = false;
    }

    var inputs = admission_form.getElementsByTagName("input");
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].disabled = false;
    }

    var btns = document.getElementsByName("btn-add");
    for (let i = 0; i < btns.length; i++) {
        btns[i].style.display = "";
    }

    var post_btn = document.getElementById("post-btn");
    post_btn.style.display = "none";

    var cont_btn = document.getElementById("cont-btn");
    cont_btn.style.display = "";
    inputs = cont_btn.getElementsByTagName("input")
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].disabled = false;
    }
    
    document.getElementById("majors_table").style.display = "none";
    
}

function submission() {
    
    var admission_form = document.getElementById("admission-form");
    var selects = admission_form.getElementsByTagName("select");
    for (let i = 0; i < selects.length; i++) {
        selects[i].disabled = false;
    }

    var inputs = admission_form.getElementsByTagName("input");
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].disabled = false;
    }

    admission_form.submit();
}

function addRowAchievement() {
    var table = document.getElementById("achievementTable");
    var row = table.insertRow(-1);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var row0 = table.rows[1];
    var cells = row0.cells;

    cell1.innerHTML += cells[0].innerHTML;
    cell2.innerHTML += cells[1].innerHTML;
}

function addRowProgress() {
    var table = document.getElementById("progressTable");
    var row = table.insertRow(-1);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var row0 = table.rows[1];
    var cells = row0.cells;

    cell1.innerHTML += cells[0].innerHTML;
    cell2.innerHTML += cells[1].innerHTML;
}

function countScore(i) {
    
}