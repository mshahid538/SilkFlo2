const arr = [];

function documentUpload() {
    const inputFile = document.getElementById("tFileElem");
    var selectElement = document.getElementById("usrOptTemplateSelect");
    var selectedOption = selectElement.options[selectElement.selectedIndex];
    var currItem = document.querySelector("input[type=hidden][name=currItem]").value;
    const errorMessage = document.getElementById('usrOptTemplateSelect-error-message');

    if (selectElement.value === '') {
        errorMessage.textContent = 'Please select an option.';
        return;
    } else {
        errorMessage.textContent = '';
    }

    // Create FormData object
    var fileData = new FormData();
    var count = 0;
    // Looping over all files and add it to FormData object
    for (const file of inputFile.files) {
        var fileName = document.getElementById('tempTDocs-' + count).value;
        const newFile = new File([file], fileName +"."+ file.name.split('.').pop());
        fileData.append("files", newFile);
        count++;
    }

    // value of the selected option
    fileData.append('selectedTemplate', selectedOption.value);
    fileData.append('currItem', currItem);

    $.ajax({
        type: 'post',
        //  url: '@Url.Action("UploadFiles", "Idea")',
        url: '/api/business/idea/detail/Documentation/ideaId/UploadFiles',
        data: fileData,
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        success: function (result) {

            if (result.message) {
                var resultBox = document.getElementById("usrAlrtBlobInfo");
                resultBox.innerHTML = "<div class='alert alert-danger' role='alert'>" + result.message + "</div>";
                return;
            }

            console.log(result);

            var errorCount = 0;
            var successCount = 0;
            var resultBox = document.getElementById("usrAlrtBlobInfo");
            for (const res of result.result) {
                if (res.error) {
                    errorCount++;
                }
                else {

                    successCount++;
                }
            }

            if (errorCount > 0) {
                resultBox.innerHTML = "<div class='alert alert-info alert-dismissible fade show' role='alert'>" +
                    "<strong></strong>" +
                    "<span class='badge badge-danger'>" + errorCount + "</span> file(s) are failed during upload!" +
                    //"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    //"<span aria-hidden='true'>&times;</span>" +
                    //"</button >" +
                    "</div> ";
            }
            if (successCount > 0) {
                resultBox.innerHTML = "<div class='alert alert-info alert-dismissible fade show' role='alert'>" +
                    "<strong></strong>" +
                    "<span class='badge badge-success'>" + successCount + "</span> file(s) are successfully uploaded!" +
                    //"<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                    //"<span aria-hidden='true'>&times;</span>" +
                    //"</button >" +
                "</div> ";
            }

            setTimeout(() => {
                $('#myModalDocUpld').modal('hide');
                const element9 = document.querySelector("[name='Business.Idea.Section.Documentation.Content']");
                element9.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

                reRenderTheDetailView();
            }, 2000);

        },
        error: function (err) {
            var resultBox = document.getElementById("usrAlrtBlobInfo");
            resultBox.innerHTML = "<div class='alert alert-danger' role='alert'>File size is too big, or may some error has been occurred during processing.</div>";
        }
    });
}

function onTFileElemChange() {
    var i = 0;
    const inputFile = document.getElementById("tFileElem");
    const tmpViewer = document.getElementById("templatesTempViewer");
    const showTableViewer = document.getElementById("filesContainer");
    const hideNoTextViewer = document.getElementById("noTextTDiv");

    var viewerHtm = "";
    for (var value of inputFile.files) {
        viewerHtm += "<tr><th scope='row'>"+ (i+1) +"</th>"
            +"<td><input id='tempTDocs-" + i + "' class='form-control' placeholder='File name here...' /></td>"
            + "<td>" + value.name + "</td>"
            + "<td><button type='button' class='btn btn-link' onclick='removeTFileElem("+i+")'><span class='bi bi-trash''></span></button></td>"
            + "</tr>";
        i++;
    };
    hideNoTextViewer.style.display = 'none';
    showTableViewer.style.display = 'block';
    tmpViewer.innerHTML = viewerHtm;
}

