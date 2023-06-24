<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="XRetailManagerUI.Pages.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


      <main>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
        <h1>User Management</h1>
  <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <!-- Left Section -->
                    <h2>User Role Grid</h2>
                              
                  <asp:Label ID="lblInventory" runat="server" />
             
          <div class="table-responsive">
            <asp:GridView ID="userManagementGrid" runat="server" CssClass="table table-striped table-bordered"  AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="DisplayRoles" HeaderText="Roles" />
                         <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Button ID="btnManageRoles" runat="server" class="btn btn-outline-success" Text="Manage Roles" OnClick="btnManageRoles_Click" CommandName="ManageRoles"  CommandArgument='<%# Eval("ID") %>' />
            </ItemTemplate>
        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
               </div>
        
                  
                <br/>
                <div class="col-md-6">
                    <!-- Right Section -->
                    <h2>User Role Management</h2>
                    <div class="form-group">
                        <label for="userRole">Manage User Role:</label>
                        <asp:Label runat="server" ID="selectedUser" />
                        <asp:ListBox id="lstUserRoles" class="form-control" runat="server">
                        
                        </asp:ListBox>
                    </div>
                    <div class="form-group">
                        <label for="newRole">Add New Role to User:</label>
                        <asp:DropDownList  id="ddlNewRole" class="form-control" runat="server">
                           
                        </asp:DropDownList>
                    </div>
                    <br/>
                    <div class="form-group">
                        <asp:Button ID="addRoleButton" runat="server" Text="Add Role" class="btn btn-outline-primary" OnClick="addRoleButton_Click" />
                        <asp:Button ID="removeRoleButton" runat="server" Text="Remove Role" class="btn  btn-outline-danger" OnClick="removeRoleButton_Click" />
                    </div>
                    <asp:Label runat="server" ID="lblUserManagementValidation" ForeColor="Red" />
                </div>
                 </div>
                     </div>


         <%-- modal for handling not unathorize --%>
  <div class="modal fade" id="modalValidator" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="smodalValidatorLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="modalValidatorLabel">Can't opent this page. Unauthorized!</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <asp:Button type="button" ID="btnClose" runat="server" OnClick="btnClose_Click" class="btn btn-secondary" data-bs-dismiss="modal" Text="Close"/>
       
      </div>
    </div>
  </div>
</div>

        </main>

</asp:Content>
