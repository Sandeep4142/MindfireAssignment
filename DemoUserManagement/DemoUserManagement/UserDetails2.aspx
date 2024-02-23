<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetails2.aspx.cs" Inherits="DemoUserManagement.UserDetails2" %>

<%@ Register Src="~/Notes.ascx" TagPrefix="uc1" TagName="Notes" %>
<%@ Register Src="~/Document.ascx" TagPrefix="uc1" TagName="Document" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <!-- Personal Details -->
        <div class="p-5" id="personalDetails">
            <h4>Personal Details</h4>
            <div class="row">
                <div class="col-md-4">
                    <label for="firstName" class="form-label required">First Name :</label>
                    <div>
                        <input class="form-control" type="text" name="First Name" id="firstName" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <label for="middleName" class="form-label">Middle Name : </label>
                    <input type="text" name="Middle Name" id="middleName" class="form-control" />
                </div>
                <div class="col-md-4">
                    <label for="lastName" class="form-label required">Last Name :</label>
                    <div>
                        <input type="text" name="Last Name" id="lastName" class="form-control" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label for="fatherName" class="form-label required">Father's name :</label>
                    <div>
                        <input type="text" name="Father Name" id="fatherName" class="form-control" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="motherName" class="form-label required">Mother's Name :</label>
                    <div>
                        <input type="text" name="Mother Name" id="motherName" class="form-control" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label for="dateOfBirth" class="form-label required">Date of Birth :</label>
                    <div>
                        <input type="date" name="Date Of Birth" id="dateOfBirth" class="form-control" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="male" class="form-label required">Gender : </label>
                    <div class="d-flex" id="genderOptions">
                        <div class="form-check me-2">
                            <input class="form-check-input" type="radio" name="Gender" id="male" value="Male" />
                            <label class="form-check-label" for="male">Male </label>
                        </div>
                        <div class="form-check me-2">
                            <input class="form-check-input" type="radio" name="Gender" id="female" value="Female" />
                            <label class="form-check-label" for="female">Female </label>
                        </div>
                        <div class="form-check me-2">
                            <input class="form-check-input" type="radio" name="Gender" id="other" value="Other" />
                            <label class="form-check-label" for="other">Other </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label for="aadharCardNo" class="form-label required">Aadhaar No. :</label>
                    <div id="inputAadharCardNo">
                        <div>
                            <input type="number" name="Aadhar Card No" id="aadharCardNo" class="form-control" value="" />
                            <div class="errorMessage"></div>
                        </div>
                        <div>
                            <input class="form-control" type="file" name="aadharCard" id="aadharCard" />
                            <label id="aadharCardFileName"></label>
                            <div class="errorMessage"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="profilePic" class="form-label required">Upload your passport size photograph :</label>
                    <div>
                        <input class="form-control" type="file" id="profilePic" name="Profile Pic" accept="image/*" />
                        <label id="profilPicFilename"></label>
                        <div class="errorMessage"></div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Contact Details -->
        <div class="p-5" id="contactDetails">
            <h4>Contact Details</h4>
            <div class="row">
                <div class="col-md-6">
                    <label for="primaryMobileno" class="form-label required">Mobile No :</label>
                    <div>
                        <input class="form-control" type="number" name="Primary Mobile No" id="primaryMobileno" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="alternateMobileno" class="form-label">Alternate Mobile No :</label>
                    <div class="">
                        <input type="number" name="Alternate Mobile No" id="alternateMobileno" class="form-control" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="primaryEmailId" class="form-label required">Email ID :</label>
                    <div>
                        <input type="email" name="Primary Email Id" id="primaryEmailID" class="form-control" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="alternateEmailId" class="form-label">Alternate Email ID :</label>
                    <div>
                        <input type="email" name="Alternate Email Id" id="alternateEmailId" class="form-control" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="password" class="form-label">Password :</label>
                    <div>
                        <input type="text" name="Password" id="password" class="form-control" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
            </div>

            <!-- Current Address -->
            <h5>Current Address</h5>
            <div class="row">
                <div class="col-md-6">
                    <label for="currentLocality" class="form-label required">Locality :</label>
                    <div>
                        <input type="text" class="form-control" name="Current Locality" id="currentLocality" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="currentCity" class="form-label required">City :</label>
                    <div>
                        <input class="form-control" type="text" name="Current City" id="currentCity" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <label for="currentCountry" class="form-label required">Country :</label>
                    <div>
                        <select name="Current Country" class="form-control" id="currentCntry">
                        </select>
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <label for="currentState" class="form-label required">State :</label>
                    <div>
                        <select class="form-control" name="Current State" id="currentState">
                        </select>
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <label for="currentPincode" class="form-label required">Pincode :</label>
                    <div>
                        <input class="form-control" type="number" name="Current Pincode" id="currentPincode" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
            </div>

            <!-- Permanent Address -->
            <h5>Permanent Address</h5>
            <div class="form-check">
                <input class="form-check-input" type="checkbox" value="" id="sameAsCurrentAddressCheck" />
                <label class="form-check-label" for="sameAsCurrentAddressCheck">Same as current address</label>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label for="permanentLocality" class="form-label required">Locality :</label>
                    <div>
                        <input type="text" class="form-control" name="Permanent Locality" id="permanentLocality" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="permanentCity" class="form-label required">City :</label>
                    <div>
                        <input class="form-control" type="text" name="Permanent City" id="permanentCity" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <label for="permanentCountry" class="form-label required">Country :</label>
                    <div>
                        <select name="Permanent Country" class="form-control" id="permanentCntry">
                        </select>
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <label for="permanentState" class="form-label required">State :</label>
                    <div>
                        <select class="form-control" name="Permanent State" id="permanentState">
                        </select>
                        <div class="errorMessage"></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <label for="permanentPincode" class="form-label required">Pincode :</label>
                    <div>
                        <input class="form-control" type="number" name="Permanent Pincode" id="permanentPincode" />
                        <div class="errorMessage"></div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Education details -->

        <div class="p-5" id="educationDetails">
            <h4>Eduaction Details</h4>
            <div class="table-responsive-lg">
                <table class="table table-bordered" id="tableEducationDetails">
                    <thead>
                        <tr>
                            <th>Standard</th>
                            <th class="required">Institution</th>
                            <th class="required">Board/University</th>
                            <th class="required">Marks Obtained(in %)</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="10thDetails">
                            <td>10th</td>
                            <td>
                                <div>
                                    <input class="form-control" type="text" id="10thInst" name="10th Institution" />
                                    <div class="errorMessage"></div>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <input class="form-control" type="text" id="10thBoard" name="10th Board" />
                                    <div class="errorMessage"></div>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <input class="form-control" type="number" name="10th Percentage" id="10thMarks" />
                                    <div class="errorMessage"></div>
                                </div>
                            </td>
                        </tr>
                        <tr id="12thDetails">
                            <td>12th</td>
                            <td>
                                <div>
                                    <input class="form-control" type="text" id="12thInst" name="12th Institution" />
                                    <div class="errorMessage"></div>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <input class="form-control" type="text" id="12thBoard" name="12th Board" />
                                    <div class="errorMessage"></div>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <input class="form-control" type="number" name="12th Percentage" id="12thMarks" />
                                    <div class="errorMessage"></div>
                                </div>
                            </td>
                        </tr>
                        <tr id="GraduationDetails">
                            <td>Graduation</td>
                            <td>
                                <div>
                                    <input class="form-control" type="text" id="gradInst" name="Graduation Institution" />
                                    <div class="errorMessage"></div>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <input class="form-control" type="text" id="gradUni" name="Graduation University" />
                                    <div class="errorMessage"></div>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <input class="form-control" type="number" id="gradMarks" name="Graduation Marks" />
                                    <div class="errorMessage"></div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>


        <!-- Extra details -->

        <div class="p-5" id="extraDetails">
            <h4>Extra Details</h4>

            <!-- Hobbies -->

            <h5>Hobbies</h5>
            <div class="d-flex flex-wrap">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" name="Hobbies" value="Cricket" id="cricket" />
                    <label class="form-check-label" for="cricket">Cricket </label>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" name="Hobbies" value="Singing" id="singing" />
                    <label class="form-check-label" for="singing">Singing </label>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" name="Hobbies" value="Dancing" id="dancing" />
                    <label class="form-check-label" for="dancing">Dancing </label>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" name="Hobbies" value="Photography" id="photography" />
                    <label class="form-check-label" for="photography">Photography </label>
                </div>

                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="otherHobbies" />
                    <label class="form-check-label" for="otherHobbies">Other Hobbies</label>
                    <textarea name="Hobbies" class="form-control" id="hobbiesTextarea" rows="2" style="display: none"></textarea>
                </div>
            </div>
        </div>

        <!-- Declaration -->

        <div id="divDeclaration">
            <h4>Declaration</h4>
            <div class="form-check">
                <input class="form-check-input" type="checkbox" value="" id="declaration" />
                <label class="form-check-label" for="declaration">I declare that all information provided by me is correct</label>
            </div>
        </div>

        <div class="d-flex justify-content-center p-3" id="submitReset">
            <input class="btn btn-primary m-2" id="submit-btn" type="submit" value="Submit" />
            <input class="btn btn-primary m-2" type="reset" value="Reset" />
        </div>


        <input type="hidden" id="ObjType" value="UserDetails2" />

<%--        user control--%>
        <div class="row justify-content-between">
            <div class="col-5">
                <uc1:Notes runat="server" ID="Notes" ObjType="UserDetails2" />
            </div>
            <div class="col-5">
                <uc1:Document runat="server" ID="Document" ObjType="UserDetails2" />
            </div>
        </div>

    </main>


</asp:Content>