function onFileElemChange() {
    var i = 0;
    const inputFile = document.getElementById("fileElem");
    const tmpViewer = document.getElementById("filesTempViewer");
    const showTableViewer = document.getElementById("usrFilesContainer");
    const hideNoTextViewer = document.getElementById("noFilesTextTDiv");

    var viewerHtm = "";
    for (var value of inputFile.files) {
        viewerHtm += "<tr><th scope='row'>" + (i + 1) + "</th>"
            //+ "<td><input id='tempTDocs-" + i + "' class='form-control' placeholder='File name here...' /></td>"
            + "<td>" + value.name + "</td>"
            //+ "<td><button type='button' class='btn btn-link' onclick='removeTFileElem(" + i + ")'><span class='bi bi-trash''></span></button></td>"
            + "</tr>";
        i++;
    };
    hideNoTextViewer.style.display = 'none';
    showTableViewer.style.display = 'block';
    tmpViewer.innerHTML = viewerHtm;
}


function removeTFileElem(ind) {
    const dt = new DataTransfer()
    const input = document.getElementById('tFileElem')
    const { files } = input

    for (let i = 0; i < files.length; i++) {
        const file = files[i]
        if (ind !== i)
            dt.items.add(file) // here you exclude the file. thus removing it.
    }

    input.files = dt.files // Assign the updates list

    //var ind = 0;
    //const inputFile = document.getElementById("tFileElem");
    const tmpViewer = document.getElementById("templatesTempViewer");
    var viewerHtm = "";

    var resultBox = document.getElementById("usrAlrtBlobInfo");
    resultBox.innerHTML = "";
    //if (inputFile.files && i > -1) {
    //    inputFile.files.splice(i, 1);
    //}

    for (var value of input.files) {
        viewerHtm += "<tr><th scope='row'>" + (ind + 1) + "</th>"
            + "<td><input id='' placeholder='File name here...' /></td>"
            + "<td>" + value.name + "</td>"
            + "<td><button type='button' class='btn btn-link' onclick='removeTFileElem(" + ind + ")'><span class='bi bi-trash''></span></button></td>"
            + "</tr>";
        ind++;
    };
    tmpViewer.innerHTML = viewerHtm;
}


