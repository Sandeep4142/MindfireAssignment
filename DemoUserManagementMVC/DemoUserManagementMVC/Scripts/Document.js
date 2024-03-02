$(document).ready(function () {
    console.log("Document user control")

    $("#addDocument").click(function () {
        console.log("doc btn clicked")
        var fileInput = $('#DocumentUpload')[0];
        var file = fileInput.files[0];
        var fileTypeValue = parseInt($('#Document').val());

        var formData = new FormData();
        formData.append('objectId', $("#docObjectId").val());
        formData.append('objectType', $("#docObjectType").val());
        formData.append('file', file);
        formData.append('fileType', fileTypeValue);

        if (file) {
            $.ajax({
                url: '/UserDetails/SaveDocument',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    console.log("Document added successfully.");
                },
                error: function (xhr, status, error) {
                    console.error("Error uploading ", error);
                }
            });
        } else {
            alert("Please add a document.");
        }
    });
});
