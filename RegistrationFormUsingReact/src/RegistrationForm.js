import React from "react";
import './RegistrationForm.css'

const RegistrationForm = () => {
  return (
    <div className="container">
      <div id="registrationForm">
        <h1>Registration Form</h1>
        <form>
          <div id="personalDetails">
            <input type="hidden" id="userId" />
            <h2>Personal Information</h2>
            <div className="row">
              <div className="inputField">
                <label htmlFor="firstName" className="required">
                  First Name :
                </label>
                <input type="text" name="First Name" id="firstName" required />
                <div className="errorMessage"></div>
              </div>
              <div className="inputField">
                <label htmlFor="lastName" className="required">
                  Last Name :
                </label>
                <input type="text" name="Last Name" id="lastName" required />
                <div className="errorMessage"></div>
              </div>
            </div>
            <div className="row">
              <div id="fatherNameDiv" className="inputField">
                <label htmlFor="fatherName" className="required">
                  Father's Name :
                </label>
                <input
                  type="text"
                  name="Father's Name"
                  id="fatherName"
                  required
                />
                <div className="errorMessage"></div>
              </div>
              <div id="motherNameDiv" className="inputField">
                <label htmlFor="motherName" className="required">
                  Mother's Name :
                </label>
                <input
                  type="text"
                  name="Mother's Name"
                  id="motherName"
                  required
                />
                <div className="errorMessage"></div>
              </div>
            </div>
            <div className="row">
              <div id="dateOfBirthDiv" className="inputField">
                <label htmlFor="dateOfBirth" className="required">
                  Date of Birth :
                </label>
                <input
                  type="date"
                  name="Date Of Birth"
                  id="dateOfBirth"
                  required
                />
                <div className="errorMessage"></div>
              </div>
              <div id="genderDiv" className="inputField">
                <label htmlFor="male" id="genderLabel" className="required">
                  Gender :
                </label>
                <div className="options">
                  <input
                    type="radio"
                    name="Gender"
                    value="male"
                    id="male"
                    required
                  />
                  <label htmlFor="male" className="">
                    Male
                  </label>
                  <input
                    type="radio"
                    name="Gender"
                    value="female"
                    id="female"
                    required
                  />
                  <label htmlFor="female" className="">
                    Female
                  </label>
                  <input
                    type="radio"
                    name="Gender"
                    value="other"
                    id="other"
                    required
                  />
                  <label htmlFor="other" className="">
                    Other
                  </label>
                  <div className="errorMessage"></div>
                </div>
              </div>
            </div>
          </div>

          <div id="contactDetails">
            <h2>Contact Information</h2>
            <div className="row" id="mobileNoDiv">
              <div className="inputField">
                <label htmlFor="primaryMobileNo" className="required">
                  Mobile No :{" "}
                </label>
                <input
                  type="number"
                  name="Primary Mobile No"
                  id="primaryMobileNo"
                  required
                />
                <div className="errorMessage"></div>
              </div>

              <div className="inputField">
                <label htmlFor="alternateMobileNo" className="">
                  Alternate Mobile No :
                </label>
                <input
                  type="number"
                  name="Alternate Mobile No"
                  id="alternateMobileNo"
                />
                <div className="errorMessage"></div>
              </div>
            </div>

            <div className="row" id="emailIdDiv">
              <div className="inputField">
                <label htmlFor="primaryEmailId" className="required">
                  Email ID :{" "}
                </label>
                <input
                  type="email"
                  name="Primary Email Id"
                  id="primaryEmailId"
                  required
                />
                <div className="errorMessage"></div>
              </div>
              <div className="inputField">
                <label htmlFor="alternateEmailId" className="">
                  Alternate Email ID :
                </label>
                <input
                  type="email"
                  name="Alternat Email Id"
                  id="alternateEmailId"
                />
                <div className="errorMessage"></div>
              </div>
            </div>

            <div id="currentAdress">
              <h3>Current Address</h3>
              <div className="row">
                <div className="inputField">
                  <label htmlFor="currentLocality" className="required">
                    Locality :
                  </label>
                  <textarea
                    name="Current Locality"
                    id="currentLocality"
                    required
                  ></textarea>
                  <div className="errorMessage"></div>
                </div>
                <div className="inputField">
                  <label htmlFor="currentCity" className="required">
                    City :{" "}
                  </label>
                  <input
                    type="text"
                    name="Current City"
                    id="currentCity"
                    required
                  />
                  <div className="errorMessage"></div>
                </div>
              </div>
              <div className="row">
                <div className="inputField">
                  <label htmlFor="currentCountry" className="required">
                    Country :{" "}
                  </label>
                  <input
                    type="text"
                    name="Current Country"
                    id="currentCountry"
                    required
                  />
                  <div className="errorMessage"></div>
                </div>
                <div className="inputField">
                  <label htmlFor="currentState" className="required">
                    State :{" "}
                  </label>
                  <input
                    type="text"
                    name="Current State"
                    id="currentState"
                    required
                  />
                  <div className="errorMessage"></div>
                </div>
                <div className="inputField">
                  <label htmlFor="currentPincode" className="required">
                    Pincode :{" "}
                  </label>
                  <input
                    type="number"
                    name="Current Pincode"
                    id="currentPincode"
                    required
                  />
                  <div className="errorMessage"></div>
                </div>
              </div>
            </div>

            <div id="permanentAddress">
              <h3>Permanent Address</h3>
              <div>
                <input
                  type="checkbox"
                  name="sameAsCurrentAddress"
                  id="sameAsCurrentAddress"
                />
                <label htmlFor="sameAsCurrentAddress">
                  Same as current address
                </label>
              </div>
              <div className="row">
                <div className="inputField">
                  <label htmlFor="permanentLocality" className="required">
                    Locality :
                  </label>
                  <textarea
                    name="Permanent Locality"
                    id="permanentLocality"
                    required
                  ></textarea>
                  <div className="errorMessage"></div>
                </div>
                <div className="inputField">
                  <label htmlFor="permanentCity" className="required">
                    City :{" "}
                  </label>
                  <input
                    type="text"
                    name="Permanent City"
                    id="permanentCity"
                    required
                  />
                  <div className="errorMessage"></div>
                </div>
              </div>
              <div className="row">
                <div className="inputField">
                  <label htmlFor="permanentCountry" className="required">
                    Country :{" "}
                  </label>
                  <input
                    type="text"
                    name="Permanent Country"
                    id="permanentCountry"
                    required
                  />
                  <div className="errorMessage"></div>
                </div>
                <div className="inputField">
                  <label htmlFor="permanentState" className="required">
                    State :{" "}
                  </label>
                  <input
                    type="text"
                    name="Permanent State"
                    id="permanentState"
                    required
                  />
                  <div className="errorMessage"></div>
                </div>
                <div className="inputField">
                  <label htmlFor="permanentPincode" className="required">
                    Pincode :
                  </label>
                  <input
                    type="number"
                    name="Permanent Pincode"
                    id="permanentPincode"
                    required
                  />
                  <div className="errorMessage"></div>
                </div>
              </div>
            </div>
          </div>

          <div id="educationDetails">
            <h2>Education Details</h2>
            <table>
              <thead>
                <tr>
                    <th></th>
                    <th className="required">Institution</th>
                    <th className="required">Board/University</th>
                    <th className="required">Marks Obtained(in %)</th>
                </tr>           
              </thead>
              <tbody>
              <tr>
                <th>10th</th>
                <td>
                  <div className="inputField">
                    <input
                      type="text"
                      name="Tenth Institution"
                      id="tenthInstitution"
                      required
                    />
                    <div className="errorMessage"></div>
                  </div>
                </td>
                <td>
                  <div className="inputField">
                    <input
                      type="text"
                      name="Tenth Board"
                      id="tenthBoard"
                      required
                    />
                    <div className="errorMessage"></div>
                  </div>
                </td>
                <td>
                  <div className="inputField">
                    <input
                      type="number"
                      name="Tenth Percentage"
                      id="tenthPercentage"
                      required
                    />
                    <div className="errorMessage"></div>
                  </div>
                </td>
              </tr>
              <tr>
                <th>12th</th>
                <td>
                  <div className="inputField">
                    <input
                      type="text"
                      name="Twelveth Institution"
                      id="twelvethInstitution"
                      required
                    />
                    <div className="errorMessage"></div>
                  </div>
                </td>
                <td>
                  <div className="inputField">
                    <input
                      type="text"
                      name="Twelveth Board"
                      id="twelvethBoard"
                      required
                    />
                    <div className="errorMessage"></div>
                  </div>
                </td>
                <td>
                  <div className="inputField">
                    <input
                      type="number"
                      name="Twelveth Percentage"
                      id="twelvethPercentage"
                      required
                    />
                    <div className="errorMessage"></div>
                  </div>
                </td>
              </tr>
              <tr>
                <th>Graduation</th>
                <td>
                  <div className="inputField">
                    <input
                      type="text"
                      name="Graduation Institution"
                      id="graduationInstitution"
                      required
                    />
                    <div className="errorMessage"></div>
                  </div>
                </td>
                <td>
                  <div className="inputField">
                    <input
                      type="text"
                      name="Graduation University"
                      id="graduationUniversity"
                      required
                    />
                    <div className="errorMessage"></div>
                  </div>
                </td>
                <td>
                  <div className="inputField">
                    <input
                      type="number"
                      name="Graduation Percentage"
                      id="graduationPercentage"
                      required
                    />
                    <div className="errorMessage"></div>
                  </div>
                </td>
              </tr>
              </tbody>
            </table>
          </div>

          <div id="declarationDiv">
            <h2>Declaration</h2>
            <div>
              <input
                type="checkbox"
                name="declaration"
                id="declaration"
                required
              />
              <label htmlFor="declaration">
                I declare that all information provided by me are correct.
              </label>
              <div className="errorMessage"></div>
            </div>
          </div>

          <div id="submitResetDiv">
            <input type="submit" value="Submit" id="submitBtn" />
            <input type="reset" value="Reset" id="resetBtn" />
          </div>
        </form>
      </div>

      <div id="userTable">
        <h1>User Table</h1>
        <table>
          <thead>
            <tr>
                <th>Sl No</th>
                <th>Name</th>
                <th>Email</th>
                <th>Update</th>
                <th>Delete</th>
            </tr>
          </thead>
        </table>
      </div>
    </div>
  );
};

export default RegistrationForm;