function documentsTUpload() {
    //get the files element
    const inputFile = document.getElementById("fileElem");
    var currItem = document.querySelector("input[type=hidden][name=currItem]").value;

    // Create FormData object
    var fileData = new FormData();

    // Looping over all files and add it to FormData object
    for (const file of inputFile.files) {
        fileData.append("files", file);
    }

    // Get the <select> element
    var selectElement = document.getElementById("optTemplateSelect");

    // Get the selected option using the selectedIndex property
    var selectedOption = selectElement.options[selectElement.selectedIndex];

    const errorMessage = document.getElementById('optTemplateSelect-error-message');

    if (selectElement.value === '') {
        errorMessage.textContent = 'Please select an option.';
        return;
    } else {
        errorMessage.textContent = '';
    }
    
    // value of the selected option
    fileData.append('selectedTemplate', selectedOption.value);
    fileData.append('currItem', currItem);

    //hit the endpoint
    $.ajax({
        type: 'post',
        url: '/api/business/idea/detail/Documentation/ideaId/UploadTemplateFile',
        contentType: false,
        processData: false,
        data: fileData,
        success: function (result) {

            if (result.message) {
                var alertInfo = document.getElementById("alrtBlobInfo");
                alertInfo.innerHTML = result.message;

                alertInfo.style.padding = '20px';
                alertInfo.style.width = '75 %';
                alertInfo.style.fontSize = '20px';
                alertInfo.style.margin = 'auto';
                alertInfo.style.borderRadius = '5px';
                alertInfo.styleboxShadow = 'rgb(0 0 0 / 25 %) 0px 5px 10px 2px';
            }

            //location.reload();
            var successCount = 0;
            var errorCount = 0;
            var failedFiles = "";

            console.log("Result of File upload", result);
            var alertInfo = document.getElementById("alrtBlobInfo");
            
            result.forEach(function (element) {
                if (element.status == "Uploaded")
                    successCount++;

                if (element.status == "Failed") {
                    errorCount++;
                    failedFiles += element.blob.name + ", "
                }
            });

            alertInfo.innerHTML = (successCount > 0 ? ("<b>" + successCount + "</b> File(s) are successfully uploaded.") : "")
                + (errorCount > 0 ? ("<br> <b>" + errorCount + "</b> File(s) are failed to upload. " + failedFiles) : "");

            alertInfo.style.padding = '20px';
            alertInfo.style.width = '75 %';
            alertInfo.style.fontSize = '20px';
            alertInfo.style.margin = 'auto';
            alertInfo.style.borderRadius = '5px';
            alertInfo.styleboxShadow = 'rgb(0 0 0 / 25 %) 0px 5px 10px 2px';

            setTimeout(() => {
                const elementIsAdminFlow = document.querySelector("[name='templateUploadAddMod']");
                var silkfloIsAdminFlow = elementIsAdminFlow.getAttribute('silkflo-IsAdminFlow');

                $('#myModal').modal('hide');
                
                if (silkfloIsAdminFlow == "true") {
                    const element9 = document.querySelector("[name='Document.Content.SubContent']");
                    element9.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

                    reRenderTheAdminFileView();
                } else {
                    const element9 = document.querySelector("[name='Business.Idea.Section.Documentation.Content']");
                    element9.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

                    reRenderTheFileView();
                }
            }, 2000);
        },
        error: function (err) {
            var alertInfo = document.getElementById("alrtBlobInfo");
            alertInfo.innerHTML = "File size is too big, or may some error occurred during processing";

            alertInfo.style.padding = '20px';
            alertInfo.style.width = '75 %';
            alertInfo.style.fontSize = '20px';
            alertInfo.style.margin = 'auto';
            alertInfo.style.borderRadius = '5px';
            alertInfo.styleboxShadow = 'rgb(0 0 0 / 25 %) 0px 5px 10px 2px';
        }
    });

    //close the modal in the end
    //$("#myModal").modal('hide');
}

function handleOnSelectChange() {
    const errorMessage = document.getElementById('optTemplateSelect-error-message');

    if (optTemplateSelect.value === '') {
        errorMessage.textContent = 'Please select an option.';
    } else {
        errorMessage.textContent = '';
    }
}

function onTDocSelect(e) {
    unSelectViewers();
    var inputElement = e.firstElementChild;
    inputElement.checked = true;
    e.style.border = "2px solid #ff8a00";

}

function unSelectViewers() {
    var parentTemplateNode = document.getElementById('vwrTemplates');
    var children = parentTemplateNode.querySelectorAll("section");
    children.forEach((element) => {
        if (element) {
            var parentElement = element.childNodes;
            if (parentElement && parentElement[1]) {
                parentElement[1].style.border = "1px solid #d7d7d7";
                var inputElement = parentElement[1].firstElementChild;
                if (inputElement) {
                    inputElement.checked = false;
                }
            }
        }
    });
}

function getSelectedViewer() {
    var parentTemplateNode = document.getElementById('vwrTemplates');
    var children = parentTemplateNode.querySelectorAll("section");
    for (var i = 0; i < children.length; i++) {
        var element = children[i];
        if (element) {
            var parentElement = element.childNodes;
            if (parentElement && parentElement[1]) {
                var inputElement = parentElement[1].firstElementChild;
                if (inputElement.checked) {
                    console.log("ChildNodes", parentElement[1].childNodes);
                    console.log(parentElement[1].childNodes[3].value);
                    return parentElement[1].childNodes[3].value;
                }
            }
        }
    }
}


