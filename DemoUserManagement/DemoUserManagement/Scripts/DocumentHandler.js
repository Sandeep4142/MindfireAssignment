$(document).ready(function () {

    $('#addDocument').on('click', function (event) {
        // Get the selected file
        event.preventDefault();

        var fileInput = $('#DocumentUpload')[0];
        var file = fileInput.files[0];

        var urlParams = new URLSearchParams(window.location.search);
        var id = urlParams.get('id');

        var fileTypeValue = parseInt($('#Document').val());
        var objectType = $("#ObjType").val();

        if (file) {
            var formData = new FormData();
            formData.append('File', file);
            formData.append('ObjectId', id);
            formData.append('FileType', fileTypeValue);
            formData.append('ObjectType', objectType);

            $.ajax({
                url: 'DocumentUpload2.ashx',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    // Handle the success response
                    console.log("Documents uploaded");
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        } else {
            console.log("File not selected");
        }

    });
});