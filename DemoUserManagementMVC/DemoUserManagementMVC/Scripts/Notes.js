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

    // Function to initialize sorting and pagination for the table
    function initializeTable(tableId) {
        const table = document.getElementById(tableId);
        const tbody = table.querySelector('tbody');
        const rows = Array.from(tbody.getElementsByTagName("tr"));

        const pagination = document.getElementById("pagination");
        const itemsPerPage = 5; 
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

        function sortTable(columnIndex) {
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
        const headers = table.querySelectorAll('th a');
        headers.forEach((header, index) => {
            header.onclick = () => sortTable(index);
        });
    }

    // Call the function to initialize the table
    initializeTable("notesTable");

});