function deleteDocument(docuri, ideaId) {
    
    
    $('#usrDocCnfrmDg').modal('show');

    $('#usrDocCnfrmDgYes').on('click', function (e) {
        $('#usrDocCnfrmDgYes').html("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>Processing...");

        processUsrDocDelete(docuri, ideaId);
    });

    $('#tempCnfrmNo').on('click', function (e) {
        $('#usrDocCnfrmDg').modal('hide');
    });
}
function processUsrDocDelete(docuri, ideaId) {
    $.ajax({
        type: 'delete',
        //url: '@Url.Action("DeleteBlob", "Idea")',
        url: '/api/business/idea/detail/Documentation/ideaId/DeleteBlob?docUri=' + docuri + '&ideaId=' + ideaId,
        //data: fileData,
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        success: function (result) {
            $("#usrDocCnfrmDg").html(`
		                <div class="modal-dialog">
			                <div class="modal-content">
				                <div class="modal-header">
					                <h4 class="modal-title">Confirm Dialog</h4>
					                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
				                </div>
				                <div class="modal-body">
					                <p>${result.status}</p>
				                </div>
				                <div class="modal-footer">
				                </div>
			                </div>
		                </div>
                `);

            setTimeout(() => {
                $('#usrDocCnfrmDg').modal('hide');
                //const element9 = document.querySelector("[name='Business.Idea.Section.Documentation.Content']");
                //var silkfloUrl = element9.getAttribute('silkflo-url');

                //element9.innerHTML = "<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>Loading...";

                //$.get("/api/" + silkfloUrl, function (response) {
                //    element9.innerHTML = response;
                //});
                const element9 = document.querySelector("[name='Business.Idea.Section.Documentation.Content']");
                element9.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

                reRenderTheDetailView();
            }, 2000);

        },
        error: function (err) {
            console.log(err)
            //location.reload();
        }
    });

}


function DownloadTDocument() {

    var templateUr = getSelectedViewer();

    if (templateUr) {
        var decodedString = decodeURIComponent(templateUr.replace(/\+/g, " "));
        console.log("templateUr", decodedString);
        var currItem = document.querySelector("input[type=hidden][name=currItem]").value;

        var fileData = new FormData();
        fileData.append('docUri', decodedString);
        fileData.append('ideaId', currItem);

        $.ajax({
            url: '/api/business/idea/detail/Documentation/ideaId/DownloadTBlob?docUri=' + decodedString + '&ideaId=' + currItem,
            type: 'GET',
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            success: function () {
                window.location.href = '/api/business/idea/detail/Documentation/ideaId/DownloadTBlob?docUri=' + decodedString + '&ideaId=' + currItem;
            }
        });
    }
    else {
        alert("Select any Standard Document by click to Download");
    }
}

function downloadAdminViewFile(docUri) {
    // Create a new XMLHttpRequest object
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/api/business/idea/detail/Documentation/ideaId/DownloadAd?docUri=" + docUri, true);
    xhr.responseType = "arraybuffer"; // Specify the response type as arraybuffer

    xhr.onload = function () {
        if (this.status === 200) {
            // Create a Blob object from the response data
            var blob = new Blob([this.response], { type: xhr.getResponseHeader('Content-Type') });

            var fileName = xhr.getResponseHeader('filename');

            // Create a URL for the blob object
            var url = window.URL.createObjectURL(blob);

            // Create a hidden link and simulate a click on it to trigger the download
            var link = document.createElement("a");
            link.style.display = "none";
            link.href = url;
            link.download = fileName;
            document.body.appendChild(link);
            link.click();

            // Cleanup
            document.body.removeChild(link);
            window.URL.revokeObjectURL(url);
        }
    };

    // Send the form data as the request body
    xhr.send();
}

