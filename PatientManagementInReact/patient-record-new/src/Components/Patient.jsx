import React from 'react'
import MaterialTable from 'material-table';

const Patient = () => {

  const columns =[
    {title:"Name", field:"name"},
    {title:"Date Of Birth", field:"dateOfBirth"},
    {title:"Email", field:"email"},
    {title:"Phone No", field:"phoneNo"},
    {title:"Medical History", field:"medicalHistory"}
  ]

  return (
    <div>
      <h1 align="center">Patient List</h1>
      <MaterialTable
    // other props
    data={query =>
        new Promise((resolve, reject) => {
            let url= 'http://localhost:3000/patients?'
            fetch(url).then(response => response.json()).then(response =>{
              resolve({
                data: response, 
                page: 1, // current page number
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
