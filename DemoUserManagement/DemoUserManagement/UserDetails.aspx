<%@ Page Title="User Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="DemoUserManagement.Users" %>

<%@ Register Src="~/Notes.ascx" TagPrefix="uc1" TagName="Notes" %>
<%@ Register Src="~/Document.ascx" TagPrefix="uc1" TagName="Document" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>

    
    <div class="p-5 " id="personalDetails" runat="server" ClientIDMode="Static">
        <h4>Personal Details</h4>
        <div class="row" runat="server">
            <div class="col-md-4" runat="server">
                <asp:Label runat="server" AssociatedControlID="firstName" CssClass="form-label required">First Name :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="firstName" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="firstName" CssClass="errorMessage" ErrorMessage="First name is required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4" runat="server">
                <asp:Label runat="server" AssociatedControlID="middleName" CssClass="form-label">Middle Name : </asp:Label>
                <asp:TextBox runat="server" ID="middleName" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="col-md-4" runat="server">
                <asp:Label runat="server" AssociatedControlID="lastName" CssClass="form-label required">Last Name :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="lastName" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="lastName" CssClass="errorMessage" ErrorMessage="Last name is required"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row" runat="server">
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" AssociatedControlID="fatherName" CssClass="form-label required">Father's name :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="fatherName" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="fatherName" CssClass="errorMessage" ErrorMessage="Father's name is required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" AssociatedControlID="motherName" CssClass="form-label required">Mother's Name :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="motherName" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="motherName" CssClass="errorMessage" ErrorMessage="Mother's name is required"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row" runat="server">
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" AssociatedControlID="dateOfBirth" CssClass="form-label required">Date of Birth :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="dateOfBirth" CssClass="form-control" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="dateOfBirth" CssClass="errorMessage" ErrorMessage="Date of birth is required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" CssClass="form-label required">Gender :</asp:Label>
                <div class="d-flex" id="genderOptions" runat="server">
                    <div class="me-2" runat="server">
                        <asp:RadioButton runat="server" ID="male" GroupName="genderGroup" ClientIDMode="Static" />
                        <asp:Label runat="server" AssociatedControlID="male" CssClass="form-check-label" Text="Male"></asp:Label>
                    </div>
                    <div class="me-2" runat="server">
                        <asp:RadioButton runat="server" ID="female" GroupName="genderGroup" ClientIDMode="Static" />
                        <asp:Label runat="server" AssociatedControlID="female" CssClass="form-check-label" Text="Female"></asp:Label>
                    </div>
                    <div class="me-2" runat="server">
                        <asp:RadioButton runat="server" ID="other" GroupName="genderGroup" ClientIDMode="Static" />
                        <asp:Label runat="server" AssociatedControlID="other" CssClass="form-check-label" Text="Other"></asp:Label>
                    </div>