function deleteAdminViewFile(docUri) {
    $('#delFileConfrm').modal('show');

    $('#tempCnfrmYes').on('click', function (e) {
        processDeletionOnAdminFile(docUri);
    });

    $('#tempCnfrmNo').on('click', function (e) {
        $('#delFileConfrm').modal('hide');
    }); 
}

function processDeletionOnAdminFile(docUri) {
    $('#tempCnfrmYes').html("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>Processing...");

    $.ajax({
        type: 'DELETE',
        url: "/api/business/idea/detail/Documentation/ideaId/DeleteBlobAd?docUri=" + docUri,
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        success: function (data) {
            $("#delFileConfrm").html(`
		                <div class="modal-dialog">
			                <div class="modal-content">
				                <div class="modal-header">
					                <h4 class="modal-title">Confirm Dialog</h4>
					                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
				                </div>
				                <div class="modal-body">
					                <p>${data.message}</p>
				                </div>
				                <div class="modal-footer">
				                </div>
			                </div>
		                </div>
                `);

            setTimeout(() => {
                $('#delFileConfrm').modal('hide');
                const element9 = document.querySelector("[name='Document.Content.SubContent']");
                element9.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';
                reRenderTheAdminFileView();
            }, 5000);

        },
        error: function (xhr, textStatus, errorThrown) {
        }
    });
}


function downloadAllSelectedFiles() {
    var checkedValues = [];
    var checkBoxes = document.getElementsByName("admTemplateChecksBx");
    var currItem = document.querySelector("input[type=hidden][name=currItem]").value;

    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked) {
            checkedValues.push(checkBoxes[i].value);
        }
    }

    if (checkedValues) {
        var fileData = new FormData();
        // value of the selected option
        fileData.append('docUris', checkedValues);
        fileData.append('ideaId', currItem);
        fileData.append('isTemplate', true);

        // Create a new XMLHttpRequest object
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/api/business/idea/detail/Documentation/ideaId/Downloads", true);
        xhr.responseType = "arraybuffer"; // Specify the response type as arraybuffer

        // Set the headers as needed
        //xhr.setRequestHeader("Content-Type", "multipart/form-data");

        // Set up the onload function to handle the response
        xhr.onload = function () {
            if (this.status === 200) {
                // Create a Blob object from the response data
                var blob = new Blob([this.response], { type: "application/zip" });

                // Create a URL for the blob object
                var url = window.URL.createObjectURL(blob);

                // Create a hidden link and simulate a click on it to trigger the download
                var link = document.createElement("a");
                link.style.display = "none";
                link.href = url;
                link.download = "TemplateDocuments.zip";
                document.body.appendChild(link);
                link.click();

                // Cleanup
                document.body.removeChild(link);
                window.URL.revokeObjectURL(url);
            }
        };

        // Send the form data as the request body
        xhr.send(fileData);
    }

    return;

    if (checkedValues) {
        var fileData = new FormData();
        // value of the selected option
        fileData.append('docUris', checkedValues);
        fileData.append('ideaId', currItem);
        fileData.append('isTemplate', true);

        $.ajax({
            type: 'post',
            url: "/api/business/idea/detail/Documentation/ideaId/Downloads",
            data: fileData,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            success: function (data, status, xhr) {
                console.log("File response", data);

                //// Create a Blob object from the response data
                //var blob = new Blob([atob(data)], { type: "application/zip" });

                //// Create a URL for the blob object
                //var url = window.URL.createObjectURL(blob);

                //// Create a hidden link and simulate a click on it to trigger the download
                //var link = document.createElement("a");
                //link.style.display = "none";
                //link.href = url;
                //link.download = "filenames.zip";
                //document.body.appendChild(link);
                //link.click();

                //// Cleanup
                //document.body.removeChild(link);
                //window.URL.revokeObjectURL(url);

                //// Create a hidden anchor tag with the download URL
                //var downloadLink = $("<a style='display:none;'/>");
                //downloadLink.attr("href", "data:application/zip;base64," + data);
                //downloadLink.attr("download", "IdeaDocuments.zip");
                //$("body").append(downloadLink);

                //// Simulate a click on the anchor tag to trigger the download
                //downloadLink[0].click();

                //// Remove the anchor tag from the page
                //downloadLink.remove();
            },
            error: function (xhr, textStatus, errorThrown) {
            }
        });
    }
}

