import React from 'react'
import MaterialTable from 'material-table';

const Patient = () => {

    const columns = [
        { title:"Name", field:"name"},
        { title:"Date Of Birth", field:"dateOfBirth"},
        { title:"Email", field:"Email"},
        { title:"Phone No", field:"phoneNo"},
        { title:"Medical history", field:"medicalHistory"}
    ]
  return (
    <div>
      <MaterialTable
    title="Patient List"
    columns = {columns}
    data={query =>
        new Promise((resolve, reject) => {
            // prepare your data and then call resolve like this:
            let url = 'http://localhost:3000/patients?'
            fetch(url).then(response => response.json()).then(response =>{
                resolve({
                    data: response ,// your data array
                    page: 1,// current page number
                    totalCount: 200 // total row number
                });
            })
            
        })
    }
/>;
    </div>
  )
}

export default Patient