</div>
<%--                <asp:RequiredFieldValidator runat="server" ControlToValidate="male" CssClass="errorMessage" ErrorMessage="Gender is required"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="row" runat="server">
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" AssociatedControlID="aadharCardNo" CssClass="form-label required">Aadhaar No. :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="aadharCardNo" CssClass="form-control" type="number" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="aadharCardNo" CssClass="errorMessage" ErrorMessage="Aadhaar number is required" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:FileUpload runat="server" ID="aadhaarCardFile" CssClass="form-control" ClientIDMode="Static" />
                    <asp:Label ID="AadhaarFileName" Text="" runat="server"></asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="errorMessage" runat="server" ControlToValidate="aadharCardNo" ValidationExpression="^\d{12}$" ErrorMessage="Aadhaar number should be 12 digits" Display="Dynamic" />
                </div>

                <asp:Label runat="server" ID="testing" Text=""></asp:Label>
                <asp:GridView ID="GridViewFiles" runat="server" AutoGenerateColumns="False" CssClass="gridview" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:Image ID="uploadedImage" runat="server" ImageUrl='<%# Eval("ImagePath") %>' AlternateText="Uploaded Image" Style="max-width: 200px; max-height: 200px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" AssociatedControlID="profilePic" CssClass="form-label required">Upload your passport size photograph :</asp:Label>
                <div runat="server">
                    <asp:FileUpload runat="server" ID="profilePic" CssClass="form-control" ClientIDMode="Static" />
                    <asp:Label ID="ProfilePicFileName" Text="" runat="server"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="profilePic" CssClass="errorMessage" ErrorMessage="Profile picture is required"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
    </div>

    <div class="p-5" id="contactDetails" runat="server" ClientIDMode="Static">
        <h4>Contact Details</h4>
        <div class="row" runat="server">
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" AssociatedControlID="primaryMobileno" CssClass="form-label required">Mobile No :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="primaryMobileno" CssClass="form-control" type="number" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="primaryMobileno" CssClass="errorMessage" ErrorMessage="Primary mobile number is required" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="primaryMobileNoValidator" CssClass="errorMessage" runat="server" ControlToValidate="primaryMobileno" ValidationExpression="^\d{10}$" ErrorMessage="Phone number should be 10 digits" Display="Dynamic" />

                </div>
            </div>
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" AssociatedControlID="alternateMobileno" CssClass="form-label">Alternate Mobile No :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="alternateMobileno" CssClass="form-control" type="number" ClientIDMode="Static"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="alternateMobileNoValidator" CssClass="errorMessage" runat="server" ControlToValidate="alternateMobileno" ValidationExpression="^\d{10}$" ErrorMessage="Phone number should be 10 digits" Display="Dynamic" />

                </div>
            </div>
        </div>
        <div class="row" runat="server">
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" AssociatedControlID="primaryEmailId" CssClass="form-label required">Email ID :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="primaryEmailId" CssClass="form-control" type="email" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="primaryEmailId" CssClass="errorMessage" ErrorMessage="Primary email ID is required" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ControlToValidate="primaryEmailId" CssClass="errorMessage" ID="primaryMailIdValidator" runat="server" Display="Dynamic" ErrorMessage="Invalid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="col-md-6" runat="server">
                <asp:Label runat="server" AssociatedControlID="alternateEmailId" CssClass="form-label">Alternate Email ID :</asp:Label>
                <div runat="server">
                    <asp:TextBox runat="server" ID="alternateEmailId" CssClass="form-control" type="email" ClientIDMode="Static"></asp:TextBox>
                    <asp:RegularExpressionValidator ControlToValidate="alternateEmailId" CssClass="errorMessage" ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ErrorMessage="Invalid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                </div>
            </div>
            <div class="row">
                <div class="col-md-6" runat="server">
                    <asp:Label runat="server" AssociatedControlID="password" CssClass="form-label required">Password :</asp:Label>
                    <div runat="server">
                        <asp:TextBox runat="server" ID="password" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
<%--                        <asp:RequiredFieldValidator runat="server" ControlToValidate="password" CssClass="errorMessage" ErrorMessage="Password is required"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
            </div>
        </div>

            <h4>Address Details</h4>

            <!-- Current Address -->
            <h5>Current Address</h5>
            <div class="row" runat="server">
                <div class="col-md-6" runat="server">
                    <asp:Label runat="server" AssociatedControlID="currentLocality" CssClass="form-label required">Locality :</asp:Label>
                    <div runat="server">
                        <asp:TextBox runat="server" ID="currentLocality" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="currentLocality" CssClass="errorMessage" ErrorMessage="Current locality is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-6" runat="server">
                    <asp:Label runat="server" AssociatedControlID="currentCity" CssClass="form-label required">City :</asp:Label>
                    <div runat="server">
                        <asp:TextBox runat="server" ID="currentCity" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="currentCity" CssClass="errorMessage" ErrorMessage="Current city is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row" runat="server">
                <div class="col-md-4" runat="server">
                    <asp:Label runat="server" AssociatedControlID="currentCntry" CssClass="form-label required">Country :</asp:Label>
                    <div runat="server">
