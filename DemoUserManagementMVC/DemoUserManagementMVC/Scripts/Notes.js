$(document).ready(function () {
    console.log("Notes user control")

    $("#addNoteBtn").click(function () {
        console.log("Note btn clicked")

        if ($("#noteText").val().trim() !== "") {

            var objId = $("#noteObjectId").val()

            var formData = new FormData();
            formData.append('objectId', $("#noteObjectId").val());
            formData.append('objectType', $("#noteObjectType").val());
            formData.append('noteText', $("#noteText").val());

            $.ajax({
                url: '/UserDetails/SaveNote',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    console.log("Note added successfully.");
                },
                error: function (xhr, status, error) {
                    console.error("Error adding note: ", error);
                }
            });

        } else {
            alert("Please enter a note text.");
        }
    });
});