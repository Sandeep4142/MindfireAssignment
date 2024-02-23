//using web method save notes
//$(document).ready(function () {
//    $('#addNote').on('click', function (event) {

//        var notes = $("#note").val()

//        var urlParams = new URLSearchParams(window.location.search);
//        var id = urlParams.get('id');
//        var objectType = $("#ObjType").val();

//        if (notes != "") {
//            var notesData = {
//                NoteText : notes,
//                ObjectId : id,
//                ObjectType: objectType
//            }

//            $.ajax({
//                url: 'UserDetails2.aspx/AddNoteToDatabase',
//                data: JSON.stringify( notesData ),
//                contentType: 'application/json; charset=utf-8',
//                dataType: 'json',
//                success: function (response) {
//                    console.log("Notes added");
//                    $("#note").val("");
//                },
//                error: function (xhr, status, error) {
//                    console.error(xhr.responseText);
//                }
//            });
//        } else {
//            console.log("failed");
//        }
//    });

//});



//another way using handler

$(document).ready(function () {
    $('#addNote').on('click', function (event) {

        var url = window.location.href;
        var pageName = url.substring(url.lastIndexOf("/") + 1).split('.')[0];
        console.log("Page name - " + pageName)

        var notes = $("#note").val()

        var urlParams = new URLSearchParams(window.location.search);
        var id = urlParams.get('id');
        var objectType = $("#ObjType").val();

        if (notes != "") {
            var formData = new FormData();
            formData.append('NoteText', notes);
            formData.append('ObjectId', id);
            formData.append('ObjectType', objectType);

            $.ajax({
                url: 'NotesUploader.ashx',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    console.log("Notes added");
                    $("#note").val("");
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        } else {
            console.log("failed");
        }

    });
});