<%--                        <asp:TextBox runat="server" ID="currentCntry" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>--%>
                            <asp:DropDownList runat="server" ID="currentCntry" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ShowSates"></asp:DropDownList>

                        <asp:RequiredFieldValidator runat="server" ID="countryValidator" ControlToValidate="currentCntry"
                            InitialValue="" ErrorMessage="Please select a country" CssClass="errorMessage" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4" runat="server">
                    <asp:Label runat="server" AssociatedControlID="currentState" CssClass="form-label required">State :</asp:Label>
                    <div runat="server">
                        <%--<asp:TextBox runat="server" ID="currentState" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>--%>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="currentState" CssClass="form-control" ClientIDMode="Static">
                                    <asp:ListItem Text="Select State --" Value="" />
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="currentCntry" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                      <asp:RequiredFieldValidator runat="server" ControlToValidate="currentState" CssClass="errorMessage" ErrorMessage="Current state is required"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="col-md-4" runat="server">
                    <asp:Label runat="server" AssociatedControlID="currentPincode" CssClass="form-label required">Pincode :</asp:Label>
                    <div runat="server">
                        <asp:TextBox runat="server" ID="currentPincode" CssClass="form-control" type="number" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="currentPincode" CssClass="errorMessage" ErrorMessage="Current pincode is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <!-- Permanent Address -->
        <h5>Permanent Address</h5>
        <div class="" runat="server">

            <asp:CheckBox runat="server" CssClass="me-3" ID="sameAsCurrentAddressCheck" ClientIDMode="Static"
                OnCheckedChanged="sameAsCurrentAddressCheck_CheckedChanged" AutoPostBack="true" />

            <asp:Label runat="server" AssociatedControlID="sameAsCurrentAddressCheck" CssClass="form-check-label" Text="Same as current address"></asp:Label>

        </div>
            <div class="row" runat="server">
                <div class="col-md-6" runat="server">
                    <asp:Label runat="server" AssociatedControlID="permanentLocality" CssClass="form-label required">Locality :</asp:Label>
                    <div runat="server">
                        <asp:TextBox runat="server" ID="permanentLocality" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="permanentLocality" CssClass="errorMessage" ErrorMessage="Permanent locality is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-6" runat="server">
                    <asp:Label runat="server" AssociatedControlID="permanentCity" CssClass="form-label required">City :</asp:Label>
                    <div runat="server">
                        <asp:TextBox runat="server" ID="permanentCity" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="permanentCity" CssClass="errorMessage" ErrorMessage="Permanent city is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row" runat="server">
                <div class="col-md-4" runat="server">
                    <asp:Label runat="server" AssociatedControlID="permanentCntry" CssClass="form-label required">Country :</asp:Label>
                    <div runat="server">
                            <asp:DropDownList runat="server" ID="permanentCntry" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ShowSates"></asp:DropDownList>

                        <asp:RequiredFieldValidator runat="server" ID="permanentCountryValidator" ControlToValidate="permanentCntry"
                            InitialValue="" ErrorMessage="Please select a country" CssClass="errorMessage" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4" runat="server">
                    <asp:Label runat="server" AssociatedControlID="permanentState" CssClass="form-label required">State :</asp:Label>
                    <div runat="server">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="permanentState" CssClass="form-control" ClientIDMode="Static">
                                    <asp:ListItem Text="Select State --" Value="" />
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="permanentCntry" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="permanentState" CssClass="errorMessage" ErrorMessage="Permanent state is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4" runat="server">
                    <asp:Label runat="server" AssociatedControlID="permanentPincode" CssClass="form-label required">Pincode :</asp:Label>
                    <div runat="server">
                        <asp:TextBox runat="server" ID="permanentPincode" CssClass="form-control" type="number" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="permanentPincode" CssClass="errorMessage" ErrorMessage="Permanent pincode is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>