function deleteAllSelectedFiles() {

    
    $('#delFileConfrm').modal('show');

    $('#tempCnfrmYes').on('click', function (e) {
        processSelectedDocsDeletion();
    }); 

    $('#tempCnfrmNo').on('click', function (e) {
        $('#delFileConfrm').modal('hide');
    }); 
}

function processSelectedDocsDeletion() {
    $('#tempCnfrmYes').html("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>Processing...");
    var checkedValues = [];
    var checkBoxes = document.getElementsByName("admTemplateChecksBx");
    var currItem = document.querySelector("input[type=hidden][name=currItem]").value;

    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked) {
            checkedValues.push(checkBoxes[i].value);
        }
    }


    if (checkedValues) {
        var fileData = new FormData();
        // value of the selected option
        fileData.append('docUris', checkedValues);
        fileData.append('ideaId', currItem);
        fileData.append('isTemplate', true);

        $.ajax({
            type: 'post',
            url: "/api/business/idea/detail/Documentation/ideaId/DeleteBlobs",
            data: fileData,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            success: function (data) {
                $("#delFileConfrm").html(`
		                <div class="modal-dialog">
			                <div class="modal-content">
				                <div class="modal-header">
					                <h4 class="modal-title">Confirm Dialog</h4>
					                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
				                </div>
				                <div class="modal-body">
					                <p>${data.message}</p>
				                </div>
				                <div class="modal-footer">
				                </div>
			                </div>
		                </div>
                `);

                setTimeout(() => {
                    $('#delFileConfrm').modal('hide');
                    const element9 = document.querySelector("[name='Document.Content.SubContent']");
                    element9.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';
                    reRenderTheAdminFileView();
                }, 5000);

            },
            error: function (xhr, textStatus, errorThrown) {
            }
        });
    }

}

function reRenderTheAdminFileView() {
    $.get("/api/business/idea/detail/TemplateDocumentation/ideaId/GetTemplates", function (response) {
        const element9 = document.querySelector("[name='Document.Content.SubContent']");
        element9.innerHTML = response;
    });
}

function reRenderTheFileView() {

    const element9 = document.querySelector("[name='Business.Idea.Section.Documentation.Content']");
    var silkfloUrl = element9.getAttribute('silkflo-url');

    element9.innerHTML = "<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>Loading...";

    $.get("/api/" + silkfloUrl, function (response) {
        element9.innerHTML = response;
    });
}


function reRenderTheDetailView() {

    const element9 = document.querySelector("[id='Detail_BusinessIdea']");
    element9.innerHTML = "<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>Loading...";

    window.location.href = window.location.href;


    //$.get(window.location.href.split('/Documentation')[0], function (response) {
    //    element9.innerHTML = response;
    //    window.location.href = window.location.href + "Documentation";
    //});
}

//function DownloadDocuments() {
//    var fileData = new FormData();
//    var currItem = document.querySelector("input[type=hidden][name=currItem]").value;
//    fileData.append('ideaId', currItem);

//    $.ajax({
//        type: 'post',
//        url: '/api/business/idea/detail/Documentation/ideaId/DownloadBlob',
//        // url: '@Url.Action("DownloadBlob", "Idea")',
//        data: fileData,
//        contentType: false, // Not to set any content header
//        processData: false, // Not to process data
//        success: function (result) {
//            //location.reload();
//        },
//        error: function (err) {
//            alert(err.statusText);
//        }
//    });
//}

