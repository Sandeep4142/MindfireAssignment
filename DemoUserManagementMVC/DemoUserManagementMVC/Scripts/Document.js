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

    // for sorting/paging
    function initializeDocumentTable(tableId) {
        const table = document.getElementById(tableId);
        const tbody = document.getElementById(tableId + "Body");
        const rows = Array.from(tbody.getElementsByTagName("tr"));

        const pagination = document.getElementById("documentPagination");
        const itemsPerPage = 5; // Adjust as needed
        let totalPages = Math.ceil(rows.length / itemsPerPage);
        let currentPage = 1;

        let sortedColumnIndex = null;
        let ascending = true;

        function displayRows() {
            tbody.innerHTML = "";
            const startIndex = (currentPage - 1) * itemsPerPage;
            const endIndex = startIndex + itemsPerPage;
            const currentPageRows = rows.slice(startIndex, endIndex);
            currentPageRows.forEach(row => tbody.appendChild(row));
        }

        function updatePagination() {
            pagination.innerHTML = "";
            for (let i = 1; i <= totalPages; i++) {
                const button = document.createElement("button");
                button.textContent = i;
                button.addEventListener("click", function () {
                    currentPage = i;
                    displayRows();
                    updatePagination();
                });
                pagination.appendChild(button);
            }
        }

        function sortTable(tableId, columnIndex) {
            if (sortedColumnIndex === columnIndex) {
                ascending = !ascending;
            } else {
                sortedColumnIndex = columnIndex;
                ascending = true;
            }

            const descending = !ascending;

            rows.sort((a, b) => {
                const cellA = a.cells[columnIndex].textContent.trim().toLowerCase();
                const cellB = b.cells[columnIndex].textContent.trim().toLowerCase();
                if (cellA < cellB) return descending ? 1 : -1;
                if (cellA > cellB) return descending ? -1 : 1;
                return 0;
            });

            displayRows();
            updatePagination();
        }

        displayRows();
        updatePagination();

        // Attach sorting event listeners to table headers
        const headers = table.querySelectorAll('th');
        headers.forEach((header, index) => {
            header.onclick = () => sortTable(tableId, index);
        });
    }

    // Call the function to initialize the table
    initializeDocumentTable("documentTable");

});