<%--    </div>--%>


    <div class="p-5" id="educationDetails" runat="server" ClientIDMode="Static">
        <h4>Education Details</h4>
        <div class="table-responsive-lg" runat="server">
            <table class="table table-bordered" id="tableEducationDetails" runat="server">
                <thead>
                    <tr>
                        <th>Standard</th>
                        <th class="required">Institution</th>
                        <th class="required">Board/University</th>
                        <th class="required">Marks Obtained(in %)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="TenthDetails">
                        <td>10th</td>
                        <td>
                            <div runat="server">
                                <asp:TextBox runat="server" ID="TenthInst" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TenthInst" CssClass="errorMessage" ErrorMessage="10th institution is required"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>
                            <div runat="server">
                                <asp:TextBox runat="server" ID="TenthBoard" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TenthBoard" CssClass="errorMessage" ErrorMessage="10th board is required"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>
                            <div runat="server">
                                <asp:TextBox runat="server" ID="TenthMarks" CssClass="form-control" type="number" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TenthMarks" CssClass="errorMessage" ErrorMessage="10th marks are required"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr id="TwelveDetails">
                        <td>12th</td>
                        <td>
                            <div runat="server">
                                <asp:TextBox runat="server" ID="TwelveInst" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TwelveInst" CssClass="errorMessage" ErrorMessage="12th institution is required"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>
                            <div runat="server">
                                <asp:TextBox runat="server" ID="TwelveBoard" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TwelveBoard" CssClass="errorMessage" ErrorMessage="12th board is required"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>
                            <div runat="server">
                                <asp:TextBox runat="server" ID="TwelveMarks" CssClass="form-control" type="number" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TwelveMarks" CssClass="errorMessage" ErrorMessage="12th marks are required"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr id="GraduationDetails">
                        <td>Graduation</td>
                        <td>
                            <div runat="server">
                                <asp:TextBox runat="server" ID="gradInst" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="gradInst" CssClass="errorMessage" ErrorMessage="Graduation institution is required"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>
                            <div runat="server">
                                <asp:TextBox runat="server" ID="gradUni" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="gradUni" CssClass="errorMessage" ErrorMessage="Graduation university is required"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>
                            <div runat="server">
                                <asp:TextBox runat="server" ID="gradMarks" CssClass="form-control" type="number" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="gradMarks" CssClass="errorMessage" ErrorMessage="Graduation marks are required"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>


    <div class="p-5 border" id="extraDetails" runat="server">
        <h4>Extra Details</h4>
        <h5>Hobbies</h5>
        <div class="d-flex flex-wrap" runat="server">


            <asp:CheckBoxList ID="hobbiesCheckBoxList" runat="server">
                <asp:ListItem Text="Cricket" Value="Cricket" />
                <asp:ListItem Text="Singing" Value="Singing" />
                <asp:ListItem Text="Dancing" Value="Dancing" />
            </asp:CheckBoxList>


        </div>
    </div>


    <div id="divDeclaration" runat="server" ClientIDMode="Static">
        <h4>Declaration</h4>
        <div class="form-check" runat="server">
            <asp:CheckBox runat="server" ID="declaration" />
<asp:Label runat="server" AssociatedControlID="declaration" CssClass="form-check-label" Text="I declare that all information provided by me are correct"></asp:Label>
        </div>
    </div>

    <div class="d-flex justify-content-center m-3" id="submitReset" runat="server">
<asp:Button runat="server" ID="Button1" CssClass="btn btn-primary me-3" Text="Submit" OnClick="SaveUser" OnClientClick="return scrollToInvalid();"/>
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Reset" OnClientClick="return confirm('Are you sure you want to reset?');" />
    </div>

        <hr />
        <hr />

        <asp:HiddenField runat="server" ID="scrollPosition" />
        <div class="row">
            <div class="col-6">
                <uc1:Notes runat="server" id="Notes" ObjType="UserDetails"/>
            </div>
            <div class="col-6">
                <uc1:Document runat="server" id="Document" ObjType="UserDetails"/>
            </div>
        </div>
        
        
</main>
</asp:Content>
