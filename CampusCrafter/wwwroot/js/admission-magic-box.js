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
    var post_btn = document.getElementById("fill-btn");
    post_btn.style.display = "";
    inputs = post_btn.getElementsByTagName("input")
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].disabled = false;
    }
    
    var cont_btn = document.getElementById("cont-btn");
    cont_btn.style.display = "none";
    
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
    var post_btn = document.getElementById("fill-btn");
    post_btn.style.display = "none";

    var cont_btn = document.getElementById("cont-btn");
    cont_btn.style.display = "";
    inputs = cont_btn.getElementsByTagName("input")
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].disabled = false;
    }
    
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

    // Serialize form data
    const formData = new FormData(admission_form);

    // Make a POST request using Fetch API
    fetch('http://localhost:5151/Admission/FillAdmission', {
        method: 'POST',
        body: formData,
    })
        .then(response => {
            if (response.ok) {
                // If the response is OK (2xx), parse the response or perform further actions
                const contentType = response.headers.get('Content-Type');

                if (contentType && contentType.includes('application/json')) {
                    return response.json();
                } else {
                    // Handle non-JSON response (e.g., text/plain)
                    return response.text();
                }
            } else if (response.status === 400) {
                // If the response status is BadRequest (400), handle the error
                return response.text().then(error => {
                    console.error('Error:', error);
                    alert('Error: ' + error);
                });
            } else {
                // Handle other non-OK responses
                console.error('Unexpected response:', response);
                alert('Error:'+ response)
            }
        })
        .then(data => {
            // Handle the successful response data
            console.log('Success:', data);
        })
        .catch(error => {
            // Handle any other errors that may occur during the fetch
            console.error('Fetch error:', error);
            alert('Fetch error: ' + error.message);
        });
}

function addRowAchievement() {
    var table = document.getElementById("achievementTable");
    var row = table.insertRow(-1);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var row0 = table.rows[1];
    var cells = row0.cells;
    row.hidden = false;
    row.disabled = false;
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
    row.setAttribute("name", row0.getAttribute("name"));
    cell1.innerHTML += cells[0].innerHTML;
    cell2.innerHTML += cells[1].innerHTML;
}

function deleteRowProgress() {
    var table = document.getElementById("progressTable");
    if (table.rows.length > 2) {
    table.deleteRow(table.rows.length-1);
}}

function deleteRowAchievement() {
    var table = document.getElementById("achievementTable");
    if (table.rows.length > 2) {
        table.deleteRow(table.rows.length - 1);
    }}

function countScore() {
        
    var inputtedRows = document.getElementsByName("progressRow");
    var inputs = [];
    for (let i = 0; i < inputtedRows.length; i++) {
        var type = inputtedRows[i].getElementsByTagName("select")[0].value;
        var input = inputtedRows[i].getElementsByTagName("input")[0].value;
        inputs.push([type,input]);
    }
    
    var scoreSlots = document.getElementsByName("scoreSlot");
    for (let i = 0; i < scoreSlots.length; i++) {
        var score = 0;
        var scoreSlot = scoreSlots[i];
        var divs = scoreSlot.getElementsByTagName("div");
        for (let j = 1; j < divs.length; j++) {
            var scoreWeights = divs[j].innerText.split(":");
            for (let k = 0; k < inputs.length; k++) {
                if (parseInt(inputs[k][0]) === parseInt(scoreWeights[1])) {
                    score += (inputs[k][1]*parseFloat(scoreWeights[0]));
                }
            }
        }
        divs[0].innerText = score;
    }
}

setInterval(countScore, 100);