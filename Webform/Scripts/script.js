



// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict'
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()


var fileName = "";
var fileGUID = "";
var hostUrl = "http://testsrv1601.enate.local/webform";

document.getElementById('submitForm').addEventListener('submit', function (e) {
    e.preventDefault();

    if (validation() != 0){
         createTicket();
    }
})


function validation() {
    if (document.getElementById('userName').value == "" ||
        /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(document.getElementById('email').value) == false ||
        document.getElementById('Subject').value == "" ||
        document.getElementById('description').value == "") {
        return 0;
    }
    else {
        return 1;
    }
}


function createTicket() {
    var xmlhttp = new XMLHttpRequest()
    theUrl = `${hostUrl}/api/Process`
    xmlhttp.open("POST", theUrl)
    xmlhttp.setRequestHeader("Content-Type", "application/json")
    let x, y


    if (fileGUID == "" || fileName == "") {
        x = ""
        y = ""
    } else {
        x = fileName
        y = fileGUID
    }
    xmlhttp.send(
        JSON.stringify({
            TicketAttributeVersionGUID: "d14d0456-19f2-477f-96f5-12a5aee7ca5f",
            RequesterUserGUID: "",
            Title: document.getElementById('Subject').value,
            Description: document.getElementById('description').value,
            emailAddress: document.getElementById('email').value,
            userName: document.getElementById('userName').value,
            fileName: x,
            fileGUID: y 
        })
    )

    document.getElementById('spinner').style.display = 'block'
    document.getElementById('formbody').style.display = 'none'
    xmlhttp.onload = function () {
        if (xmlhttp.status === 200 || xmlhttp.status === 204) {
            document.getElementById('refId').innerHTML = xmlhttp.response;
            document.getElementById('spinner').style.display = 'none'
            document.getElementById('formbody').style.display = 'block'
            document.getElementById("myModal").style.display = "block"
            document.getElementById('submitForm').reset();
        }
    }
}

// Get the modal
var modal = document.getElementById("myModal");

// Get the button that opens the modal
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];




// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
    document.querySelector(".drop-zone__prompt").style.display = 'block';
    document.querySelector(".drop-zone__thumb").style.display = 'none';
    document.getElementById('submitForm').classList.remove('was-validated');
    fileGUID = "";
    fileName = "";
}



// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}




// upload file
function myFunction(dropZoneElement, file) { 
    console.log("svdsd:");
    document.querySelector('.lds-ellipsis').style.display = "block"
    document.querySelector('.drop-zone__prompt').style.display = "none"
    let h = new Headers()
    h.append("Accept", "application/json")
    let fd = new FormData()

    myFile = document.getElementById("myFile").files[0]
    myFileName = document.getElementById("myFile").files[0].name
    myFileSize = document.getElementById("myFile").files[0].size
    fd.append("pdf-file", myFile)
    
    fileName = myFileName.name;
    let req = new Request(`${hostUrl}/api/PreapareNewFile`, {
        method: "POST",
        headers: h,
        body: fd
    })

    fetch(req)
        .then(response => response.text())
        .then(data => {
            fileGUID = data;
            fileName = myFile.name;
            updateThumbnail(dropZoneElement, file);
            function updateThumbnail(dropZoneElement) {
                let thumbnailElement = dropZoneElement.querySelector(".drop-zone__thumb");
               
                // First time - remove the prompt
                if (dropZoneElement.querySelector(".drop-zone__prompt")) {
                    dropZoneElement.querySelector(".drop-zone__prompt").style.display = 'none';
                }

                // First time - there is no thumbnail element, so lets create it
                if (!thumbnailElement) {
                    thumbnailElement = document.createElement("div");
                    thumbnailElement.classList.add("drop-zone__thumb");
                    dropZoneElement.appendChild(thumbnailElement);
                }

                thumbnailElement.dataset.label = fileName;
                document.querySelector('.lds-ellipsis').style.display = "none"
                document.querySelector(".drop-zone__thumb").style.display = "block"
                
              

            }
        
        })
}



document.querySelectorAll(".drop-zone__input").forEach((inputElement) => {
    const dropZoneElement = inputElement.closest(".drop-zone");

    dropZoneElement.addEventListener("click", (e) => {
        inputElement.click();
        
    });

    inputElement.addEventListener("change", (e) => {
        if (inputElement.files.length) {     
            myFunction(dropZoneElement);
        }
    });

    dropZoneElement.addEventListener("dragover", (e) => {
        e.preventDefault();
        dropZoneElement.classList.add("drop-zone--over");
    });

    ["dragleave", "dragend"].forEach((type) => {
        dropZoneElement.addEventListener(type, (e) => {
            dropZoneElement.classList.remove("drop-zone--over");
        });
    });



    dropZoneElement.addEventListener("drop", (e) => {
        e.preventDefault();
        if (e.dataTransfer.files.length) {
            inputElement.files = e.dataTransfer.files;
            myFunction(dropZoneElement)
        }
        dropZoneElement.classList.remove("drop-zone--over");
    });
});

/**
 * Updates the thumbnail on a drop zone element.
 *
 * @param {HTMLElement} dropZoneElement
 * @param {File} file
 */