function selectAllDocuments() {

    const documents = document.getElementsByClassName("fileSelected");
    if (arr.includes('1')) {
        for (let i = 0; i < documents.length; i++) {
            const item = documents[i];
            item.checked = false;
        }
        arr.pop(1);
    } else {
        for (let i = 0; i < documents.length; i++) {
            const item = documents[i];
            item.checked = true;
        }
        arr.push("1");
    }

}

function addNew() {

    const templateName = document.getElementById("documentId").value;

}

function downloadAllUserSelectedDocuments(){
    var checkedValues = [];
    var checkBoxes = document.getElementsByName("usrDocumentChk");
    var currItem = document.querySelector("input[type=hidden][name=currItem]").value;

    for (var i = 0; i < checkBoxes.length; i++) {
        debugger
        if (checkBoxes[i].checked) {
            checkedValues.push(checkBoxes[i].value);
        }
    }

    if (checkedValues) {
        var fileData = new FormData();

        // value of the selected option
        fileData.append('selectedDocs', checkedValues.join(","));
        fileData.append('ideaId', currItem);

        // Create a new XMLHttpRequest object
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/api/business/idea/detail/Documentation/ideaId/DownloadBlob", true);
        xhr.responseType = "arraybuffer"; // Specify the response type as arraybuffer

        // Set the headers as needed
        //xhr.setRequestHeader("Content-Type", "multipart/form-data");

        // Set up the onload function to handle the response
        xhr.onload = function () {
            if (this.status === 200) {
                // Create a Blob object from the response data
                var blob = new Blob([this.response], { type: "application/zip" });

                // Create a URL for the blob object
                var url = window.URL.createObjectURL(blob);

                // Create a hidden link and simulate a click on it to trigger the download
                var link = document.createElement("a");
                link.style.display = "none";
                link.href = url;
                link.download = "UserDocuments.zip";
                document.body.appendChild(link);
                link.click();

                // Cleanup
                document.body.removeChild(link);
                window.URL.revokeObjectURL(url);
            }
        };

        // Send the form data as the request body
        xhr.send(fileData);
    }

    return;
}


function showDialogForDeletingDocs() {
    $('#usrDocCnfrmDg').modal('show');

    $('#usrDocCnfrmDgYes').on('click', function (e) {
        $('#usrDocCnfrmDgYes').html("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>Processing...");

        deleteAllUserSelectedDocuments();
    });

    $('#tempCnfrmNo').on('click', function (e) {
        $('#usrDocCnfrmDg').modal('hide');
    });
}

function deleteAllUserSelectedDocuments() {
    var checkedValues = [];
    var checkBoxes = document.getElementsByName("usrDocumentChk");
    var currItem = document.querySelector("input[type=hidden][name=currItem]").value;

    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked) {
            checkedValues.push(checkBoxes[i].value);
        }
    }

    if (checkedValues) {
        $.ajax({
            type: 'delete',
            //url: '@Url.Action("DeleteBlob", "Idea")',
            url: '/api/business/idea/detail/Documentation/ideaId/DeleteUserBlobs?selectedDocs=' + checkedValues.join(",") + '&ideaId=' + currItem,
            //data: fileData,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            success: function (result) {
                $("#usrDocCnfrmDg").html(`
		                <div class="modal-dialog">
			                <div class="modal-content">
				                <div class="modal-header">
					                <h4 class="modal-title">Confirm Dialog</h4>
					                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
				                </div>
				                <div class="modal-body">
					                <p>${result.message}</p>
				                </div>
				                <div class="modal-footer">
				                </div>
			                </div>
		                </div>
                `);

                setTimeout(() => {
                    $('#usrDocCnfrmDg').modal('hide');
                    
                    const element9 = document.querySelector("[name='Business.Idea.Section.Documentation.Content']");
                    element9.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';

                    reRenderTheDetailView();
                }, 2000);

            },
            error: function (err) {
                console.log(err)
                //location.reload();
            }
        });
    }

}